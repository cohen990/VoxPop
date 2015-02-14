namespace Site.Storage.Models
{
    public class VoteModel
    {
        public string PollItemKey { get; set; }

        public string BlogPostPartitionKey { get; set; }

        public string BlogPostRowKey { get; set; }
    }
}