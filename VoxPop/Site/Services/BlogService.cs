namespace Site.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Models;

    class BlogService : IBlogService
    {
        private readonly IStore<BlogPostEntity> _blogStore;

        public BlogService()
        {
            _blogStore = new TableStore<BlogPostEntity>();
        }

        /// <summary>
        /// Creates a new <see cref="BlogViewModel"/> entity in the table storage.
        /// </summary>
        /// <param name="blog">This is the blog entity which will be inserted into the database.</param>
        /// <returns>Returns <see cref="Task"/> </returns>
        public async Task CreateAsync(BlogViewModel blog, string encodedImage)
        {
            var blogEntity = blog.AsEntity(encodedImage);

            await _blogStore.CreateAsync(blogEntity);
        }

        public IEnumerable<BlogPostEntity> GetAll()
        {
            IEnumerable<BlogPostEntity> blogs = _blogStore.GetAll();

            List<BlogPostEntity> sortedBlogs = blogs.OrderByDescending(b => b.Timestamp).ToList();

            return sortedBlogs;
        }

        public void Vote(VoteModel model)
        {
            BlogPostEntity blogPost = _blogStore.Get(model.BlogPostRowKey, model.BlogPostPartitionKey);

            var key =
                blogPost.Poll.Keys.Single(x => x.Trim().Equals(model.PollItemKey, StringComparison.OrdinalIgnoreCase));

            blogPost.Poll[key] += 1;

            _blogStore.Merge(blogPost);
        }

        public BlogPostEntity Get(string rowKey, string partitionKey)
        {
            return _blogStore.Get(rowKey, partitionKey);
        }

    }
}