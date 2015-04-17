namespace Site.Models
{
    public class VoteModel
    {
        public string UserId { get; set; }

        public string PollItemKey { get; set; }

        //public string UserComment { get; set; }

        public string BlogPostPartitionKey { get; set; }

        public string BlogPostRowKey { get; set; }
    }
}