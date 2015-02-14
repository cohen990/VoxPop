namespace Site.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;
    using Storage;
    using Storage.Models;
    using ViewModels;

    public interface IBlogService
    {
        Task CreateAsync(BlogViewModel blog, Stream imageStream);

        IEnumerable<BlogPostEntity> GetAll();

        void Vote(VoteModel model);

        BlogPostEntity Get(string rowKey, string partitionKey);
    }
}