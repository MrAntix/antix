using Antix.Html;
using Xunit;

namespace Antix.Tests.Html
{
    public class ParseAttributeTests
    {
        [Fact]
        public void double_quotes()
        {
            Exec("anAttribute=\"aValue\" anotherAttribute");
        }

        [Fact]
        public void single_quotes()
        {
            Exec("anAttribute='aValue' anotherAttribute");
        }

        [Fact]
        public void no_quotes()
        {
            Exec("anAttribute='aValue' anotherAttribute");
        }

        [Fact]
        public void mixed_quotes()
        {
            Exec("anAttribute='aValue\" anotherAttribute", expectedValue: "aValue\" anotherAttribute");
        }

        [Fact]
        public void white_space_before_the_equals()
        {
            Exec("anAttribute  =\"aValue\" anotherAttribute");
        }

        [Fact]
        public void white_space_after_the_equals()
        {
            Exec("anAttribute=  \"aValue\" anotherAttribute");
        }

        [Fact]
        public void switched_quotes_are_ignored()
        {
            Exec("anAttribute=\"aV\\\"alue\" anotherAttribute", expectedValue: "aV\"alue");
        }

        [Fact]
        public void will_not_consume_tag_closer_without_quotes()
        {
            Exec("anAttribute=aValue>");
        }

        [Fact]
        public void will_consume_tag_closer_with_quotes()
        {
            Exec("anAttribute='aValue>'>", expectedValue: "aValue>");
        }

        static void Exec(string html,
                         string expectedName = "anAttribute", string expectedValue = "aValue")
        {
            var result = HtmlParser.ParseAttribute(new HtmlReader(html));

            Assert.NotNull(result);
            Assert.Equal(expectedName, result.Name);
            Assert.Equal(expectedValue, result.Value);
        }

        [Fact]
        public void empty_string_gives_null()
        {
            ExpectNull(string.Empty);
        }

        [Fact]
        public void whitespace_gives_null()
        {
            ExpectNull(string.Empty);
        }

        static void ExpectNull(string html)
        {
            var result = HtmlParser.ParseAttribute(new HtmlReader(html));

            Assert.Null(result);
        }

        [Fact]
        public void no_value_does_not_effect_next_attribute()
        {
            const string value = "anAttribute anotherAttribute";

            var reader = new HtmlReader(value);

            var result = HtmlParser.ParseAttribute(reader);
            Assert.NotNull(result);
            Assert.Equal("anAttribute", result.Name);
            Assert.Equal(null, result.Value);

            result = HtmlParser.ParseAttribute(reader);
            Assert.NotNull(result);
            Assert.Equal("anotherAttribute", result.Name);
            Assert.Equal(null, result.Value);
        }
    }
}