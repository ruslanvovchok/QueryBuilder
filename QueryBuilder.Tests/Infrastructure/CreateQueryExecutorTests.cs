using QueryBuilder.Infrastucture.Services;
using QueryBuilder.Shared.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBuilder.Tests.Infrastructure
{
    public class CreateQueryExecutorTests
    {
        [Fact]
        public async Task QueryExecutor_Should_ReturnObjectList()
        { 
            //string connectionString = "Host=localhost;Database=northwind;User Id=postgres;Password=panda13yu7";
            //string sqlQuery1 = "select * from employees";
            //string sqlQuery2 = "select * from order_details";
            //string sqlQuery3 = "select * from categories";


            //var queryExecutor = new QueryExecutor();
            //var list = await queryExecutor.ExecuteQueryAsync(connectionString, DataBaseTypes.Postgres, sqlQuery1, default);
            //var list2 = await queryExecutor.ExecuteQueryAsync(connectionString, DataBaseTypes.Postgres, sqlQuery2, default);
            //var list3 = await queryExecutor.ExecuteQueryAsync(connectionString, DataBaseTypes.Postgres, sqlQuery3, default);

            //Assert.NotNull(list);
            //Assert.NotNull(list2);
            //Assert.NotNull(list3);
        }
    }

    public class OrderDetails
    { 
        public int order_id { get; set; }
        public int product_id { get; set; }
        public double unit_price { get; set; }
        public int quantity { get; set; }
        public double discount { get; set; }
    }
}
