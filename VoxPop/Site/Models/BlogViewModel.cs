﻿namespace Site.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Storage.Models;

    public class BlogViewModel
    {
        public string Title { get; set; }

        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        public List<string> PollOptions { get; set; }

        public string ImageCaption { get; set; }

        public BlogPostEntity AsEntity(Uri imageUri, string userName)
        {
            var entity = new BlogPostEntity(Title, imageUri, Content, PollOptions, ImageCaption, userName);

            return entity;
        }

        public BlogViewModel()
        {
            PollOptions = new List<string>();
        }
    }
}

