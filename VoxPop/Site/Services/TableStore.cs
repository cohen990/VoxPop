namespace Site.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.WindowsAzure;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Table;

    public class TableStore<TEntity> : IStore<TEntity> where TEntity : class, ITableEntity
    {
        private readonly CloudTableClient _client;

        private readonly CloudTable _table;

        public TableStore()
        {
            _client = GetStorageAccount().CreateCloudTableClient();

            _table = _client.GetTableReference("voxpopblogs");
            _table.CreateIfNotExists();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var query = new TableQuery();

            IEnumerable<TEntity> entities = _table.ExecuteQuery(query).Select(x => x as TEntity);

            return entities;
        }

        public void CreateAsync(TEntity entity)
        {
            throw new NotImplementedException();
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

    public interface IStore<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();

        void CreateAsync(TEntity entity);
    }
}