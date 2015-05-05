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

    public class TableVoteStore : IVoteStore
    {
        private readonly CloudTable _table;

        public TableVoteStore()
        {
            CloudTableClient client = GetStorageAccount().CreateCloudTableClient();

            _table = client.GetTableReference("voxpopvotes");
            _table.CreateIfNotExists();
        }

        public async Task VoteAsync(VoteEntity entity)
        {
            var operation = TableOperation.InsertOrReplace(entity);
            await _table.ExecuteAsync(operation);
        }

        private CloudStorageAccount GetStorageAccount()
        {
            string connectionString = CloudConfigurationManager.GetSetting("voxpop.votestorage");

            CloudStorageAccount storageAccount;
            if (CloudStorageAccount.TryParse(connectionString, out storageAccount))
                return storageAccount;

            string message = string.Format("The connection string '{0}' is invalid.", connectionString);
            throw new InvalidOperationException(message);
        }

        public async Task<VoteEntity> GetAsync(string partitionKey, string rowKey)
        {
            TableOperation operation = TableOperation.Retrieve(partitionKey, rowKey);

            TableResult result = await _table.ExecuteAsync(operation);

            return result.Result as VoteEntity;
        }

        public async Task<List<VoteEntity>> GetAllForBlogAsync(string blogRowKey)
        {
            var query = new TableQuery<VoteEntity>()
                .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, blogRowKey));

            List<VoteEntity> result = _table.ExecuteQuery(query).ToList();

            return result;
        }

        public IEnumerable<VoteEntity> GetAllVotes(string blogRowKey)
        {
            var query = new TableQuery<VoteEntity>();

            IEnumerable<VoteEntity> entities = _table.ExecuteQuery(query)
                .Where(x => x.PartitionKey == blogRowKey)
                .Select(x => x);

            return entities;
        }
    }
}