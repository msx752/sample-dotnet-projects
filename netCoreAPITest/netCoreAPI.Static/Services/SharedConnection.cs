using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using netCoreAPI.Core.ApplicationService;
using netCoreAPI.Core.ApplicationService.Services;
using netCoreAPI.Data.Migrations;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;

namespace netCoreAPI.Static.Services
{
    public partial class SharedConnection : ISharedConnection, IDisposable
    {
        private readonly MyContext _context;
        private bool _disposed;
        private readonly ConcurrentDictionary<Type, object> _repositories;

        public SharedConnection(MyContext context)
        {
            _repositories = new ConcurrentDictionary<Type, object>();
            _context = context;
        }

        public IBaseEntityRepository<TEntity> Db<TEntity>() where TEntity : class
        {
            return _repositories.GetOrAdd(typeof(TEntity), new BaseEntityRepository<TEntity>(_context)) as IBaseEntityRepository<TEntity>;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public int RawQuery(string sql, params object[] parameters)
        {
            return _context.Database.ExecuteSqlRaw(sql, parameters);
        }

        /// <summary>
        /// long way external sql query extension, prevents parameter type issues
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="rawSql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public List<T> RawQuery<T>(string rawSql, params SqlParameter[] parameters)
        {
            var conn = this._context.Database.GetDbConnection();
            List<T> res = new List<T>();
            using (var command = conn.CreateCommand())
            {
                command.CommandText = rawSql;
                command.Parameters.Clear();
                if (parameters != null)
                {
                    foreach (var item in parameters)
                    {
                        var p = command.CreateParameter();
                        p.ParameterName = item.ParameterName;
                        p.Value = item.Value;
                        p.DbType = item.DbType;
                        command.Parameters.Add(p);
                    }
                }
                var wasOpen = false;
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                    wasOpen = true;
                }
                using (var r = command.ExecuteReader())
                {
                    while (r.Read())
                    {
                        T t = Activator.CreateInstance<T>();
                        Type type = t.GetType();
                        for (int inc = 0; inc < r.FieldCount; inc++)
                        {
                            string pname = r.GetName(inc);
                            PropertyInfo prop = type.GetProperty(pname);
                            prop.SetValue(t, r.GetValue(inc), null);
                        }
                        res.Add(t);
                    }
                }
                if (wasOpen)
                    conn.Close();
            }
            return res;
        }

        /// <summary>
        /// MAGIC
        /// </summary>
        /// <returns></returns>
        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context?.Dispose();
                    _repositories?.Clear();
                }
                this._disposed = true;
            }
        }
    }
}