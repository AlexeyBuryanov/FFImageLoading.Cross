using Android.Graphics;
using FFImageLoading.Droid.Work;
using FFImageLoading.Work;

namespace FFImageLoading.Droid.Transformations
{
    public abstract class TransformationBase : ITransformation
    {
        public abstract string Key { get; }

        public IBitmap Transform(IBitmap bitmapHolder, string path, ImageSource source, bool isPlaceholder, string key)
        {
            var sourceBitmap = bitmapHolder.ToNative();

            if (sourceBitmap == null)
            {
	            var emptyBitmap = Bitmap.CreateBitmap(1, 1, Bitmap.Config.Argb8888);
	            emptyBitmap.EraseColor(Color.Transparent);

	            sourceBitmap = emptyBitmap;
            }

            return new BitmapHolder(Transform(sourceBitmap, path, source, isPlaceholder, key));
        }

        protected virtual Bitmap Transform(Bitmap sourceBitmap, string path, ImageSource source, bool isPlaceholder, string key)
        {
            return sourceBitmap;
        }
    }
}
