using System.Collections.Concurrent;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using FFImageLoading.Decoders;
using FFImageLoading.Work;

namespace FFImageLoading.Droid.DataResolvers
{
    public class ResourceDataResolver : IDataResolver
    {
        private const int DefaultDrawableSizePx = 64;
        static ConcurrentDictionary<string, int> _resourceIdentifiersCache = new ConcurrentDictionary<string, int>();

        public virtual Task<DataResolverResult> Resolve(string identifier, TaskParameter parameters, CancellationToken token)
        {
            // Resource name is always without extension
            string resourceName = System.IO.Path.GetFileNameWithoutExtension(identifier);

            if (!_resourceIdentifiersCache.TryGetValue(resourceName, out var resourceId))
            {
                token.ThrowIfCancellationRequested();
                resourceId = Context.Resources.GetIdentifier(resourceName.ToLowerInvariant(), "drawable", Context.PackageName);
                _resourceIdentifiersCache.TryAdd(resourceName.ToLowerInvariant(), resourceId);
            }

            if (resourceId == 0)
                throw new FileNotFoundException(identifier);

            token.ThrowIfCancellationRequested();

            var imageInformation = new ImageInformation();
            imageInformation.SetPath(identifier);
            imageInformation.SetFilePath(identifier);

            // Try to open as a raw resource stream (works for PNG, JPEG, etc.)
            // For XML-based drawables (vector, shape, layer-list, etc.), OpenRawResource
            // may throw or return XML that BitmapFactory cannot decode.
            try
            {
                Stream stream = Context.Resources.OpenRawResource(resourceId);

                if (stream == null)
                    throw new FileNotFoundException(identifier);

                // Check if the stream starts with an Android binary XML header (first byte 0x03)
                var firstByte = stream.ReadByte();
                stream.Position = 0;

                if (firstByte == 0x03)
                {
                    // Binary XML resource - use drawable inflation instead
                    stream.Dispose();
                    return Task.FromResult(ResolveXmlDrawable(resourceId, imageInformation));
                }

                return Task.FromResult(new DataResolverResult(
                    stream, LoadingResult.CompiledResource, imageInformation));
            }
            catch (Exception ex) when (ex is not OperationCanceledException)
            {
                // Fallback: inflate the drawable and convert to bitmap
                return Task.FromResult(ResolveXmlDrawable(resourceId, imageInformation));
            }
        }

        private DataResolverResult ResolveXmlDrawable(int resourceId, ImageInformation imageInformation)
        {
            var drawable = Context.Resources.GetDrawable(resourceId, Context.Theme);

            if (drawable == null)
                throw new FileNotFoundException(imageInformation.Path);

            int width = drawable.IntrinsicWidth > 0 ? drawable.IntrinsicWidth : DefaultDrawableSizePx;
            int height = drawable.IntrinsicHeight > 0 ? drawable.IntrinsicHeight : DefaultDrawableSizePx;

            var bitmap = Bitmap.CreateBitmap(width, height, Bitmap.Config.Argb8888);
            using (var canvas = new Canvas(bitmap))
            {
                drawable.SetBounds(0, 0, canvas.Width, canvas.Height);
                drawable.Draw(canvas);
            }

            var decoded = new DecodedImage<object>
            {
                Image = bitmap,
            };

            return new DataResolverResult(decoded, LoadingResult.CompiledResource, imageInformation);
        }

        protected Context Context => new ContextWrapper(Android.App.Application.Context);
    }
}

