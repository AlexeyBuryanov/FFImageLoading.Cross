namespace FFImageLoading.Exceptions
{
    [Helpers.Preserve(AllMembers = true)]
    public class DownloadHeadersTimeoutException : Exception
    {
        public DownloadHeadersTimeoutException() : base("Headers timeout")
        {
        }
    }
}
