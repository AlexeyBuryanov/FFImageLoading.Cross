namespace FFImageLoading.Helpers
{
	public static class StaticLocks
	{
		public static SemaphoreSlim DecodingLock { get; set; }
	}
}
