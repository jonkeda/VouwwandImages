using SkiaSharp;
using System.IO;
using System.Reflection;

namespace VouwwandImages.Images
{
    public class ImageResource
    {
        public static SKBitmap LoadImage()
        {
            string resourceID = "VouwwandImages.Images.Weiland.png";
            Assembly assembly = typeof(ImageResource).GetTypeInfo().Assembly;

            using Stream? stream = assembly.GetManifestResourceStream(resourceID);
            return SKBitmap.Decode(stream);
        }
    }
}
