using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Hoursly.Domain.Projects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Hoursly.Persistance
{
    public interface IHourslyDbContex
    {
        DbSet<Project> Projects { get; }

        ValueTask<EntityEntry<T>> AddAsync<T>([NotNull] T entity,
            CancellationToken cancellationToken = default)
            where T : class;
    }
}