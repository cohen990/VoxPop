namespace Site.Models
{
    using System;
    using System.Collections.Generic;
    using Machine.Specifications;
    using Storage.Models;

    class when_creating_blog_model_from_blog_post_entity
    {
        internal static BlogPostEntity entity;
        internal static BlogModel model;

        private Establish context = () => entity = new BlogPostEntity
        {
            Author = "author",
            Content = "content",
            ImageCaption = "image caption",
            ImageUri = new Uri("http://www.image.com"),
            PartitionKey = "author identifier",
            Poll = new Dictionary<string, int>
            {
                { "option+1", 2},
                {"option+2", 3},
                {"option%e3+4", 3},
            },
            RowKey = "blog identifier",
            Title = "article title"
        };

        private Because of = () => model = entity.ToModel();

        private It should_set_author_to_author = () => model.Author.ShouldEqual("author");
        private It should_set_content_to_content = () => model.Content.ShouldEqual("content");
        private It should_set_image_caption_to_image_caption = () => model.ImageCaption.ShouldEqual("image caption");
        private It should_set_image_uri_to_www_image_com =
            () => model.ImageUri.ShouldEqual(new Uri("http://www.image.com"));
        private It should_set_author_identifier_to_author_identifier = () => model.AuthorIdentifier.ShouldEqual("author identifier");
        private It should_set_blog_identifier_to_blog_identifier = () => model.BlogIdentifier.ShouldEqual("blog identifier");
        private It should_set_title_to_article_title = () => model.Title.ShouldEqual("article title");
        private It should_set_3_poll_options = () => model.Poll.Count.ShouldEqual(3);
        private It should_contain_option_1_as_poll_key = () => model.Poll.ContainsKey("option 1");
        private It should_contain_option_comma_4_as_poll_key = () => model.Poll.ContainsKey("option, 4");
    }
}
