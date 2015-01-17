namespace Site.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface IBlogService
    {
        Task CreateAsync(BlogViewModel blog);
        IEnumerable<BlogPostEntity> GetAll();

        Task VoteAsync(BlogViewModel blog, string pollOption);
    }
}