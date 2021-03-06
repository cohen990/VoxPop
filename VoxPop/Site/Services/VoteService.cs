﻿namespace Site.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Storage;
    using Storage.Models;

    public class VoteService : IVoteService
    {
        private readonly IVoteStore _voteStore;

        public VoteService(IVoteStore voteStore)
        {
            _voteStore = voteStore;
        }

        public async Task<BlogPostEntity> RetrieveVotes(BlogPostEntity entity)
        {
            if (entity != null)
            {
                // TODO: Add unit tests
                List<VoteEntity> votes = await _voteStore.GetAllForBlogAsync(entity.RowKey);

                foreach (var key in entity.Poll.Keys.ToList())
                {
                    string pollOptionKey = key;
                    entity.Poll[pollOptionKey] = votes.Count(x => x.PollOptionKey == pollOptionKey);
                }
            }
            return entity;
        }

        public async Task<ResponseEntity> RetrieveVotes(ResponseEntity entity)
        {
            if (entity != null)
            {
                // TODO: Add unit tests
                List<VoteEntity> votes = await _voteStore.GetAllForBlogAsync(entity.RowKey);

                foreach (var key in entity.Poll.Keys.ToList())
                {
                    string pollOptionKey = key;
                    entity.Poll[pollOptionKey] = votes.Count(x => x.PollOptionKey == pollOptionKey);
                }
            }
                return entity;
        }

        public async Task<CommentEntity> RetrieveVotes(CommentEntity entity)
        {
            if (entity != null)
            {
                // TODO: Add unit tests
                List<VoteEntity> votes = await _voteStore.GetAllForBlogAsync(entity.RowKey);

                foreach (var key in entity.Poll.Keys.ToList())
                {
                    string pollOptionKey = key;
                    entity.Poll[pollOptionKey] = votes.Count(x => x.PollOptionKey == pollOptionKey);
                }
            }
            return entity;
        }

        //public async Task<VoteEntity> RetrieveVotes(VoteEntity entity)
        //{
        //    if (entity != null)
        //    {
        //        // TODO: Add unit tests
        //        List<VoteEntity> votes = await _voteStore.GetAllForBlogAsync(entity.RowKey);
        //    }
        //    return entity;
        //}



    }


}