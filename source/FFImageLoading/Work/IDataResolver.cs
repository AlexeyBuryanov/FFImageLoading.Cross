namespace FFImageLoading.Work
{
    public interface IDataResolver
    {
        Task<DataResolverResult> Resolve(string identifier, TaskParameter parameters, CancellationToken token);
    }
}
