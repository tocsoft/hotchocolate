namespace HotChocolate.Execution.Batching
{
    public interface INamedBatchQueryExecutor
    {
        IBatchQueryExecutor Executor { get; }
        string Name { get; }
    }
}