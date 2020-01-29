using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotChocolate.Execution
{
    internal static class QueryExecutorExtension
    {
        public static NamedQueryExecutor WithName(this IQueryExecutor queryExecutor, string name)
        {
            return new NamedQueryExecutor(name, queryExecutor);
        }
    }

    public interface INamedQueryExecutor
    {
        public string Name { get; }
        public IQueryExecutor QueryExecutor { get; }
    }

    public class NamedQueryExecutor : INamedQueryExecutor
    {
        public NamedQueryExecutor(string name, IQueryExecutor queryExecutor)
        {
            Name = name;
            QueryExecutor = queryExecutor;
        }

        public string Name { get; }
        public IQueryExecutor QueryExecutor { get; }
    }

    public interface INamedQueryExecutorProvider
    {
        IQueryExecutor GetQueryExecutor(string name);
    }

    public class NamedQueryExecutorProvider : INamedQueryExecutorProvider
    {
        private readonly Lazy<Dictionary<string, IQueryExecutor>> executorsLookup;
        private readonly Func<IEnumerable<INamedQueryExecutor>> executorsLoader;

        public NamedQueryExecutorProvider(Func<IEnumerable<INamedQueryExecutor>> executorsLoader)
        {
            this.executorsLoader = executorsLoader;
        }

        public IQueryExecutor GetQueryExecutor(string name)
        {
            return executorsLoader().LastOrDefault(x => x.Name == name)?.QueryExecutor;
        }
    }
}
