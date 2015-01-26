namespace Site.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IStore<TEntity>
    {
        IEnumerable<TEntity> GetAll();

        Task CreateAsync(TEntity entity);

        void Merge(TEntity entity);

        TEntity Get(string entityRowKey, string entityPartitionKey);
    }
}