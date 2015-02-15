namespace Site.Storage
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface IVoteStore
    {
        Task VoteAsync(VoteEntity entity);
        Task<VoteEntity> GetAsync(string partitionKey, string rowKey);
        Task<List<VoteEntity>> GetAllForBlogAsync(string blogRowKey);
    }
}