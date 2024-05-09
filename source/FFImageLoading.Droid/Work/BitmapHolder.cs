using Android.Graphics;
using FFImageLoading.Work;

namespace FFImageLoading.Droid.Work
{
    public class BitmapHolder: IBitmap
    {
        public BitmapHolder(Bitmap bitmap)
        {
            NativeBitmap = bitmap;
        }

        public int Width => NativeBitmap.Width;

        public int Height => NativeBitmap.Height;

        internal Bitmap NativeBitmap
        {
            get;
            private set;
        }
    }

    public static class BitmapExtensions
    {
        public static Bitmap ToNative(this IBitmap bitmap)
        {
            return ((BitmapHolder)bitmap).NativeBitmap;
        }
    }
}

