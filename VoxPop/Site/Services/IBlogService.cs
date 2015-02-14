namespace Site.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web;
    using Models;
    using Storage.Models;

    public interface IBlogService
    {
        Task CreateBlogAsync(BlogViewModel blog, HttpPostedFileBase imageFile);

        IEnumerable<BlogPostEntity> GetAllBlogs();

        void Vote(VoteModel model);

        BlogPostEntity GetBlog(string rowKey, string partitionKey);
    }
}