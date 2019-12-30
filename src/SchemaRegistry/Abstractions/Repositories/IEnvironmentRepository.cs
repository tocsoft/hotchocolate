using System.Dynamic;
using System.Threading;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MarshmallowPie.Repositories
{
    public interface IEnvironmentRepository
    {
        IQueryable<Environment> GetEnvironments();

        Task<Environment> GetEnvironmentAsync(
            Guid id,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyDictionary<Guid, Environment>> GetEnvironmentsAsync(
            IReadOnlyList<Guid> ids,
            CancellationToken cancellationToken = default);

        Task AddEnvironmentAsync(
            Environment environment,
            CancellationToken cancellationToken = default);

        Task UpdateEnvironmentAsync(
            Environment environment,
            CancellationToken cancellationToken = default);
    }
}
