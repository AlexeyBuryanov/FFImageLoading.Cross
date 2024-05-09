namespace FFImageLoading.Exceptions
{
    [Helpers.Preserve(AllMembers = true)]
    public class DownloadException : Exception
    { 
        public DownloadException(string message) : base(message)
        {
        }
    }
}
