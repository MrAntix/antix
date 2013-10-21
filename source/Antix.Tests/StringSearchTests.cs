using Xunit;

namespace Antix.Tests
{
    public class StringSearchTests
    {
        [Fact]
        public void CanFindAtBegining()
        {
            const string toSearch = "ABCD";

            var search = new StringSearch("AB");

            search.Execute(toSearch);

            Assert.True(search.IsFound);
            Assert.Equal(0, search.Start);
        }

        [Fact]
        public void CanFindInMiddle()
        {
            const string toSearch = "ABCD";

            var search = new StringSearch("BC");

            search.Execute(toSearch);

            Assert.True(search.IsFound);
            Assert.Equal(1, search.Start);
        }

        [Fact]
        public void CanFindAtEnd()
        {
            const string toSearch = "ABCD";

            var search = new StringSearch("CD");

            search.Execute(toSearch);

            Assert.True(search.IsFound);
            Assert.Equal(2, search.Start);
        }


        [Fact]
        public void CanFindBetween()
        {
            const string toSearch1 = "ABCD";
            const string toSearch2 = "EFGH";

            var search = new StringSearch("CDEFGH");

            search.Execute(toSearch1);

            Assert.False(search.IsFound);
            Assert.Equal(2, search.Start);
            Assert.Equal(2, search.Index);

            search.Execute(toSearch2);

            Assert.True(search.IsFound);
            Assert.Equal(-2, search.Start);
        }
    }
}