namespace Site.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Storage;
    using Storage.Models;
    using ViewModels;
    using System.IO;

    public class BlogService : IBlogService
    {
        private readonly IBlogStore _blogBlogStore;

        private readonly IImageStore _imageStore;

        public BlogService(IBlogStore blogStore, IImageStore imageStore)
        {
            _blogBlogStore = blogStore;
            _imageStore = imageStore;
        }

        /// <summary>
        /// Creates a new <see cref="BlogViewModel"/> entity in the table storage.
        /// </summary>
        /// <param name="blog">This is the blog entity which will be inserted into the database.</param>
        /// <returns>Returns <see cref="Task"/> </returns>
        public async Task CreateAsync(BlogViewModel blog, Stream imageStream)
        {
            Uri imageUri = _imageStore.StoreImageAsync(imageStream);

            var blogEntity = blog.AsEntity(imageUri);

            await _blogBlogStore.CreateBlogAsync(blogEntity);
        }

        public IEnumerable<BlogPostEntity> GetAll()
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

        public BlogPostEntity Get(string rowKey, string partitionKey)
        {
            return _blogBlogStore.GetBlog(rowKey, partitionKey);
        }
    }
}