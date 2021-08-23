using Microsoft.Data.SqlClient;
using netCoreAPI.Core.ApplicationService.Services;
using System.Collections.Generic;

namespace netCoreAPI.Core.ApplicationService
{
    public interface IUnitOfWork
    {
        int SaveChanges();

        IEFRepository<TEntity> Db<TEntity>() where TEntity : class;

        void Dispose();

        int RawQuery(string sql, params object[] parameters);

        List<T> RawQuery<T>(string rawSql, params SqlParameter[] parameters);
    }
}