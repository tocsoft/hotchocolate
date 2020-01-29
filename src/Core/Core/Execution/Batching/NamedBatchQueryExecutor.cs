using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate.Utilities;

namespace HotChocolate.Execution.Batching
{
    public class NamedBatchQueryExecutor : INamedBatchQueryExecutor
    {
        public NamedBatchQueryExecutor(
            string name,
            IBatchQueryExecutor executor)
        {
            Name = name;
            Executor = executor;
        }

        public string Name { get; }
        public IBatchQueryExecutor Executor { get; }
    }
}
