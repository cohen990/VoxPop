namespace Site.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Machine.Specifications;
    using Models;
    using Storage.Models;

    class when_creating_blog_post_entity_from_blog_view_model
    {
        internal static BlogModel model;

        internal static BlogPostEntity result;

        internal static string content;

        internal static string title;

        internal static List<string> pollOptions;

        internal static Uri imageUrl;

        internal static string authorId;

        internal static string imageCaption;

        private Establish context = () =>
        {
            content = "placeholder content";
            title = "Placeholder Title";
            pollOptions = new List<string> {"poll option 1", "poll option 2", "poll option 3"};
            imageUrl = new Uri("http://www.blobstorage.com/image.png");
            authorId = "user@name.com";
            imageCaption = "image caption";
            model =
                    new BlogModel
                    {
                        Content = content,
                        Title = title,
                        PollOptions = pollOptions,
                        ImageUri = imageUrl,
                        AuthorId = authorId,
                        ImageCaption = imageCaption
                    };
        };

        private Because of = () => result = BlogPostEntity.For(model);
        private It should_have_the_same_content = () => result.Content.ShouldEqual(content);
        private It should_have_the_same_title = () => result.Title.ShouldEqual(title);
        private It should_contain_the_first_poll_option_encoded =
            () => result.Poll.Keys.ShouldContain(pollOptions.First().Replace(' ', '+'));
        private It should_contain_the_last_poll_option =
            () => result.Poll.Keys.ShouldContain(pollOptions.Last().Replace(' ', '+'));
        private It should_initialize_all_poll_options_to_zero_votes =
            () => result.Poll.All(x => x.Value == 0).ShouldBeTrue();
        private It should_have_the_same_encoded_image = () => result.ImageUri.ShouldEqual(imageUrl);
        private It should_have_the_same_author_id = () => result.Author.ShouldEqual(authorId);
        private It should_have_same_image_caption = () => result.ImageCaption.ShouldEqual(imageCaption);
    }
}
