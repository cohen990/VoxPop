using System.ComponentModel.DataAnnotations;

namespace Site.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;


    public class ResponseModel
    {
        [Required(ErrorMessage = "Your Story needs a title...!")]
        public string Title { get; set; }

        [DataType(DataType.MultilineText), AllowHtml]
        [Required]
        public string Content { get; set; }

        public List<string> PollOptions { get; set; }

        [Display(Name = "Image Caption")]
        [Required(ErrorMessage = "Looks like you skipped adding an Image Caption")]
        public string ImageCaption { get; set; }

        [Display(Name = "Image")]
        public Uri ImageUri { get; set; }

        public Dictionary<string, int> Poll { get; set; }

        public string BlogIdentifier { get; set; }

        public string Author { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime TimeCreated { get; set; }

        public string AuthorIdentifier { get; set; }

        public string ReplyeeTitle { get; set; }

        public string Replyee { get; set; }

        public string ReplyeeRowKey { get; set; }

        public string ReplyeePartitionKey { get; set; }

        public ResponseModel()
        {
            PollOptions = new List<string>();
        }
    }
}

