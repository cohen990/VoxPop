namespace Site.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web;
    using Models;
    using Storage.Models;

    public interface IBlogService
    {
        IEnumerable<BlogPostEntity> GetAllBlogs();

        void Vote(VoteModel model);

        BlogPostEntity GetBlog(string rowKey, string partitionKey);

        Task CreateBlogAsync(BlogViewModel blog, HttpPostedFileBase image, string userName);
    }
}