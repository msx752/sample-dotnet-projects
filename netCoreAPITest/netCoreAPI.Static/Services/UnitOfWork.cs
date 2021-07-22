using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using netCoreAPI.Core.ApplicationService;
using netCoreAPI.Core.ApplicationService.Services;
using netCoreAPI.Data.Migrations;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace netCoreAPI.Static.Services
{
    public partial class UnitOfWork : IUnitOfWork, IDisposable
    {
        private bool _disposed;
        private Dictionary<Type, object> _repositories;

        public UnitOfWork(MyContext context)
        {
            _repositories = new Dictionary<Type, object>(30);
            Context = context;
        }

        public MyContext Context { get; }

        /// <summary>
        /// MAGIC
        /// </summary>
        /// <returns></returns>
        public int Commit()
        {
            return Context.SaveChanges();
        }

        public IEFRepository<TEntity> Db<TEntity>() where TEntity : class
        {
            if (_repositories.ContainsKey(typeof(TEntity)))
                return _repositories[typeof(TEntity)] as IEFRepository<TEntity>;
            var repository = new EFRepository<TEntity>(Context);
            _repositories.Add(typeof(TEntity), repository);
            return repository;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public int RawQuery(string sql, params object[] parameters)
        {
            return Context.Database.ExecuteSqlRaw(sql, parameters);
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
            var conn = this.Context.Database.GetDbConnection();
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
                        for (int inc = 0; inc < r.FieldCount; inc++)
                        {
                            Type type = t.GetType();
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

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    Context?.Dispose();
                    _repositories?.Clear();
                }
                this._disposed = true;
            }
        }
    }
}