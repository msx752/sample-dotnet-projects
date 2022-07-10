using Microsoft.Data.SqlClient;
using netCoreAPI.Core.Interfaces.Repositories;
using System.Collections.Generic;

namespace netCoreAPI.Core.Interfaces.Repositories.Shared
{
    public interface ISharedConnection
    {
        int SaveChanges();

        IEntityRepository<TEntity> Db<TEntity>() where TEntity : class;

        void Dispose();

        int RawQuery(string sql, params SqlParameter[] parameters);

        List<T> RawQuery<T>(string rawSql, params SqlParameter[] parameters);
    }
}