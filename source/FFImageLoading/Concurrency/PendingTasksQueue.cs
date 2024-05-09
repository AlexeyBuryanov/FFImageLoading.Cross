using FFImageLoading.Concurrency.FastPriorityQueue;
using FFImageLoading.Work;

namespace FFImageLoading.Concurrency
{
    public class PendingTasksQueue : SimplePriorityQueue<IImageLoaderTask, int>
    {
        public IImageLoaderTask FirstOrDefaultByRawKey(string rawKey)
        {
            lock (_queue)
            {
                return _queue.FirstOrDefault(v => v.Data?.KeyRaw == rawKey)?.Data;
            }
        }
    }
}
