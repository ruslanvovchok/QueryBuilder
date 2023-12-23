using QueryBuilder.Infrastucture.Services;
using QueryBuilder.Shared.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBuilder.Tests.Infrastructure
{
    public class CreateConnectionProviderTests
    {
        [Fact]
        public async Task Execute_Should_ReturnDatabaseInfoResult()
        {
            string connectionString = "Host=localhost;Database=northwind;User Id=postgres;Password=panda13yu7";
            var connectionExecutor = new ConnectionExecutor();

            var databaseInfo = await connectionExecutor.ExecuteAsync(connectionString, DataBaseTypes.Postgres, default);

            Assert.NotNull(databaseInfo);
            Assert.True(databaseInfo.Tables.Any() is true);
            Assert.True(databaseInfo.Tables.Select(x => x.Columns).Any() is true);
        }
    }
}
