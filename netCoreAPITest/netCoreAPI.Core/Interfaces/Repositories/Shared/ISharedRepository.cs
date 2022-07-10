using Microsoft.EntityFrameworkCore;
using System;

namespace netCoreAPI.Core.Interfaces.Repositories.Shared
{
    public interface ISharedRepository<TDbContext>
        : ISharedRepository
        where TDbContext : DbContext
    {
        IEntityRepository<TEntity> Db<TEntity>() where TEntity : class;
    }

    public interface ISharedRepository : IDisposable
    {
        int SaveChanges();
    }
}