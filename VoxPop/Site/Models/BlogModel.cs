namespace Site.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Services;
    using Storage.Models;

    public class BlogModel
    {
        public string Title { get; set; }

        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        public List<string> PollOptions { get; set; }

        public string ImageCaption { get; set; }

        public Uri ImageUri { get; set; }

        public Dictionary<string, int> Poll { get; set; }

        public string PartitionKey { get; set; }

        public string RowKey { get; set; }

        public string Author { get; set; }

        public BlogModel()
        {
            PollOptions = new List<string>();
        }
    }
}

