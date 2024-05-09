using Android.Content;
using Android.Content.Res;
using FFImageLoading.Work;

namespace FFImageLoading.Droid.DataResolvers
{
    public class BundleDataResolver : IDataResolver
    {
        public virtual Task<DataResolverResult> Resolve(string identifier, TaskParameter parameters, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();
            var stream = Context.Assets.Open(identifier, Access.Streaming);

            if (stream == null)
                throw new FileNotFoundException(identifier);

            var imageInformation = new ImageInformation();
            imageInformation.SetPath(identifier);
            imageInformation.SetFilePath(null);

            return Task.FromResult(new DataResolverResult(stream, LoadingResult.ApplicationBundle, imageInformation));
        }

        protected Context Context => new ContextWrapper(Android.App.Application.Context);
    }
}

