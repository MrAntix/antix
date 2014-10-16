using System.Dynamic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Antix.Xml
{
    public class DynamicXml : DynamicObject
    {
        readonly XElement _root;

        DynamicXml(XElement root)
        {
            _root = root;
        }

        public static dynamic Load(Stream stream)
        {
            return new DynamicXml(XDocument.Load(stream).Root);
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = null;

            var nodes = _root.Elements()
                .Where(e => e.Name.LocalName == binder.Name)
                .ToArray();

            if (nodes.Count() > 1)
            {
                result = nodes.Select(n => new DynamicXml(n)).ToList();
                return true;
            }

            var node = nodes.SingleOrDefault();
            if (node == null) return true;

            if (node.HasElements)
            {
                result = new DynamicXml(node);
            }
            else
            {
                result = node.Value;
            }

            return true;
        }
    }
}