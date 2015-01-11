using Antix.Xml;
using Xunit;

namespace Antix.Tests.Xml
{
    public class DynamicXmlTests
    {
        [Fact]
        public void sub_nodes()
        {
            var xml = DynamicXml.Parse("<x><y>hi</y></x>");

            Assert.Equal("hi", xml.y);
        }

        [Fact]
        public void sub_nodes_attriute()
        {
            var xml = DynamicXml.Parse("<x><y z='hi'></y></x>");

            Assert.Equal("hi", xml.y.z);
        }

        [Fact]
        public void attributes()
        {
            var xml = DynamicXml.Parse("<x y='hi'></x>");

            Assert.Equal("hi", xml.y);
        }

        [Fact]
        public void collections()
        {
            var xml = DynamicXml.Parse("<x><y>hi</y><y>ya</y></x>");

            Assert.Equal(2, xml.y.Count);
        }
    }
}