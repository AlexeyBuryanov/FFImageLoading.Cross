namespace FFImageLoading.Exceptions
{
    [Helpers.Preserve(AllMembers = true)]
    public class DownloadReadTimeoutException : Exception
    {
        public DownloadReadTimeoutException() : base("Read timeout")
        {
        }
    }
}
