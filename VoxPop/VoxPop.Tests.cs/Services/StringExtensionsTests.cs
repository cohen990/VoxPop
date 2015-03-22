namespace Site.Services
{
    using Machine.Specifications;

    public class StringExtensionsTestsContext
    {
        internal static string input;
        internal static string result;

        Because of = () => result = input.StripHtmlTags();
    }

    public class when_stripping_empty_string : StringExtensionsTestsContext
    {
        Establish context = () => input = string.Empty;

        It should_return_empty_string = () => result.ShouldEqual(string.Empty);
    }

    public class when_stripping_tags_from_string_without_tags : StringExtensionsTestsContext
    {
        Establish content = () => input = "plain text string";

        It should_return_the_same_string = () => result.ShouldEqual(input);
    }

    public class when_stripping_tags_from_string_with_p_tags : StringExtensionsTestsContext
    {
        Establish context = () => input = "<p>I am a dog</p>";

        It should_return_string_without_tags = () => result.ShouldEqual("I am a dog");
    }

    public class when_stripping_tags_from_string_with_multiple_tags : StringExtensionsTestsContext
    {
        Establish context = () => input =
            "<html>" +
                "<head>" +
                    "<meta property=\"og:title\" content=\"Got questions? Get Answers.\" />" +
                "</head>" +
                "<body>" +
                    "<div>I am a dog</div>" +
                "</body>" +
            "</html>";

        It should_return_I_am_a_dog = () => result.ShouldEqual("I am a dog");
    }
}
