using System;
using System.Collections.Generic;

namespace Antix.Html
{
    public static class HtmlExtensionsQuery
    {
        public static IEnumerable<HtmlElement> Query(
            this IEnumerable<IHtmlNode> nodes, Func<HtmlElement, bool> clause)
        {
            var matches = new List<HtmlElement>();

            nodes.Search(clause, matches);

            return matches;
        }
    }
}