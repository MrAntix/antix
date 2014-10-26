using Antix.Xml;
using Xunit;

namespace Antix.Tests.Xml
{
    public class DynamicXmlTests
    {

        [Fact]
        public void sub_nodes()
        {
            dynamic xml = DynamicXml.Parse("<x><y>hi</y></x>");

            Assert.Equal("hi", xml.y);
        }
        [Fact]
        public void attributes()
        {
            dynamic xml = DynamicXml.Parse("<x y='hi'></x>");

            Assert.Equal("hi", xml.y);
        }
        [Fact]
        public void collections()
        {
            dynamic xml = DynamicXml.Parse("<x><y>hi</y><y>ya</y></x>");

            Assert.Equal(2, xml.y.Count);
        }
    }
}