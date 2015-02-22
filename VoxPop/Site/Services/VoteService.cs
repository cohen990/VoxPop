namespace Site.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Storage;
    using Storage.Models;

    public class VoteService : IVoteService
    {
        public async Task<BlogPostEntity> RetrieveVotes(BlogPostEntity entity)
        {
            var voteStore = new TableVoteStore();
            List<VoteEntity> votes = await voteStore.GetAllForBlogAsync(entity.RowKey);

            foreach (var key in entity.Poll.Keys.ToList())
            {
                string pollOptionKey = key;
                entity.Poll[pollOptionKey] = votes.Count(x => x.PollOptionKey == pollOptionKey);
            }

            return entity;
        }
    }
}