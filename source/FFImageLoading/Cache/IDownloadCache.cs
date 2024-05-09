using FFImageLoading.Config;
using FFImageLoading.Work;

namespace FFImageLoading.Cache
{
    [Helpers.Preserve(AllMembers = true)]
	public interface IDownloadCache
	{
        Task<CacheStream> DownloadAndCacheIfNeededAsync (string url, TaskParameter parameters, Configuration configuration, CancellationToken token);
	}
}

