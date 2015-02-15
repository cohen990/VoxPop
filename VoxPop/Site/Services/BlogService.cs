namespace Site.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;
    using Models;
    using Storage;
    using Storage.Models;

    public class BlogService : IBlogService
    {
        private readonly IBlogStore _blogBlogStore;

        private readonly IImageStore _imageStore;

        public BlogService(IBlogStore blogStore, IImageStore imageStore)
        {
            _blogBlogStore = blogStore;
            _imageStore = imageStore;
        }

        public async Task CreateBlogAsync(BlogViewModel blog, HttpPostedFileBase imageFile, string userName)
        {
            Uri imageUri = _imageStore.StoreImageAsync(imageFile);

            var blogEntity = blog.AsEntity(imageUri, userName);

            await _blogBlogStore.CreateBlogAsync(blogEntity);
        }

        public IEnumerable<BlogPostEntity> GetAllBlogs()
        {
            IEnumerable<BlogPostEntity> blogs = _blogBlogStore.GetAllBlogs();

            List<BlogPostEntity> sortedBlogs = blogs.OrderByDescending(b => b.Timestamp).ToList();

            return sortedBlogs;
        }

        public void Vote(VoteModel model)
        {
            BlogPostEntity blogPost = _blogBlogStore.GetBlog(model.BlogPostRowKey, model.BlogPostPartitionKey);

            var key =
                blogPost.Poll.Keys.Single(x => x.Trim().Equals(model.PollItemKey, StringComparison.OrdinalIgnoreCase));

            blogPost.Poll[key] += 1;

            _blogBlogStore.MergeBlog(blogPost);
        }

        public BlogPostEntity GetBlog(string rowKey, string partitionKey)
        {
            return _blogBlogStore.GetBlog(rowKey, partitionKey);
        }
    }
}