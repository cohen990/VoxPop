namespace Site.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface IBlogService
    {
        void CreateAsync(BlogViewModel blog);
        Task<IEnumerable<BlogEntity>> GetAllAsync();
    }
}