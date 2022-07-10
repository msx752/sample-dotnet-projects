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

        List<T> RawQuery<T>(string rawSql, params SqlParameter[] parameters);
    }

    public interface ISharedConnection
    {
        void Dispose();

        int RawQuery(string sql, params SqlParameter[] parameters);

        int SaveChanges();
    }
}