using FFImageLoading.Cache;
using FFImageLoading.Config;
using FFImageLoading.Extensions;
using FFImageLoading.Work;

namespace FFImageLoading.DataResolvers
{
    public class UrlDataResolver : IDataResolver
    {
        public UrlDataResolver(Configuration configuration)
        {
            Configuration = configuration;
        }

        protected IDownloadCache DownloadCache => Configuration.DownloadCache;
        protected Configuration Configuration { get; private set; }

        public virtual async Task<DataResolverResult> Resolve(string identifier, TaskParameter parameters, CancellationToken token)
        {

            var downloadedData = await DownloadCache.DownloadAndCacheIfNeededAsync(identifier, parameters, Configuration, token).ConfigureAwait(false);

            if (token.IsCancellationRequested)
            {
                downloadedData?.ImageStream.TryDispose();
                token.ThrowIfCancellationRequested();
            }

            var imageInformation = new ImageInformation();
            imageInformation.SetPath(identifier);
            imageInformation.SetFilePath(downloadedData?.FilePath);

            return new DataResolverResult(
                downloadedData?.ImageStream, downloadedData.RetrievedFromDiskCache ? LoadingResult.DiskCache : LoadingResult.Internet, imageInformation);
        }
    }
}
