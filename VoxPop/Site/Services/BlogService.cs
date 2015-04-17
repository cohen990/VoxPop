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

        private readonly IImageStore _imageStore;

        private readonly IVoteService _voteService;

        public BlogService(IBlogStore blogStore, IImageStore imageStore, IVoteService voteService)
        {
            _blogStore = blogStore;
            _imageStore = imageStore;
            _voteService = voteService;
        }

        public async Task CreateBlogAsync(
            BlogModel blog,
            HttpPostedFileBase imageFile,
            string authorName,
            string authorIdentifier)
        {
            Uri imageUri = _imageStore.StoreImageAsync(imageFile);

            blog.ImageUri = imageUri;
            blog.AuthorIdentifier = authorIdentifier;
            blog.Author = authorName;
            var blogEntity = BlogPostEntity.For(blog);

            await _blogStore.CreateBlogAsync(blogEntity);
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

        public async Task<BlogModel> GetBlog(string blogRowKey, string blogPartitionKey)
        {
            BlogPostEntity entity = _blogStore.GetBlog(blogRowKey, blogPartitionKey);

            entity = await _voteService.RetrieveVotes(entity);

            return entity.ToModel();
        }

        public void UpdateBlog(BlogModel updatedBlog)
        {
            BlogPostEntity originalBlog = _blogStore.GetBlog(updatedBlog.BlogIdentifier, updatedBlog.AuthorIdentifier);

            originalBlog.UpdateContent(updatedBlog.Content);

            _blogStore.MergeBlog(originalBlog);
        }

        public IEnumerable<BlogPostEntity> GetAuthorBlogs(string blogPartitionKey)
        {
            IEnumerable<BlogPostEntity> blogs = _blogStore.GetAuthorBlogs(blogPartitionKey);

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