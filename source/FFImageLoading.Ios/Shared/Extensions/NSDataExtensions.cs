#if MACOS
using AppKit;
using PImage = AppKit.NSImage;
#elif IOS
using PImage = UIKit.UIImage;
#endif
using System.Runtime.InteropServices;
using FFImageLoading.Config;
using FFImageLoading.Ios.Shared.Decoders;
using FFImageLoading.Work;

namespace FFImageLoading.Ios.Shared.Extensions
{
    public static class NSDataExtensions
    {
        public static async Task<PImage> ToImageAsync(this NSData data, CGSize destSize, NFloat destScale, Configuration config, TaskParameter parameters, GifDecoderIos.RCTResizeMode resizeMode = GifDecoderIos.RCTResizeMode.ScaleAspectFit, ImageInformation imageinformation = null, bool allowUpscale = false)
        {
            var decoded = await GifDecoderIos.SourceRegfToDecodedImageAsync(
				data, destSize, destScale, config, parameters, resizeMode, imageinformation, allowUpscale).ConfigureAwait(false);

            PImage result;

            if (decoded.IsAnimated)
            {
#if IOS
                    result = PImage.CreateAnimatedImage(decoded.AnimatedImages
                                                        .Select(v => v.Image)
                                                        .Where(v => v?.CGImage != null).ToArray(), decoded.AnimatedImages.Sum(v => v.Delay) / 100.0);
#elif MACOS
                result = new PImage();
                var repr = decoded.AnimatedImages
                                  .Select(v => v.Image.Representations().First())
                                  .ToArray();
                result.AddRepresentations(repr);
#endif
            }
            else
            {
                result = decoded.Image;
            }

            return result;
        }
    }
}

