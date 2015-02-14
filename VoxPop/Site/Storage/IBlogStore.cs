namespace Site.Storage
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface IBlogStore
    {
        IEnumerable<BlogPostEntity> GetAllBlogs();

        Task CreateBlogAsync(BlogPostEntity entity);

        void MergeBlog(BlogPostEntity entity);

        BlogPostEntity GetBlog(string entityRowKey, string entityPartitionKey);
    }
}