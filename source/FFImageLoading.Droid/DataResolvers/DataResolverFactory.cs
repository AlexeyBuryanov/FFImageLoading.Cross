using FFImageLoading.Config;
using FFImageLoading.DataResolvers;
using FFImageLoading.Extensions;
using FFImageLoading.Work;

namespace FFImageLoading.Droid.DataResolvers
{
    public class DataResolverFactory : IDataResolverFactory
    {
        public IDataResolver GetResolver(string identifier, ImageSource source, TaskParameter parameters, Configuration configuration)
        {
            switch (source)
            {
                case ImageSource.ApplicationBundle:
                    return new BundleDataResolver();
                case ImageSource.CompiledResource:
                    return new ResourceDataResolver();
                case ImageSource.Filepath:
                    return new FileDataResolver();
                case ImageSource.Url:
                    if (!string.IsNullOrWhiteSpace(identifier) && identifier.IsDataUrl())
                        return new DataUrlResolver();
                    return new UrlDataResolver(configuration);
                case ImageSource.Stream:
                    return new StreamDataResolver();
                case ImageSource.EmbeddedResource:
                    return new EmbeddedResourceResolver();
                default:
                    throw new NotSupportedException("Unknown type of ImageSource");
            }
        }
    }
}
