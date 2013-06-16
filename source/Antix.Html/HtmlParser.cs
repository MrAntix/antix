using System.Collections.Generic;
using System.Linq;

namespace Antix.Html
{
    public class HtmlParser
    {
        public static readonly HtmlParser Instance = new HtmlParser();

        public IEnumerable<IHtmlNode> Parse(string html)
        {
            return ParseElements(new HtmlQueue(html));
        }

        public HtmlAttribute ParseAttribute(HtmlQueue html)
        {
            var name = ParseAttributeName(html);
            if (string.IsNullOrWhiteSpace(name)) return null;

            return new HtmlAttribute
                {
                    Name = name,
                    Value = ParseAttributeValue(html)
                };
        }

        static string ParseAttributeName(HtmlQueue html)
        {
            var word = new List<char>();
            var hasWhitespace = false;

            while (html.Any())
            {
                var c = html.Peek();
                if (char.IsWhiteSpace(c))
                {
                    hasWhitespace = word.Any();
                    html.Consume();
                    continue;
                }
                if (!char.IsLetterOrDigit(c)
                    || hasWhitespace) break;

                html.Consume();
                word.Add(c);
            }

            return string.Join("", word);
        }

        static string ParseAttributeValue(HtmlQueue html)
        {
            if (html.Peek() != '=') return null;

            html.Consume();

            var word = new List<char>();
            var quote = (char) 0;
            var switched = false;

            while (html.Any())
            {
                var c = html.Peek();
                if (char.IsWhiteSpace(c)
                    || c == '>')
                {
                    if (quote == 0)
                    {
                        html.Consume();
                        if (word.Any()) break;

                        continue;
                    }
                }
                else if (c == '\'' || c == '"')
                {
                    if (!switched)
                    {
                        if (quote == c)
                        {
                            html.Consume();
                            break;
                        }

                        if (quote == 0)
                        {
                            quote = c;
                            html.Consume();
                            continue;
                        }
                    }
                }
                else if (c == '\\' && !switched)
                {
                    switched = true;
                    html.Consume();
                    continue;
                }
                else
                {
                    switched = false;
                }

                html.Consume();
                word.Add(c);
            }

            return string.Join("", word);
        }

        public HtmlElement ParseElement(HtmlQueue html)
        {
            html.ConsumeWhiteSpace();

            var name = ParseElementOpener(html);
            if (string.IsNullOrWhiteSpace(name)) return null;
            var element = new HtmlElement(name);

            var attributes = ParseAttributes(html);
            element.Attributes.AddRange(attributes);

            html.ConsumeWhiteSpace();

            if (html.TryConsume("/>"))
            {
                element.IsClosed = true;
                return element;
            }

            html.TryConsume('>');
            if (element.IsTextOnlyContainer)
            {
                var textElement = ParseTextElement(html, string.Concat("</", name, ">"));
                element.Children.Add(textElement);
            }
            else if (!element.IsNonContainer)
            {
                var children = ParseElements(html);
                element.Children.AddRange(children);
            }

            TryConsumeElementCloser(name, html);

            return element;
        }

        IEnumerable<IHtmlNode> ParseElements(HtmlQueue html)
        {
            var items = new List<IHtmlNode>();

            IHtmlNode item;
            while ((item = ParseElement(html)
                           ?? ParseTextElement(html)) != null)
            {
                items.Add(item);
            }

            return items;
        }

        static IHtmlNode ParseTextElement(HtmlQueue html)
        {
            string text;

            if (!html.TryConsume(c => c == '<', true, false, out text)
                || text == string.Empty) return null;

            return new HtmlTextElement {Value = CollapseWhiteSpace(text)};
        }

        static IHtmlNode ParseTextElement(HtmlQueue html, string upto)
        {
            string text;

            if (!html.TryConsume(upto, true, false, out text)
                || text == string.Empty) return null;

            return new HtmlTextElement { Value = CollapseWhiteSpace(text) };
        }

        static string CollapseWhiteSpace(string text)
        {
            if (text == null
                || text.Length < 2) return text;

            return string.Concat(
                char.IsWhiteSpace(text.First()) ? " " : "",
                text.Trim(),
                char.IsWhiteSpace(text.Last()) ? " " : ""
                );
        }

        IEnumerable<HtmlAttribute> ParseAttributes(HtmlQueue html)
        {
            var items = new List<HtmlAttribute>();

            HtmlAttribute item;
            while ((item = ParseAttribute(html)) != null)
            {
                items.Add(item);
            }

            return items;
        }

        static string ParseElementOpener(HtmlQueue html)
        {
            if (html.Peek() != '<') return null;

            if (string.Join("", html.Peek(2)) == "</") return null;
            html.Consume();

            var word = new List<char>();
            while (html.Any())
            {
                var c = html.Peek();
                if (!char.IsLetterOrDigit(c)
                    && c != '!'
                    && c != '-') break;

                html.Consume();
                word.Add(c);
            }

            return string.Join("", word);
        }

        static void TryConsumeElementCloser(string name, HtmlQueue html)
        {
            var closer = string.Format("</{0}>", name);

            html.TryConsume(closer);
        }

        static readonly string[] InlineElements;
        static readonly string[] NonContainers;
        static readonly string[] NonClosers;
        static readonly string[] TextOnlyContainers;

        static HtmlParser()
        {
            InlineElements = HtmlSettings.Default.HtmlInlineElements.Split(',');
            NonContainers = HtmlSettings.Default.HtmlNonContainers.Split(',');
            NonClosers = HtmlSettings.Default.HtmlNonClosers.Split(',');
            TextOnlyContainers = HtmlSettings.Default.HtmlTextOnlyContainers.Split(',');
        }

        public static bool IsNonCloser(string name)
        {
            return NonClosers.Contains(name);
        }

        public static bool IsNonContainer(string name)
        {
            return NonContainers.Contains(name)
                   || IsNonCloser(name);
        }

        public static bool IsInline(string name)
        {
            return InlineElements.Contains(name);
        }

        public static bool IsTextOnlyContainer(string name)
        {
            return TextOnlyContainers.Contains(name);
        }
    }
}