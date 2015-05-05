namespace Site.Storage
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface IBlogStore
    {
        IEnumerable<BlogPostEntity> GetAllBlogs();

        IEnumerable<BlogPostEntity> GetAuthorBlogs(string blogPartitionKey);

        Task CreateBlogAsync(BlogPostEntity entity);

        void MergeBlog(BlogPostEntity entity);

        void DeleteBlog(BlogPostEntity entity);

        BlogPostEntity GetBlog(string entityRowKey, string entityPartitionKey);
    }
}