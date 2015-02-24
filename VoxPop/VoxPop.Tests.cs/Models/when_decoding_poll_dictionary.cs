namespace Site.Models
{
    using System.Collections.Generic;
    using Machine.Specifications;
    using Services;

    class when_decoding_poll_dictionary
    {
        internal static Dictionary<string, int> poll;
        internal static Dictionary<string, int> result;

        private Establish context = () => poll = new Dictionary<string, int>
        {
            {"option+1", 1},
            {"comma%2c%2c+2", 5},
            {"plus+%2b+minus+-+option", 17},
            {"%22escaped+quotes%22", 27}
        };

        private Because of = () => result = poll.DecodePoll();

        private It should_contain_same_number_of_keys = () => result.Count.ShouldEqual(poll.Count);
        private It should_decode_option_1_as_option_1 = () => result.Keys.ShouldContain("option 1");
        private It should_decode_comma_2 = () => result.Keys.ShouldContain("comma,, 2");
        private It should_decode_plus_minus_option = () => result.Keys.ShouldContain("plus + minus - option");
        private It should_decode_escapes_quotes = () => result.Keys.ShouldContain("\"escaped quotes\"");
    }
}
