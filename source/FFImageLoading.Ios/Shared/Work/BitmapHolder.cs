#if MACOS
using AppKit;
using PImage = AppKit.NSImage;
#elif IOS
using PImage = UIKit.UIImage;
#endif
using FFImageLoading.Work;

namespace FFImageLoading.Ios.Shared.Work
{
    public class BitmapHolderIos : IBitmap
    {
        public BitmapHolderIos(PImage bitmap)
        {
            NativeBitmap = bitmap;
        }

        public int Width => (int)NativeBitmap.Size.Width;

        public int Height => (int)NativeBitmap.Size.Height;

        internal PImage NativeBitmap
        {
            get;
            private set;
        }
    }

    public static class IBitmapExtensionsIos
    {
        public static PImage ToNative(this IBitmap bitmap)
        {
            return ((BitmapHolderIos)bitmap).NativeBitmap;
        }
    }
}

