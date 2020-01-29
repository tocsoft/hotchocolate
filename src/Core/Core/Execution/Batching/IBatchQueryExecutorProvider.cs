namespace HotChocolate.Execution.Batching
{
    public interface IBatchQueryExecutorProvider
    {
        IBatchQueryExecutor GetExecutor(string name);
    }
}