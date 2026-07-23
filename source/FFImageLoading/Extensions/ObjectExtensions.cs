namespace FFImageLoading.Extensions
{
    public static class ObjectExtensions
    {
        public static bool TryDispose(this IDisposable obj)
        {
            try
            {
                if (obj != null)
                {
                    obj?.Dispose();
                    return true;
                }
            }
            catch (Exception)
            {
            }

            return false;
        }
    }
}
