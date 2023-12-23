using Microsoft.Data.SqlClient;
using Npgsql;
using QueryBuilder.Postgres.Providers;
using QueryBuilder.Shared.Common.Enums;
using QueryBuilder.Shared.Common.Exceptions.Database;
using QueryBuilder.Shared.Common.Models.ConnectionResult;
using QueryBuilder.Shared.Infrastucture.Abstractions.Services;
using QueryBuilder.SQLServer.Providers;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBuilder.Infrastucture.Services
{
    public class ConnectionExecutor : IConnectionExecutor
    {
        private readonly SqlServerConnectionProvider sqlServerConnectionProvider;
        private readonly PostgresDBConnectionProvider postgresDbConnectionProvider;

        public ConnectionExecutor()
        {
            sqlServerConnectionProvider = new();
            postgresDbConnectionProvider = new();
        }

        public async Task<Database?> ExecuteAsync(string connectionString, DataBaseTypes dbType, CancellationToken cancellationToken)
        {
            switch (dbType)
            {
                case DataBaseTypes.SqlServer:
                    var sqlDatabase = await sqlServerConnectionProvider.GetDatabaseInfoAsync(connectionString, cancellationToken);
                    return sqlDatabase;
                case DataBaseTypes.Postgres:
                    var postgresDatabase = await postgresDbConnectionProvider.GetDatabaseInfoAsync(connectionString, cancellationToken);
                    return postgresDatabase;
                default:
                    throw new NotSupportedDBTypeException();
            }
        }

        public async Task<DbDataReader> ConnectAsync(string connectionString, DataBaseTypes dbType, string query, CancellationToken cancellationToken)
        {
            switch (dbType)
            {
                case DataBaseTypes.SqlServer:
                    var sqlServerConnection = await ConnectToSqlServerAsync(connectionString, cancellationToken);
                    var sqlServerCommand = sqlServerConnection.CreateCommand();
                    sqlServerCommand.CommandText = query;
                    return await sqlServerCommand.ExecuteReaderAsync(cancellationToken);
                case DataBaseTypes.Postgres:
                    var postgrConnection = await ConnectToPosgtresAsync(connectionString, cancellationToken);
                    var postgresCommand = postgrConnection.CreateCommand();
                    postgresCommand.CommandText = query;
                    return await postgresCommand.ExecuteReaderAsync(cancellationToken);
                default:
                    throw new NotSupportedDBTypeException();
            }
        }

        private async Task<NpgsqlConnection> ConnectToPosgtresAsync(string connectionString, CancellationToken cancellationToken)
            => await postgresDbConnectionProvider.CreateConnectionAsync(connectionString, cancellationToken);

        private async Task<SqlConnection> ConnectToSqlServerAsync(string connectionString, CancellationToken cancellationToken)
            => await sqlServerConnectionProvider.CreateConnectionAsync(connectionString, cancellationToken);

        
    }
}
