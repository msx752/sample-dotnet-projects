using Microsoft.EntityFrameworkCore;
using Samp.Core.Entities;
using System;

namespace Samp.Core.Interfaces.Repositories
{
    public interface ISharedRepository<TDbContext>
        : ISharedRepository
        where TDbContext : DbContext
    {
        IEFRepository<TEntity> Table<TEntity>() where TEntity : BaseEntity;
    }

    public interface ISharedRepository : IDisposable
    {
        int Commit(string userId);
    }
}