namespace Site.Storage
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface ICommentStore
    {
        IEnumerable<CommentEntity> GetAllComments(string blogRowKey);

        Task CommentAsync(CommentEntity entity);
        Task<CommentEntity> GetAsync(string partitionKey, string rowKey);
        Task<List<CommentEntity>> GetAllForBlogAsync(string blogRowKey);
    }
}