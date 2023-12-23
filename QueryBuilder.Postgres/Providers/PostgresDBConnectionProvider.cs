using Npgsql;
using QueryBuilder.Shared.Common.Models.ConnectionResult;
using QueryBuilder.Shared.Extensions.DatabaseExtensions;
using QueryBuilder.Shared.Infrastucture.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBuilder.Postgres.Providers
{
    public class PostgresDBConnectionProvider : DBConnectionProvider<NpgsqlConnection>
    {
        public override async Task<NpgsqlConnection> CreateConnectionAsync(string connectionString, CancellationToken cancellationToken)
        {
            var connection = new NpgsqlConnection(connectionString);
            await connection.OpenAsync(cancellationToken);
            return connection;
        }

        /// <summary>
        /// This method serves for getting information about database like tables, columns, and columns types
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override async Task<Database> GetDatabaseInfoAsync(string connectionString, CancellationToken cancellationToken)
        {
            DatabaseInfoExtension.CheckPostgresDbPassword(connectionString);

            Database database = new();
            List<Table> tables = new();

            database.Name = DatabaseInfoExtension.GetPostgresDatabaseName(connectionString);
            await using NpgsqlConnection? connection = await CreateConnectionAsync(connectionString, cancellationToken);
            await using NpgsqlCommand command = connection.CreateCommand();

            //Get tables from database
            command.CommandText = @"
                                   SELECT *
                                   FROM pg_catalog.pg_tables
                                   WHERE schemaname != 'pg_catalog' AND 
                                       schemaname != 'information_schema';
                                   ";

            using NpgsqlDataReader tablesReader = await command.ExecuteReaderAsync(cancellationToken);

            while (await tablesReader.ReadAsync(cancellationToken))
            {
                var table = new Table()
                {
                    Name = tablesReader.GetString(1),
                    Columns = new List<Column?>()
                };

                if (table is not null)
                    tables.Add(table);
            }

            await tablesReader.DisposeAsync();

            foreach (var table in tables)
            {
                await using NpgsqlCommand columnsCommand = connection.CreateCommand();

                //Get columns from table
                columnsCommand.CommandText = $@"
                                        SELECT column_name, data_type
                                        FROM information_schema.columns
                                        WHERE table_name = '{table.Name}' AND table_schema = 'public';
                                        ";

                using NpgsqlDataReader columnsReader = await columnsCommand.ExecuteReaderAsync(cancellationToken);

                while (await columnsReader.ReadAsync(cancellationToken))
                {
                    Column column = new()
                    {
                        Name = columnsReader.GetString(0),
                        Type = columnsReader.GetString(1)
                    };

                    if (column is not null)
                        table.Columns.Add(column);

                }
            }

            database.Tables.AddRange(tables);

            return database;
        }

    }
}
