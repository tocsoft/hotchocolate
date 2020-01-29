using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate.Utilities;

namespace HotChocolate.Execution.Batching
{
    public class BatchQueryExecutorProvider : IBatchQueryExecutorProvider
    {
        private readonly Func<IEnumerable<INamedBatchQueryExecutor>> executorsLoader;

        public BatchQueryExecutorProvider(
            Func<IEnumerable<INamedBatchQueryExecutor>> executorsLoader)
        {
            this.executorsLoader = executorsLoader;
        }

        public IBatchQueryExecutor GetExecutor(string name)
        {
            return executorsLoader().LastOrDefault(x => x.Name == name)?.Executor;
        }
    }
}
