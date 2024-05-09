using FFImageLoading.Config;
using FFImageLoading.DataResolvers;
using FFImageLoading.Extensions;
using FFImageLoading.Work;

namespace FFImageLoading.Ios.Shared.DataResolvers
{
    public class DataResolverFactoryIos : IDataResolverFactory
    {
        public virtual IDataResolver GetResolver(string identifier, ImageSource source, TaskParameter parameters, Configuration configuration)
        {
            switch (source)
            {
                case ImageSource.Filepath:
                    return new FileDataResolverIos();
				case ImageSource.ApplicationBundle:
				case ImageSource.CompiledResource:
                    return new BundleDataResolverIos();
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

