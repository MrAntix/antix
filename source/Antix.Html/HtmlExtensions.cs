using System;
using System.Collections.Generic;
using System.Linq;
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

        public static string ToHtml(
            this IEnumerable<IHtmlNode> nodes)
        {
            var output = new StringBuilder();

            nodes.ToHtml(output);

            return output.ToString().Trim();
        }

        public static void ToHtml(
            this IEnumerable<IHtmlNode> nodes, StringBuilder output)
        {
            foreach (var node in nodes)
                node.ToString(output);
        }

        public static IEnumerable<HtmlElement> Query(
            this IEnumerable<IHtmlNode> nodes, Func<HtmlElement, bool> clause)
        {
            var matches = new List<HtmlElement>();

            Query(nodes, clause, matches);

            return matches;
        }

        static void Query(
            this IEnumerable<IHtmlNode> nodes, Func<HtmlElement, bool> clause,
            ICollection<HtmlElement> matches)
        {
            foreach (var node in nodes.OfType<HtmlElement>())
            {
                if (clause(node))
                    matches.Add(node);

                Query(node.Children, clause, matches);
            }
        }
    }
}