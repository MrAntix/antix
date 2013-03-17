using System;
using System.Drawing;
using Antix.Drawing;
using Xunit;

namespace Antix.Tests.Drawing
{
    public class FitCropTest
    {
        [Fact]
        public void when_smaller_width_ratio_same_size()
        {
            var original = new Size(200, 100);
            var max = new Size(10, 10);

            var result = original.FitCrop(max);
            Console.Write(result);

            Assert.Equal(50, result.X);
            Assert.Equal(0, result.Y);
            Assert.Equal(100, result.Width);
            Assert.Equal(100, result.Height);
        }

        [Fact]
        public void when_smaller_width_ratio()
        {
            var original = new Size(200, 100);
            var max = new Size(1, 1);

            var result = original.FitCrop(max);
            Console.Write(result);

            Assert.Equal(50, result.X);
            Assert.Equal(0, result.Y);
            Assert.Equal(100, result.Width);
            Assert.Equal(100, result.Height);
        }

        [Fact]
        public void when_same_ratio()
        {
            var original = new Size(200, 100);
            var max = new Size(2, 1);

            var result = original.FitCrop(max);
            Console.Write(result);

            Assert.Equal(0, result.X);
            Assert.Equal(0, result.Y);
            Assert.Equal(200, result.Width);
            Assert.Equal(100, result.Height);
        }

        [Fact]
        public void when_bigger_width_ratio()
        {
            var original = new Size(200, 100);
            var max = new Size(3, 1);

            var result = original.FitCrop(max);
            Console.Write(result);

            Assert.Equal(0, result.X);
            Assert.Equal(17, result.Y);
            Assert.Equal(200, result.Width);
            Assert.Equal(67, result.Height);
        }
    }
}