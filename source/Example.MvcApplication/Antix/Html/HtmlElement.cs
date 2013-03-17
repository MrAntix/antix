using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Antix.Html
{
    public class HtmlElement : IHtmlNode
    {
        static readonly string[] HtmlInlineElements;
        static readonly string[] HtmlNonContainers;

        static HtmlElement()
        {
            HtmlNonContainers = HtmlSettings.Default.HtmlNonContainers.Split(',');
            HtmlInlineElements = HtmlSettings.Default.HtmlInlineElements.Split(',');
        }

        readonly List<HtmlAttribute> _attributes;
        readonly List<IHtmlNode> _children;
        string _name;

        internal HtmlElement(
            string name)
        {
            Name = name;
            IsClosed = IsNonContainer;

            _attributes = new List<HtmlAttribute>();
            _children = new List<IHtmlNode>();
        }

        public HtmlElement() : this(null)
        {
        }

        public string Name
        {
            get{return _name;}
            set
            {
                Debug.Assert(value != null, "value != null");
                _name = value.ToLower();
            }
        }

        public bool IsClosed { get; set; }

        public List<HtmlAttribute> Attributes
        {
            get { return _attributes; }
        }

        public List<IHtmlNode> Children
        {
            get { return _children; }
        }

        public bool IsNonContainer
        {
            get { return HtmlNonContainers.Contains(Name); }
        }

        public bool IsInline
        {
            get { return HtmlInlineElements.Contains(Name); }
        }

        public void ToString(StringBuilder output)
        {
            output.Append("<");
            output.Append(Name);

            foreach (var item in Attributes)
            {
                output.Append(" ");
                item.ToString(output);
            }

            if (IsNonContainer || IsClosed)
            {
                output.Append("/>");
                return;
            }

            output.Append(">");
            foreach (var item in Children)
            {
                item.ToString(output);
            }

            output.Append("</");
            output.Append(Name);
            output.Append(">");
        }

        public override string ToString()
        {
            var output = new StringBuilder();
            ToString(output);

            return output.ToString();
        }
    }
}