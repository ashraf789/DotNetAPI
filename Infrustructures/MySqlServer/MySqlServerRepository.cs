using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Infrustructures.MySqlServer
{
    public static class MySqlServerRepository
    {
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString;

        public static async Task<DbConnection> OpenConnection()
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            await connection.EnsureOpenAsync().ConfigureAwait(false);
            return connection;
        }

        public static async Task<IDisposable> EnsureOpenAsync(this MySqlConnection connection)
        {
            if (connection == null) throw new ArgumentNullException(nameof(connection));

            switch (connection.State)
            {
                case ConnectionState.Open:
                    return null;
                case ConnectionState.Closed:
                    await connection.OpenAsync().ConfigureAwait(false);

                    try{
                        return new ConenctionCloser(connection);
                    }
                    catch
                    {
                        try
                        {
                            connection.Close();
                        }
                        catch { }
                        throw;
                    }
                default:
                    throw new InvalidOperationException("Cannot use EnsureOpen when connection is " + connection.State);
                
            }
        }

        public static bool IsAvailable()
        {
            MySqlConnection connection = new MySqlConnection(connectionString);

            try
            {
                connection.Open();
                connection.Close();
                return true;
            }catch
            {
                return false;
            }
        }

        private class ConenctionCloser : IDisposable
        {
            private MySqlConnection _connection;
            
            public ConenctionCloser(MySqlConnection connection)
            {
                _connection = connection;
            }

            public void Dispose()
            {
                var cn = _connection;
                _connection = null;

                try
                {
                    cn?.Close();
                }
                catch { }
            }

        }
    }
}