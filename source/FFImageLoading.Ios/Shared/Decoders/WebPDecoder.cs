#if MACOS
using AppKit;
using PImage = AppKit.NSImage;
//using WebPCodec = WebP.Mac.WebPCodec;
#elif IOS
using PImage = UIKit.UIImage;
//using WebPCodec = WebP.Touch.WebPCodec;
#endif
using FFImageLoading.Config;
using FFImageLoading.Decoders;
using FFImageLoading.Extensions;
using FFImageLoading.Helpers;
using FFImageLoading.Ios.Shared.Extensions;
using FFImageLoading.Work;


namespace FFImageLoading.Ios.Shared.Decoders
{
    public class WebPDecoderIos : IDecoder<PImage>
    {
        // TODO: WebPCodec
        //WebPCodec _decoder;

        public Task<IDecodedImage<PImage>> DecodeAsync(Stream stream, string path, ImageSource source, ImageInformation imageInformation, TaskParameter parameters)
        {
            //if (_decoder == null)
            //    _decoder = new WebPCodec();

            var result = new DecodedImage<PImage>();
            //result.Image = _decoder.Decode(stream);

            var downsampleWidth = parameters.DownSampleSize?.Item1 ?? 0;
            var downsampleHeight = parameters.DownSampleSize?.Item2 ?? 0;
            // TODO allowUpscale
            // bool allowUpscale = parameters.AllowUpscale ?? Configuration.AllowUpscale;

            if (parameters.DownSampleUseDipUnits)
            {
                downsampleWidth = downsampleWidth.DpToPixels();
                downsampleHeight = downsampleHeight.DpToPixels();
            }

            if (downsampleWidth != 0 || downsampleHeight != 0)
            {
                var interpolationMode = parameters.DownSampleInterpolationMode == InterpolationMode.Default ? Configuration.DownsampleInterpolationMode : parameters.DownSampleInterpolationMode;
                var old = result.Image;
                result.Image = old.ResizeUIImage(downsampleWidth, downsampleHeight, interpolationMode);
                old.TryDispose();
            }

            return Task.FromResult<IDecodedImage<PImage>>(result);
        }

        public Configuration Configuration => ImageService.Instance.Config;

        public IMiniLogger Logger => ImageService.Instance.Config.Logger;
    }
}
