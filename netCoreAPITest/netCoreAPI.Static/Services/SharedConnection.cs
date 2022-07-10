using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using netCoreAPI.Core.Interfaces.Repositories;
using netCoreAPI.Core.Interfaces.Repositories.Shared;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;

namespace netCoreAPI.Static.Services
{
    public sealed class SharedConnection<TDbContext>
        : ISharedConnection<TDbContext>
        , IDisposable
        where TDbContext : DbContext
    {
        private readonly TDbContext _context;
        private readonly ConcurrentDictionary<Type, object> _repositories;
        private bool _disposed;

        public SharedConnection(TDbContext context)
        {
            _repositories = new ConcurrentDictionary<Type, object>();
            _context = context;
        }

        public IEntityRepository<TEntity> Db<TEntity>() where TEntity : class
        {
            return _repositories.GetOrAdd(typeof(TEntity), new EntityRepository<TEntity, TDbContext>(_context)) as IEntityRepository<TEntity>;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public int RawQuery(string sql, params SqlParameter[] parameters)
        {
            return DbExtensions._IsolatedDbConnetion<int>(_context.Database.GetConnectionString(), sql, parameters, (command) =>
            {
                return command.ExecuteNonQuery();
            });
        }

        /// <summary>
        /// long way external sql query extension, prevents parameter type issues
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="rawSql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public List<T> RawQuery<T>(string sql, params SqlParameter[] parameters)
        {
            return DbExtensions._IsolatedDbConnetion<List<T>>(_context.Database.GetConnectionString(), sql, parameters, (command) =>
            {
                List<T> res = new List<T>();
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

                return res;
            });
        }

        /// <summary>
        /// MAGIC
        /// </summary>
        /// <returns></returns>
        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        internal void Dispose(bool disposing)
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