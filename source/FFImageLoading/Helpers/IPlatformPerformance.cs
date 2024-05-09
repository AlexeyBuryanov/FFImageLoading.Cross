namespace FFImageLoading.Helpers
{
    [Preserve(AllMembers = true)]
    public interface IPlatformPerformance
    {
        int GetCurrentManagedThreadId();

        int GetCurrentSystemThreadId();

        string GetMemoryInfo();
    }
}

