namespace Site.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    class BlogService : IBlogService
    {
        private readonly IStore<BlogPostEntity> _blogStore;

        public BlogService()
        {
            _blogStore = new TableStore<BlogPostEntity>();
        }

        public async Task CreateAsync(BlogViewModel blog)
        {
            blog.PollOptions = new List<string> {"option1", "option2", "nick fury"};

            var blogEntity = blog.AsEntity();

            await _blogStore.CreateAsync(blogEntity);
        }

        public IEnumerable<BlogPostEntity> GetAll()
        {
            return _blogStore.GetAll();
        }
    }
}