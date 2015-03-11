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

        Task Vote(VoteModel model);

        Task<BlogModel> GetBlog(string blogRowKey, string blogPartitionKey);

        Task CreateBlogAsync(BlogModel blog, HttpPostedFileBase image, string authorName, string authorIdentifier);
    }
}