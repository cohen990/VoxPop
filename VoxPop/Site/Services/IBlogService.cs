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

        IEnumerable<ResponseEntity> GetAllResponses(string blogRowKey);

        IEnumerable<CommentEntity> GetAllComments(string blogRowKey);


        IEnumerable<BlogPostEntity> GetAuthorBlogs(string Auth);

        Task Vote(VoteModel model);

        Task Comment(CommentModel model);

        Task<BlogModel> GetBlog(string blogRowKey, string blogPartitionKey);

        Task<ResponseModel> GetResponse(string blogRowKey, string blogPartitionKey);

        void UpdateBlog(BlogModel updatedBlog);

        void UpdateResponse(ResponseModel updatedResponse);

        Task CreateBlogAsync(BlogModel blog, HttpPostedFileBase image, string authorName, string authorIdentifier, string sharedBlogIdentifier);

        Task CreateResponseAsync(
            ResponseModel response,
            HttpPostedFileBase image,
            string authorName,
            string authorIdentifier,
            string replyeeTitle,
            string replyee,
            string replyeeBlogIdentifier,
            string replyeeIdentifier,
            string sharedBlogIdentifier);
    }
}