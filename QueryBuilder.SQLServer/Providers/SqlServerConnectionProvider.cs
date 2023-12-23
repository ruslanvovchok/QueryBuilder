using Microsoft.Data.SqlClient;
using QueryBuilder.Shared.Common.Models.ConnectionResult;
using QueryBuilder.Shared.Extensions.DatabaseExtensions;
using QueryBuilder.Shared.Infrastucture.Abstractions;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBuilder.SQLServer.Providers
{
    public class SqlServerConnectionProvider : DBConnectionProvider<SqlConnection>
    {
        public override async Task<SqlConnection> CreateConnectionAsync(string connectionString, CancellationToken cancellationToken)
        {
            var connection = new SqlConnection(connectionString);
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
            DatabaseInfoExtension.CheckSqlServerDbPassword(connectionString);

            Database database = new();
            List<Table> tables = new();

            database.Name = DatabaseInfoExtension.GetSqlServerDatabaseName(connectionString);
            await using SqlConnection? connection = await CreateConnectionAsync(connectionString, cancellationToken);
            await using SqlCommand? command = connection.CreateCommand();

            //Get tables from database
            command.CommandText = @$"
                                    SELECT TABLE_NAME 
                                    FROM INFORMATION_SCHEMA.TABLES 
                                    WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_CATALOG = '{database.Name}'
                                    ";

            using SqlDataReader tablesReader = await command.ExecuteReaderAsync(cancellationToken);

            while (await tablesReader.ReadAsync(cancellationToken))
            {
                if (tablesReader.GetString(0) == "sysdiagrams")
                    continue;

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
                await using SqlCommand? columnsCommand = connection.CreateCommand();

                columnsCommand.CommandText = $@"
                                        SELECT COLUMN_NAME, DATA_TYPE
                                        FROM INFORMATION_SCHEMA.COLUMNS
                                        WHERE TABLE_NAME = {table.Name} AND TABLE_CATALOG = '{database.Name}';           
                                       ";

                using SqlDataReader columnsReader = await command.ExecuteReaderAsync(cancellationToken);

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
