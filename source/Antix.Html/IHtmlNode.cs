using System.Text;

namespace Antix.Html
{
    public interface IHtmlNode
    {
        void ToString(StringBuilder output);
    }
}