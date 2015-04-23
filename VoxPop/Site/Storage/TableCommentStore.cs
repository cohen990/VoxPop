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

    public class TableCommentStore : ICommentStore
    {
        private readonly CloudTable _table;

        public TableCommentStore()
        {
            CloudTableClient client = GetStorageAccount().CreateCloudTableClient();

            _table = client.GetTableReference("voxpopcomments");
            _table.CreateIfNotExists();
        }

        public async Task CommentAsync(CommentEntity entity)
        {
            var operation = TableOperation.Insert(entity);
            await _table.ExecuteAsync(operation);
        }

        private CloudStorageAccount GetStorageAccount()
        {
            string connectionString = CloudConfigurationManager.GetSetting("voxpop.commentstorage");

            CloudStorageAccount storageAccount;
            if (CloudStorageAccount.TryParse(connectionString, out storageAccount))
                return storageAccount;

            string message = string.Format("The connection string '{0}' is invalid.", connectionString);
            throw new InvalidOperationException(message);
        }

        public async Task<CommentEntity> GetAsync(string partitionKey, string rowKey)
        {
            TableOperation operation = TableOperation.Retrieve(partitionKey, rowKey);

            TableResult result = await _table.ExecuteAsync(operation);

            return result.Result as CommentEntity;
        }

        public async Task<List<CommentEntity>> GetAllForBlogAsync(string blogRowKey)
        {
            var query = new TableQuery<CommentEntity>()
                .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, blogRowKey));

            List<CommentEntity> result = _table.ExecuteQuery(query).ToList();

            return result;
        }

        public IEnumerable<CommentEntity> GetAllComments(string blogRowKey)
        {
            var query = new TableQuery<CommentEntity>();

            IEnumerable<CommentEntity> entities = _table.ExecuteQuery(query)
                .Where(x => x.PartitionKey == blogRowKey)
                .Select(x => x);

            return entities;
        }

        public BlogPostEntity GetBlog(string entityRowKey, string entityPartitionKey)
        {
            TableOperation operation = TableOperation.Retrieve<BlogPostEntity>(entityPartitionKey, entityRowKey);

            TableResult result = _table.Execute(operation);

            var entity = result.Result as BlogPostEntity;

            return entity;
        }


    }
}