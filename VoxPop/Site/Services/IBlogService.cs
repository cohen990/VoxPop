namespace Site.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web;
    using Storage.Models;
    using ViewModels;

    public interface IBlogService
    {
        Task CreateBlogAsync(BlogViewModel blog, HttpPostedFileBase imageFile);

        IEnumerable<BlogPostEntity> GetAllBlogs();

        void Vote(VoteModel model);

        BlogPostEntity GetBlog(string rowKey, string partitionKey);
    }
}