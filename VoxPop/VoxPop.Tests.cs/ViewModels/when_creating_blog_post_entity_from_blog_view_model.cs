namespace Site.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Machine.Specifications;
    using Storage;
    using Storage.Models;

    class when_creating_blog_post_entity_from_blog_view_model
    {
        internal static BlogViewModel model;

        internal static BlogPostEntity result;

        internal static string content;

        internal static string title;

        internal static List<string> pollOptions;

        internal static Uri imageUrl;

        private Establish context = () =>
        {
            content = "placeholder content";
            title = "Placeholder Title";
            pollOptions = new List<string> {"poll option 1", "poll option 2", "poll option 3"};
            imageUrl = new Uri("http://www.blobstorage.com/image.png");

            model =
                    new BlogViewModel
                    {
                        Content = content,
                        Title = title,
                        PollOptions = pollOptions
                    };
        };

        private Because of = () => result = model.AsEntity(imageUrl);

        private It should_have_the_same_content = () => result.BlogContent.ShouldEqual(content);

        private It should_have_the_same_title = () => result.BlogTitle.ShouldEqual(title);

        private It should_contain_the_first_poll_option = () => result.Poll.Keys.ShouldContain(pollOptions.First());

        private It should_contain_the_last_poll_option = () => result.Poll.Keys.ShouldContain(pollOptions.Last());

        private It should_initialize_all_poll_options_to_zero_votes =
            () => result.Poll.All(x => x.Value == 0).ShouldBeTrue();

        private It should_have_the_same_encoded_image = () => result.BlogImageUri.ShouldEqual(imageUrl);
    }
}
