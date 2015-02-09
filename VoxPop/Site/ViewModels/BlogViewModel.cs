namespace Site.ViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Models;
    using System;


    public class BlogViewModel
    {
        public string Title { get; set; }

        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        public List<string> PollOptions { get; set; }

        public BlogPostEntity AsEntity(Uri imageUri)
        {
            var entity = new BlogPostEntity(Title, imageUri, Content, PollOptions, ImageCaption);

            return entity;
        }

        public BlogViewModel()
        {
            PollOptions = new List<string>();
        }

        public string ImageCaption { get; set; }
    }
}

