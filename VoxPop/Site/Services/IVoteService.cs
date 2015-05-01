namespace Site.Services
{
    using System.Threading.Tasks;
    using Storage.Models;

    public interface IVoteService
    {
        Task<BlogPostEntity> RetrieveVotes(BlogPostEntity entity);

        Task<ResponseEntity> RetrieveVotes(ResponseEntity entity);
    }
}