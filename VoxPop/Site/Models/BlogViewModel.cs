namespace Site.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Services;

    public class BlogViewModel
    {
        public string Title { get; set; }

        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        public List<string> PollOptions { get; set; }

        public BlogEntity AsEntity()
        {
            throw new System.NotImplementedException();
        }
    }
}