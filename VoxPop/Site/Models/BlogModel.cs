namespace Site.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Storage.Models;

    public class BlogModel
    {
        public string Title { get; set; }

        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        public List<string> PollOptions { get; set; }

        [Display(Name="Image Caption")]
        public string ImageCaption { get; set; }

        [Display(Name="Add an Image")]
        public Uri ImageUri { get; set; }
        public Dictionary<string, int> Poll { get; set; }

        public string PartitionKey { get; set; }

        public string RowKey { get; set; }

        public string AuthorId { get; set; }

        public BlogPostEntity AsEntity(Uri imageUri, string userName)
        {
            var entity = new BlogPostEntity(Title, imageUri, Content, PollOptions, ImageCaption, userName);

            return entity;
        }

        public static BlogModel For(BlogPostEntity entity)
        {
            // TODO: Unit test
            return new BlogModel
            {
                ImageCaption = entity.ImageCaption,
                AuthorId = entity.Author,
                Content = entity.Content,
                ImageUri =  entity.ImageUri,
                PartitionKey = entity.PartitionKey,
                Poll = entity.Poll,
                RowKey = entity.RowKey,
                Title = entity.Title
            };
        }

        public BlogModel()
        {
            PollOptions = new List<string>();
        }
    }
}

