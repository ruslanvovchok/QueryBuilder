using QueryBuilder.SQLServer.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBuilder.Tests.Infrastructure
{
    public class CreateConnectionExecutorToSQLServerTests
    {
        [Fact]
        public async Task GetDatabaseInfo_Should_ReturnDatabaseInfoResult()
        {
            string connectionString = "Server=MUKM0520\\SQLEXPRESS;Database=NPIBuildBookDigitalization;Trusted_Connection=True";
            var sqlServerConnectionProvider = new SqlServerConnectionProvider();

            var database = await sqlServerConnectionProvider.GetDatabaseInfoAsync(connectionString, default);

            Assert.NotNull(database);
            Assert.True(database.Tables.Any() is true);
            Assert.True(database.Tables.Select(x => x.Columns).Any() is true);
        }
    }
}
