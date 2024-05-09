#if MACOS
using AppKit;
using PImage = AppKit.NSImage;
#elif IOS
using PImage = UIKit.UIImage;
#endif
using System.Runtime.InteropServices;
using FFImageLoading.Extensions;
using FFImageLoading.Work;

namespace FFImageLoading.Ios.Shared.Extensions
{
    public static class PImageExtensions
    {
        public static nuint GetMemorySize(this PImage image)
        {
            return (nuint)(image.CGImage.BytesPerRow * image.CGImage.Height);
        }

        public static PImage ResizeUIImage(this PImage image, double desiredWidth, double desiredHeight, InterpolationMode interpolationMode)
        {
            var widthRatio = desiredWidth / image.Size.Width;
            var heightRatio = desiredHeight / image.Size.Height;
            var scaleRatio = Math.Min(widthRatio, heightRatio);

            if (Math.Abs(desiredWidth) < double.Epsilon )
                scaleRatio = heightRatio;

            if (Math.Abs(desiredHeight) < double.Epsilon)
                scaleRatio = widthRatio;

            var aspectWidth = image.Size.Width * scaleRatio;
            var aspectHeight = image.Size.Height * scaleRatio;

            var newSize = new CGSize(aspectWidth, aspectHeight);
#if MACOS
            var resizedImage = new PImage(newSize);
            resizedImage.LockFocus();
            image.Draw(new CGRect(CGPoint.Empty, newSize), CGRect.Empty, NSCompositingOperation.SourceOver, 1.0f);
            resizedImage.UnlockFocus();
            return resizedImage;
#elif IOS
            UIGraphics.BeginImageContextWithOptions(newSize, false, 0);

            try
            {
                image.Draw(new CGRect((NFloat)0.0, (NFloat)0.0, newSize.Width, newSize.Height));

                using (var context = UIGraphics.GetCurrentContext())
                {
                    if (interpolationMode == InterpolationMode.None)
                        context.InterpolationQuality = CGInterpolationQuality.None;
                    else if (interpolationMode == InterpolationMode.Low)
                        context.InterpolationQuality = CGInterpolationQuality.Low;
                    else if (interpolationMode == InterpolationMode.Medium)
                        context.InterpolationQuality = CGInterpolationQuality.Medium;
                    else if (interpolationMode == InterpolationMode.High)
                        context.InterpolationQuality = CGInterpolationQuality.High;
                    else
                        context.InterpolationQuality = CGInterpolationQuality.Low;

                    var resizedImage = UIGraphics.GetImageFromCurrentImageContext();

                    return resizedImage;
                }
            }
            finally
            {
                UIGraphics.EndImageContext();
                image.TryDispose();
            }
#endif
        }

        public static System.IO.Stream AsPngStream(this PImage image)
        {
#if IOS
            return image.AsPNG()?.AsStream();
#elif MACOS
            var imageRep = new NSBitmapImageRep(image.AsTiff());
            return imageRep.RepresentationUsingTypeProperties(NSBitmapImageFileType.Png)
                                                             .AsStream();
#endif
        }

        public static System.IO.Stream AsJpegStream(this PImage image, int quality = 80)
        {
#if IOS
            return image.AsJPEG(quality / 100f).AsStream();
#elif MACOS
            // todo: jpeg quality?
            var imageRep = new NSBitmapImageRep(image.AsTiff());
            return imageRep.RepresentationUsingTypeProperties(NSBitmapImageFileType.Jpeg)
                           .AsStream();
#endif
        }
    }
}

