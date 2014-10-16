using System.Drawing.Imaging;
using System.Linq;

namespace Antix.Drawing
{
    public class PngImageTranformOptions : ImageTransformOptions
    {
        public override ImageCodecInfo GetEncoder()
        {
            return ImageCodecInfo.GetImageEncoders()
                .Single(e => e.FormatID == ImageFormat.Png.Guid);
        }

        public override EncoderParameters GetEncoderParams()
        {
            return new EncoderParameters();
        }
    }
}