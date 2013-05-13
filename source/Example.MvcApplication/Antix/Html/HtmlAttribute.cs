using System.Text;

namespace Antix.Html
{
    public class HtmlAttribute : IHtmlNode
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public void ToString(StringBuilder output)
        {
            if (Value == null)
                output.Append(Name);

            else
            {
                var quoteSwitch =
                    "\\" + HtmlSettings.Default.HtmlAttributeQuote;
                var value = Value.Replace(HtmlSettings.Default.HtmlAttributeQuote, quoteSwitch);

                output.Append(string.Format(
                    "{0}={1}{2}{1}",
                    Name,
                    HtmlSettings.Default.HtmlAttributeQuote,
                    value));
            }
        }

        public override string ToString()
        {
            var output = new StringBuilder();
            ToString(output);

            return output.ToString();
        }
    }
}