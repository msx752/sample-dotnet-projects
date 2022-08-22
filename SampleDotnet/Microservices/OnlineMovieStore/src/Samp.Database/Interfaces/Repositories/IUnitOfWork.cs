using Microsoft.EntityFrameworkCore;
using Samp.Core.Entities;
using System;

namespace Samp.Core.Interfaces.Repositories
{
    public interface IUnitOfWork<TDbContext>
        : ISharedRepository
        where TDbContext : DbContext
    {
        IEFRepository<TEntity> Table<TEntity>() where TEntity : BaseEntity;
    }

    public interface ISharedRepository : IDisposable
    {
        int SaveChanges(Guid userId);
    }
}