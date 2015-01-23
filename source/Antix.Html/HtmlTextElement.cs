using System.Text;

namespace Antix.Html
{
    public class HtmlTextElement : IHtmlNode
    {
        public string Value { get; set; }

        public void ToString(StringBuilder output)
        {
            output.Append(Value);
        }

        public override string ToString()
        {
            return Value;
        }
    }
}