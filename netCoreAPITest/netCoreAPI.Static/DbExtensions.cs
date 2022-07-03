using Microsoft.Data.SqlClient;
using System;

namespace netCoreAPI.Static
{
    public static class DbExtensions
    {
        public static T _IsolatedDbConnetion<T>(string connectionString, string sqlQuery, SqlParameter[] parameters, Func<SqlCommand, T> commandExecute)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = sqlQuery;
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

                    conn.Open();

                    return commandExecute(command);
                }
            }
        }
    }
}