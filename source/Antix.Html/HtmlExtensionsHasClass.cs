using System;
using System.Collections.Generic;
using System.Linq;

namespace Antix.Html
{
    public static class HtmlExtensionsHasClass
    {
        public static IEnumerable<IHtmlNode> HasClass(
            this IEnumerable<IHtmlNode> nodes,
            string value)
        {
            if (nodes == null) throw new ArgumentNullException("nodes");

            var found = new List<IHtmlNode>();
            nodes.Search(n => from attr in n.Attributes
                where attr.Name.Equals("class", StringComparison.OrdinalIgnoreCase)
                      && !string.IsNullOrWhiteSpace(attr.Value)
                let classes = attr.Value.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries)
                where classes.Any(c => c.Equals(value, StringComparison.OrdinalIgnoreCase))
                select n, found);

            return found;
        }
    }
}