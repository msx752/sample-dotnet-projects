using Microsoft.EntityFrameworkCore;
using System;

namespace Samp.Core.Interfaces.Repositories
{
    public interface ISharedRepository<TDbContext>
        : ISharedRepository
        where TDbContext : DbContext
    {
        IEFRepository<TEntity> Table<TEntity>() where TEntity : class;
    }

    public interface ISharedRepository : IDisposable
    {
        int Commit(string userId);
    }
}