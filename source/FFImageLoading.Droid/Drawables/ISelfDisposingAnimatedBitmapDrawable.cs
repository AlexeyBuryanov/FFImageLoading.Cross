using Android.Graphics;
using FFImageLoading.Decoders;

namespace FFImageLoading.Droid.Drawables
{
	public interface ISelfDisposingAnimatedBitmapDrawable : ISelfDisposingBitmapDrawable
	{
		IAnimatedImage<Bitmap>[] AnimatedImages { get; }
	}
}
