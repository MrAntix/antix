using System.Drawing.Imaging;
using System.Linq;

namespace Antix.Drawing
{
    public class JpegImageTranformOptions : ImageTransformOptions
    {
        public int Quality { get; set; }

        public override ImageCodecInfo GetEncoder()
        {
            return ImageCodecInfo.GetImageEncoders()
                .Single(e => e.FormatID == ImageFormat.Jpeg.Guid);
        }

        public override EncoderParameters GetEncoderParams()
        {
            var parameters = new EncoderParameters(1);
            parameters.Param[0]
                = new EncoderParameter(Encoder.Quality, Quality);

            return parameters;
        }
    }
}