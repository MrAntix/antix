using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Antix.Html
{
    public static class HtmlExtensions
    {
        public static string ToText(
            this IEnumerable<IHtmlNode> nodes)
        {
            var output = new StringBuilder();

            nodes.ToText(output);

            return output.ToString().Trim();
        }

        public static void ToText(
            this IEnumerable<IHtmlNode> nodes, StringBuilder output)
        {
            foreach (var node in nodes)
            {
                node.ToText(output);
            }
        }

        public static void ToText(
            this IHtmlNode node, StringBuilder output)
        {
            var textElement = node as HtmlTextElement;
            if (textElement != null)
                output.Append(
                    WebUtility.HtmlDecode(textElement.Value)
                    );

            else
            {
                var htmlElement = node as HtmlElement;
                if (htmlElement != null)
                {
                    if (htmlElement.Name == "br")
                    {
                        output.Append("\n");
                    }
                    else
                    {
                        if (!htmlElement.IsInline)
                            output.Append("\n");

                        htmlElement.Children.ToText(output);
                    }
                }
            }
        }

        public static string ToString(
            this IEnumerable<IHtmlNode> nodes)
        {
            var output = new StringBuilder();

            nodes.ToString(output);

            return output.ToString().Trim();
        }

        public static void ToString(
            this IEnumerable<IHtmlNode> nodes, StringBuilder output)
        {
            foreach (var node in nodes)
                node.ToString(output);
        }
    }
}