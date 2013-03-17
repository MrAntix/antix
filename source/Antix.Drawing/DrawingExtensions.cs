using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Threading.Tasks;

namespace Antix.Drawing
{
    public static class DrawingExtensions
    {
        /// <summary>
        ///     <para>Convert an image into a byte array</para>
        /// </summary>
        public static byte[] ToBytes(
            this Image image)
        {
            using (var memoryStream = new MemoryStream())
            {
                image.Save(memoryStream, image.RawFormat);
                return memoryStream.ToArray();
            }
        }

        /// <summary>
        ///     <para>Transform an image or part of, to the given dimensions</para>
        /// </summary>
        public static async Task<byte[]> TransformAsync(
            this Image image, Size size, ImageTransformOptions options)
        {
            var crop = options.Crop.Equals(Rectangle.Empty)
                           ? new Rectangle(0, 0, image.Size.Width, image.Size.Height)
                           : options.Crop;

            var newSize = size.Equals(Size.Empty)
                              ? crop.Size
                              : crop.Size.Constrain(size);

            return await Task.Run(() =>
                {
                    using (var newImage = (Image) new Bitmap(newSize.Width, newSize.Height))
                    using (var gfx = Graphics.FromImage(newImage))
                    {
                        gfx.SmoothingMode = SmoothingMode.HighQuality;
                        gfx.CompositingQuality = CompositingQuality.HighQuality;
                        gfx.CompositingMode = CompositingMode.SourceCopy;
                        gfx.InterpolationMode = InterpolationMode.HighQualityBicubic;

                        gfx.Clear(Color.Transparent);

                        gfx.DrawImage(image,
                                      new Rectangle(0, 0, newSize.Width, newSize.Height),
                                      crop,
                                      GraphicsUnit.Pixel);

                        using (var memStream = new MemoryStream())
                        {
                            newImage.Save(memStream, options.GetEncoder(), options.GetEncoderParams());

                            return memStream.ToArray();
                        }
                    }
                });
        }

        /// <summary>
        ///     <para>Zoom a rectangle by a given factor</para>
        /// </summary>
        public static Rectangle Zoom(this Rectangle original, decimal factor)
        {
            return new Rectangle(
                (int) Math.Round(original.X*factor),
                (int) Math.Round(original.Y*factor),
                (int) Math.Round(original.Width*factor),
                (int) Math.Round(original.Height*factor));
        }

        /// <summary>
        ///     <para>Restrict a size to the maximum</para>
        /// </summary>
        /// <param name="original"> Original Size </param>
        /// <param name="max"> Maximum Size </param>
        /// <returns> Restricted Size </returns>
        public static Size Constrain(this Size original, Size max)
        {
            double ratio = 1;

            // check the height
            if (original.Height > max.Height && max.Height > 0)
            {
                // too tall, get a ratio to shrink
                ratio = max.Height/(double) original.Height;
            }
            // check the width
            if (original.Width > max.Width && max.Width > 0)
            {
                // too wide, check ratio is smaller than current
                var ratioWidth = max.Width/(double) original.Width;
                if (ratioWidth < ratio)
                {
                    ratio = ratioWidth;
                }
            }

            return new Size((int) Math.Round(original.Width*ratio), (int) Math.Round(original.Height*ratio));
        }

        public static Rectangle FitCrop(this Size original, Size max)
        {
            var ratio = Math.Min(
                original.Width/(double) max.Width,
                original.Height/(double) max.Height);

            return new Rectangle(
                (int) Math.Round((original.Width - max.Width*ratio)/2),
                (int) Math.Round((original.Height - max.Height*ratio)/2),
                (int) Math.Round((max.Width*ratio)),
                (int) Math.Round((max.Height*ratio)));
        }
    }
}