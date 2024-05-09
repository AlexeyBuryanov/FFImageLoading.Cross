using FFImageLoading.Work;

namespace FFImageLoading.Args
{
    [Helpers.Preserve(AllMembers = true)]
    public class FileWriteFinishedEventArgs : EventArgs
    {
        public FileWriteFinishedEventArgs(FileWriteInfo fileWriteInfo)
        {
            FileWriteInfo = fileWriteInfo;
        }

        public FileWriteInfo FileWriteInfo { get; private set; }
    }
}
