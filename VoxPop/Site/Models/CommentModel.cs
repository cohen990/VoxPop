namespace Site.Models
{
    public class CommentModel
    {
        public string UserId { get; set; }

        public string PollItemKey { get; set; }

        public string VotersComment { get; set; }

        public string BlogPostPartitionKey { get; set; }

        public string BlogPostRowKey { get; set; }
    }
}