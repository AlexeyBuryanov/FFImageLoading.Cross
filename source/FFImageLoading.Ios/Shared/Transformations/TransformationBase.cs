#if MACOS
using AppKit;
using PImage = AppKit.NSImage;
#elif IOS
using PImage = UIKit.UIImage;
#endif
using FFImageLoading.Ios.Shared.Work;
using FFImageLoading.Work;

namespace FFImageLoading.Ios.Shared.Transformations
{
    public abstract class TransformationBase : ITransformation
    {
        public abstract string Key { get; }

        public IBitmap Transform(IBitmap bitmapHolder, string path, ImageSource source, bool isPlaceholder, string key)
        {
            var sourceBitmap = bitmapHolder.ToNative();
            return new BitmapHolderIos(Transform(sourceBitmap, path, source, isPlaceholder, key));
        }

        protected virtual PImage Transform(PImage sourceBitmap, string path, ImageSource source, bool isPlaceholder, string key)
        {
            return sourceBitmap;
        }
    }
}

