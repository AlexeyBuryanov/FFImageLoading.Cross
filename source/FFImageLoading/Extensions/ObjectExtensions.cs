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
            catch (Exception ex) when (ex is not OutOfMemoryException
                and not StackOverflowException
                and not AccessViolationException
                and not AppDomainUnloadedException
                and not BadImageFormatException
                and not CannotUnloadAppDomainException
                and not InvalidProgramException)
            {
            }

            return false;
        }
    }
}
