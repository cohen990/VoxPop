namespace Site.Models
{
    using Machine.Specifications;
    using System.Collections.Generic;
    using Services;

    class when_encoding_poll_options_list
    {
        internal static List<string> pollOptions;
        internal static List<string> result;

        private Establish context = () => pollOptions = new List<string>
        {
            "option 1",
            "comma,, 2",
            "plus + minus - option",
            "\"escaped quotes\""
        };

        private Because of = () => result = pollOptions.EncodePollOptions();

        private It should_return_same_number_of_options = () => result.Count.ShouldEqual(pollOptions.Count);
        private It should_encode_option_1_as_option_plus_1 = () => result.ShouldContain("option+1");
        private It should_encode_comma_2 = () => result.ShouldContain("comma%2c%2c+2");
        private It should_encode_plus_minus_option = () => result.ShouldContain("plus+%2b+minus+-+option");
        private It should_encode_escaped_quotes = () => result.ShouldContain("%22escaped+quotes%22");
    }
}
