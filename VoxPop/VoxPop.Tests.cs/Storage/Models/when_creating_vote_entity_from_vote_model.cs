namespace Site.Storage.Models
{
    using Machine.Specifications;
    using Site.Models;

    class when_creating_vote_entity_from_vote_model
    {
        internal static VoteModel model;
        internal static VoteEntity result;

        private Establish context = () => model = new VoteModel
        {
            BlogPostPartitionKey = "blog post partition key",
            BlogPostRowKey = "blog post row key",
            PollItemKey = "poll item key",
            UserId = "user id"
        };

        private Because of = () => result = VoteEntity.For(model);

        private It should_set_partition_key_to_blog_post_row_key =
            () => result.PartitionKey.ShouldEqual("blog post row key");

        private It should_set_row_key_to_user_id = () => result.RowKey.ShouldEqual("user id");
        private It should_set_poll_option_key_to_poll_item_key = () => result.PollOptionKey.ShouldEqual("poll+item+key");
    }
}
