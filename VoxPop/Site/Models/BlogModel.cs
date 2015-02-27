namespace Site.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Services;
    using Storage.Models;
    using System.Web.Mvc;


    public class BlogModel
    {
        public string Title { get; set; }

        [DataType(DataType.MultilineText), AllowHtml]
        public string Content { get; set; }


        public List<string> PollOptions { get; set; }

        [Display(Name="Image Caption")]
        public string ImageCaption { get; set; }

        [Display(Name="Add an Image")]
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

