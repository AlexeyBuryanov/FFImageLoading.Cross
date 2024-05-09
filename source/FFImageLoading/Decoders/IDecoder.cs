using FFImageLoading.Work;

namespace FFImageLoading.Decoders
{
    public interface IDecoder<TDecoderContainer>
    {
        Task<IDecodedImage<TDecoderContainer>> DecodeAsync(Stream stream, string path, ImageSource source, ImageInformation imageInformation, TaskParameter parameters);
    }
}
