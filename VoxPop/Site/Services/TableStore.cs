namespace Site.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
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