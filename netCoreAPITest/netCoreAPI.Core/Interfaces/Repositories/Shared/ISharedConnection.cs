using Microsoft.EntityFrameworkCore;
using System;

namespace netCoreAPI.Core.Interfaces.Repositories.Shared
{
    public interface ISharedConnection<TDbContext>
        : ISharedConnection
        where TDbContext : DbContext
    {
        IEntityRepository<TEntity> Db<TEntity>() where TEntity : class;

        int SaveChanges();
    }

    public interface ISharedConnection : IDisposable
    {
    }
}