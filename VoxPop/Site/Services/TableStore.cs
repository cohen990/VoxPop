namespace Site.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.UI.WebControls;
    using Microsoft.Data.Edm.Csdl;
    using Microsoft.WindowsAzure;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Table;

    public class TableStore<TEntity> : IStore<TEntity> where TEntity : class, ITableEntity, new()
    {
        private readonly CloudTable _table;

        public TableStore()
        {
            CloudTableClient client = GetStorageAccount().CreateCloudTableClient();

            _table = client.GetTableReference("voxpopblogs");
            _table.CreateIfNotExists();
        }

        public IEnumerable<TEntity> GetAll()
        {
            var query = new TableQuery<TEntity>();

            IEnumerable<TEntity> entities = _table.ExecuteQuery(query).Select(x => x);

            return entities;
        }

        public async Task CreateAsync(TEntity entity)
        {
            var operation = TableOperation.Insert(entity);
            await _table.ExecuteAsync(operation);
        }

        public void Merge(TEntity entity)
        {
            TableOperation operation = TableOperation.Merge(entity);

            _table.Execute(operation);
        }

        public TEntity Get(string entityRowKey, string entityPartitionKey)
        {
            TableOperation operation = TableOperation.Retrieve<TEntity>(entityPartitionKey, entityRowKey);

            TableResult result = _table.Execute(operation);

            var entity = result.Result as TEntity;

            return entity;
        }

        private CloudStorageAccount GetStorageAccount()
        {
            string connectionString = CloudConfigurationManager.GetSetting("voxpop.storageaccount");

            CloudStorageAccount storageAccount;
            if (CloudStorageAccount.TryParse(connectionString, out storageAccount))
                return storageAccount;

            string message = string.Format("The connection string '{0}' is invalid.", connectionString);
            throw new InvalidOperationException(message);
        }
    }
}