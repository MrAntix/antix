using System;
using System.Collections.Generic;
using System.Linq;

namespace Antix.Html
{
    public static class HtmlExtensionsAttr
    {
        /// <summary>
        /// <para>Get the values of the attributes with the given name</para>
        /// </summary>
        public static IEnumerable<string> Attr(
            this IEnumerable<IHtmlNode> nodes,
            string name)
        {
            if (nodes == null) throw new ArgumentNullException("nodes");

            var found = new List<string>();
            nodes.Search(n => from attr in n.Attributes
                where attr.Name.Equals(name, StringComparison.OrdinalIgnoreCase)
                select attr.Value, found);

            return found;
        }

        /// <summary>
        /// <para>Set the attribute on the all nodes passed</para>
        /// </summary>
        public static IEnumerable<IHtmlNode> Attr(
            this IEnumerable<IHtmlNode> nodes,
            string name,
            string value)
        {
            if (nodes == null) throw new ArgumentNullException("nodes");
            var nodesArray = nodes as IHtmlNode[] ?? nodes.ToArray();

            var found = new List<HtmlAttribute>();
            nodesArray.Search(
                n =>
                {
                    var attributes =
                        n.Attributes
                            .Where(attr => attr.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                            .ToArray();

                    if (!attributes.Any())
                    {
                        var newAttribute = new HtmlAttribute
                        {
                            Name = name,
                            Value = value
                        };
                        n.Attributes.Add(newAttribute);
                        attributes = new[] {newAttribute};
                    }
                    else
                    {
                        var first = true;
                        foreach (var attribute in attributes)
                        {
                            if (first)
                            {
                                attribute.Value = value;
                                first = false;
                            }
                            else
                                n.Attributes.Remove(attribute);
                        }
                    }

                    return attributes;
                },
                found);

            return nodesArray;
        }
    }

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