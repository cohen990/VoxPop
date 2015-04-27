//namespace Site.Models
//{

namespace Site.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class CommentModel
    {
        public string UserId { get; set; }

        public string CommenterName { get; set; }

        public string PollItemKey { get; set; }

        public int PollItemIndex { get; set; }

        public string VotersComment { get; set; }

        public string CommentIdentifier { get; set; }

        public bool ReplyYayOrNay { get; set; }

        public string RepliedTo { get; set; }

        public string RepliedToUN { get; set; }

        public string CommentPic { get; set; }

        public string BlogPostPartitionKey { get; set; }

        public string BlogPostRowKey { get; set; }

        public string CommentTimestamp { get; set; }

    }
}