namespace FFImageLoading.Exceptions
{
    [Helpers.Preserve(AllMembers = true)]
    public class DownloadAggregateException : AggregateException
    {
        public DownloadAggregateException()
        {
        }

        public DownloadAggregateException(IEnumerable<Exception> exceptions) : base(exceptions)
        {
        }
    }
}
