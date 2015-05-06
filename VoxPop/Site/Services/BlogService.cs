namespace Site.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;
    using Microsoft.Security.Application;
    using Models;
    using Storage;
    using Storage.Models;

    public class BlogService : IBlogService
    {
        private readonly IBlogStore _blogStore;

        private readonly IResponseStore _responseStore;

        private readonly IImageStore _imageStore;

        private readonly IVoteStore _voteStore;

        private readonly IVoteService _voteService;

        private readonly ICommentStore _commentStore;

        public BlogService(IBlogStore blogStore, IResponseStore responseStore, IImageStore imageStore, IVoteStore voteStore, IVoteService voteService, ICommentStore commentStore)
        {
            _blogStore = blogStore;
            _responseStore = responseStore;
            _imageStore = imageStore;
            _voteStore = voteStore;
            _voteService = voteService;
            _commentStore = commentStore;
        }

        public async Task CreateBlogAsync(
            BlogModel blog,
            HttpPostedFileBase imageFile,
            string authorName,
            string authorIdentifier,
            string sharedBlogIdentifier)
        {
            Uri imageUri = _imageStore.StoreImageAsync(imageFile);

            blog.ImageUri = imageUri;
            blog.AuthorIdentifier = authorIdentifier;
            blog.Author = authorName;
            if (sharedBlogIdentifier != "")
            {
                blog.BlogIdentifier = sharedBlogIdentifier;
            }
            var blogEntity = BlogPostEntity.For(blog);

            await _blogStore.CreateBlogAsync(blogEntity);
        }

        public async Task CreateResponseAsync(
            ResponseModel response,
            HttpPostedFileBase imageFile,
            string authorName,
            string authorIdentifier,
            string replyeeTitle,
            string replyee,
            string replyeeBlogIdentifier,
            string replyeeIdentifier,
            string sharedBlogIdentifier)
        {
            Uri imageUri = _imageStore.StoreImageAsync(imageFile);

            response.ImageUri = imageUri;
            response.AuthorIdentifier = authorIdentifier;
            response.Author = authorName;
            response.BlogIdentifier = sharedBlogIdentifier;
            response.ReplyeeTitle = replyeeTitle;
            response.Replyee = replyee;
            response.ReplyeeRowKey = replyeeBlogIdentifier;
            response.ReplyeePartitionKey = replyeeIdentifier;
            var responseEntity = ResponseEntity.For(response);

            await _responseStore.CreateResponseAsync(responseEntity);

        }

        public IEnumerable<BlogPostEntity> GetAllBlogs()
        {
            IEnumerable<BlogPostEntity> blogs = _blogStore.GetAllBlogs();

            IEnumerable<BlogPostEntity> blogsWithVotes = blogs
                .Select(x => _voteService.RetrieveVotes(x)
                    .GetAwaiter()
                    .GetResult());

            List<BlogPostEntity> sortedBlogs = blogsWithVotes.OrderByDescending(b => b.Timestamp).ToList();

            return sortedBlogs;
        }

        public IEnumerable<ResponseEntity> GetAllResponses(string blogRowKey)
        {
            IEnumerable<ResponseEntity> responses = _responseStore.GetAllResponses(blogRowKey);

            IEnumerable<ResponseEntity> responsesWithVotes = responses
                .Select(x => _voteService.RetrieveVotes(x)
                    .GetAwaiter()
                    .GetResult());

            List<ResponseEntity> sortedResponses = responsesWithVotes.OrderByDescending(b => b.Timestamp).ToList();

            return sortedResponses;
        }

        public IEnumerable<CommentEntity> GetAllComments(string blogRowKey)
        {
            IEnumerable<CommentEntity> comments = _commentStore.GetAllComments(blogRowKey);

            IEnumerable<CommentEntity> commentsWithVotes = comments
                .Select(x => _voteService.RetrieveVotes(x)
                    .GetAwaiter()
                    .GetResult());

            List<CommentEntity> sortedComments = commentsWithVotes.OrderByDescending(b => b.RowKey).ToList();

            return sortedComments;
        }

        public IEnumerable<VoteEntity> GetAllVotes(string blogRowKey)
        {
            IEnumerable<VoteEntity> votes = _voteStore.GetAllVotes(blogRowKey);

            List<VoteEntity> sortedVotes = votes.OrderByDescending(b => b.RowKey).ToList();

            return sortedVotes;
        }

        public async Task<BlogModel> GetBlog(string blogRowKey, string blogPartitionKey)
        {
            BlogPostEntity entity = _blogStore.GetBlog(blogRowKey, blogPartitionKey);

            if (entity != null)
            {
                entity = await _voteService.RetrieveVotes(entity);

                return entity.ToModel();
            }
            else
            {
                return null;
            }
        }

        public async Task<ResponseModel> GetResponse(string blogRowKey, string blogPartitionKey)
        {
            ResponseEntity entity = _responseStore.GetResponse(blogRowKey, blogPartitionKey);

            if (entity != null)
            {
                entity = await _voteService.RetrieveVotes(entity);

                return entity.ToModel();
            }
            else
            {
                return null;
            }
        }

        public void UpdateBlog(BlogModel updatedBlog)
        {
            BlogPostEntity originalBlog = _blogStore.GetBlog(updatedBlog.BlogIdentifier, updatedBlog.AuthorIdentifier);

            originalBlog.UpdateContent(updatedBlog.Content, updatedBlog.Poll);

            _blogStore.MergeBlog(originalBlog);
        }

        public void UpdateResponse(ResponseModel updatedResponse)
        {
            ResponseEntity originalResponse = _responseStore.GetResponse(updatedResponse.BlogIdentifier, updatedResponse.AuthorIdentifier);

            if (originalResponse != null)
            {

                originalResponse.UpdateContent(updatedResponse.Content);

                _responseStore.MergeResponse(originalResponse);
            }
        }

        public void DeleteYourBlog(BlogModel blog)
        {
            BlogPostEntity blogToDelete = _blogStore.GetBlog(blog.BlogIdentifier, blog.AuthorIdentifier);

            _blogStore.DeleteBlog(blogToDelete);
        }

        public void DeleteYourResponse(ResponseModel response)
        {
            ResponseEntity responseToDelete = _responseStore.GetResponse(response.BlogIdentifier, response.AuthorIdentifier);

            _responseStore.DeleteResponse(responseToDelete);
        }


        public IEnumerable<BlogPostEntity> GetAuthorBlogs(string Auth)
        {
            IEnumerable<BlogPostEntity> blogs = _blogStore.GetAuthorBlogs(Auth);

            IEnumerable<BlogPostEntity> blogsWithVotes = blogs
                .Select(x => _voteService.RetrieveVotes(x)
                    .GetAwaiter()
                    .GetResult());

            List<BlogPostEntity> sortedBlogs = blogsWithVotes.OrderByDescending(b => b.Timestamp).ToList();

            return sortedBlogs;
        }

        public async Task Vote(VoteModel model)
        {
            var voteStore = new TableVoteStore();

            var voteEntity = VoteEntity.For(model);

            await voteStore.VoteAsync(voteEntity);
        }

        public async Task Comment(CommentModel model)
        {
            var commentStore = new TableCommentStore();

            var commentEntity = CommentEntity.For(model);

            await commentStore.CommentAsync(commentEntity);
        }
    }
}