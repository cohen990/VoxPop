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

    public class TableBlogStore : IBlogStore
    {
        private readonly CloudTable _table;

        public TableBlogStore()
        {
            CloudTableClient client = GetStorageAccount().CreateCloudTableClient();

            _table = client.GetTableReference("voxpopblogs");
            _table.CreateIfNotExists();
        }

        public IEnumerable<BlogPostEntity> GetAllBlogs()
        {
            var query = new TableQuery<BlogPostEntity>();

            IEnumerable<BlogPostEntity> entities = _table.ExecuteQuery(query).Select(x => x);

            return entities;
        }

        public IEnumerable<BlogPostEntity> GetAuthorBlogs(string entityPartitionKey)
        {
            var query = new TableQuery<BlogPostEntity>();

            IEnumerable<BlogPostEntity> entities = _table.ExecuteQuery(query).Select(x => x);

            return entities;
        }

        public BlogPostEntity GetBlog(string entityRowKey, string entityPartitionKey)
        {
            TableOperation operation = TableOperation.Retrieve<BlogPostEntity>(entityPartitionKey, entityRowKey);

            TableResult result = _table.Execute(operation);

            var entity = result.Result as BlogPostEntity;

            return entity;
        }

        public async Task CreateBlogAsync(BlogPostEntity entity)
        {
            var operation = TableOperation.Insert(entity);
            await _table.ExecuteAsync(operation);
        }

        public void MergeBlog(BlogPostEntity entity)
        {
            TableOperation operation = TableOperation.Merge(entity);

            _table.Execute(operation);
        }

        private CloudStorageAccount GetStorageAccount()
        {
            string connectionString = CloudConfigurationManager.GetSetting("voxpop.articlestorage");

            CloudStorageAccount storageAccount;
            if (CloudStorageAccount.TryParse(connectionString, out storageAccount))
                return storageAccount;

            string message = string.Format("The connection string '{0}' is invalid.", connectionString);
            throw new InvalidOperationException(message);
        }
    }
}