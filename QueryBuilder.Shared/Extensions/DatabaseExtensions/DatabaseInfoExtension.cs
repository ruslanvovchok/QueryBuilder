using Microsoft.Data.SqlClient;
using Npgsql;
using QueryBuilder.Shared.Common.Exceptions.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBuilder.Shared.Extensions.DatabaseExtensions
{
    public static class DatabaseInfoExtension
    {
        #region SQLServer
        public static void CheckSqlServerDbPassword(string connectionString)
        {
            var builder = new SqlConnectionStringBuilder(connectionString);

            if (String.IsNullOrEmpty(builder.Password))
                throw new NotValidConnectionStringException();
        }

        public static string GetSqlServerDatabaseName(string connectionString)
        {
            var builder = new SqlConnectionStringBuilder(connectionString);

            if (String.IsNullOrEmpty(builder.InitialCatalog))
                throw new NotValidConnectionStringException();

            return builder.InitialCatalog;
        }
        #endregion

        #region Postgres
        public static void CheckPostgresDbPassword(string connectionString)
        {
            var builder = new NpgsqlConnectionStringBuilder(connectionString);

            if (String.IsNullOrEmpty(builder.Password))
                throw new NotValidConnectionStringException();
        }

        public static string GetPostgresDatabaseName(string connectionString)
        {
            var builder = new NpgsqlConnectionStringBuilder(connectionString);

            if (String.IsNullOrEmpty(builder.Database))
                throw new NotValidConnectionStringException();

            return builder.Database;
        }
        #endregion
    }
}
