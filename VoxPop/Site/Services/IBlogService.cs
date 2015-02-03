namespace Site.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface IBlogService
    {
        Task CreateAsync(BlogViewModel blog, string Image);

        IEnumerable<BlogPostEntity> GetAll();

        void Vote(VoteModel model);

        BlogPostEntity Get(string rowKey, string partitionKey);
    }
}