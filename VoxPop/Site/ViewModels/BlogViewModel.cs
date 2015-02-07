namespace Site.ViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Models;


    public class BlogViewModel
    {
        public string Title { get; set; }

        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        public List<string> PollOptions { get; set; }

        public BlogPostEntity AsEntity(string image)
        {
            var entity = new BlogPostEntity(Title, image, Content, PollOptions);

            return entity;
        }

        public BlogViewModel()
        {
            PollOptions = new List<string>();
        }
    }
}

