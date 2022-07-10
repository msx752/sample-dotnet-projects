using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace netCoreAPI.Core.Interfaces.Repositories.Shared
{
    public interface ISharedConnection<TDbContext>
        : ISharedConnection
        where TDbContext : DbContext
    {
        IEntityRepository<TEntity> Db<TEntity>() where TEntity : class;
    }

    public interface ISharedConnection
    {
        void Dispose();

        int SaveChanges();
    }
}