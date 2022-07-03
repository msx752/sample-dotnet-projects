using Microsoft.Data.SqlClient;
using netCoreAPI.Core.ApplicationService.Services;
using System.Collections.Generic;

namespace netCoreAPI.Core.ApplicationService
{
    public interface ISharedConnection
    {
        int SaveChanges();

        IBaseEntityRepository<TEntity> Db<TEntity>() where TEntity : class;

        void Dispose();

        int RawQuery(string sql, params SqlParameter[] parameters);

        List<T> RawQuery<T>(string rawSql, params SqlParameter[] parameters);
    }
}