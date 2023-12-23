using QueryBuilder.Postgres.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBuilder.Tests.DatabaseProviders
{
    public class CreateConnectionExecutorToPostgresTests
    {
        [Fact]
        public async Task GetDatabaseInfo_Should_ReturnDatabaseInfoResult()
        {
            string connectionString = "Host=localhost;Database=northwind;User Id=postgres;Password=panda13yu7";
            var postgresConnectionProvider = new PostgresDBConnectionProvider();

            var databaseInfo = await postgresConnectionProvider.GetDatabaseInfoAsync(connectionString, default);

            Assert.NotNull(databaseInfo);
            Assert.True(databaseInfo.Tables.Any() is true);
            Assert.True(databaseInfo.Tables.Select(x => x.Columns).Any() is true);
        }
    }
}
