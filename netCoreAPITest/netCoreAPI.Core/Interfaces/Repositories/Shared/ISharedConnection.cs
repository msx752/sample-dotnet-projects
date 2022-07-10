using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

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