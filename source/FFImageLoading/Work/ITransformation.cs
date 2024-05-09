namespace FFImageLoading.Work
{
    public interface ITransformation
    {
        string Key { get; }

        IBitmap Transform(IBitmap sourceBitmap, string path, ImageSource source, bool isPlaceholder, string key);
    }
}
