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

        IEnumerable<CommentEntity> GetAllComments(string blogRowKey);


        IEnumerable<BlogPostEntity> GetAuthorBlogs(string Auth);

        Task Vote(VoteModel model);

        Task Comment(CommentModel model);

        Task<BlogModel> GetBlog(string blogRowKey, string blogPartitionKey);

        void UpdateBlog(BlogModel updatedBlog);

        Task CreateBlogAsync(BlogModel blog, HttpPostedFileBase image, string authorName, string authorIdentifier);
    }
}