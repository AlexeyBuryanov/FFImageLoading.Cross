namespace FFImageLoading.Decoders
{
    public class AnimatedImage<TNativeImageContainer> : IAnimatedImage<TNativeImageContainer>
    {
        public TNativeImageContainer Image { get; set; }

        public int Delay { get; set; }
    }
}
