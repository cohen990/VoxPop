namespace Site.Storage
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.WindowsAzure;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Table;
    using Models;

    public class TableResponseStore : IResponseStore
    {
        private readonly CloudTable _table;

        public TableResponseStore()
        {
            CloudTableClient client = GetStorageAccount().CreateCloudTableClient();

            _table = client.GetTableReference("voxpopresponses");
            _table.CreateIfNotExists();
        }

        public IEnumerable<ResponseEntity> GetAllResponses(string blogRowKey)
        {
            var query = new TableQuery<ResponseEntity>();

            IEnumerable<ResponseEntity> entities = _table.ExecuteQuery(query)
                .Where(x => x.ReplyeeRowKey == blogRowKey)
                .Select(x => x);

            return entities;
        }

        public ResponseEntity GetResponse(string entityRowKey, string entityPartitionKey)
        {
            TableOperation operation = TableOperation.Retrieve<ResponseEntity>(entityPartitionKey, entityRowKey);

            TableResult result = _table.Execute(operation);

            var entity = result.Result as ResponseEntity;

            return entity;
        }

        public async Task CreateResponseAsync(ResponseEntity entity)
        {
            var operation = TableOperation.Insert(entity);
            await _table.ExecuteAsync(operation);
        }

        public void MergeResponse(ResponseEntity entity)
        {
            TableOperation operation = TableOperation.Merge(entity);

            _table.Execute(operation);
        }

        public void DeleteResponse(ResponseEntity entity)
        {
            if (entity != null)
            {
                TableOperation operation = TableOperation.Delete(entity);

                _table.Execute(operation);
            }
        }

        private CloudStorageAccount GetStorageAccount()
        {
            string connectionString = CloudConfigurationManager.GetSetting("voxpop.responsestorage");

            CloudStorageAccount storageAccount;
            if (CloudStorageAccount.TryParse(connectionString, out storageAccount))
                return storageAccount;

            string message = string.Format("The connection string '{0}' is invalid.", connectionString);
            throw new InvalidOperationException(message);
        }
    }
}