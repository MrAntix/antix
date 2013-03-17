using System.Drawing;
using System.Drawing.Imaging;

namespace Antix.Drawing
{
    public abstract class ImageTransformOptions
    {
        public Rectangle Crop { get; set; }

        public abstract ImageCodecInfo GetEncoder();
        public abstract EncoderParameters GetEncoderParams();
    }
}