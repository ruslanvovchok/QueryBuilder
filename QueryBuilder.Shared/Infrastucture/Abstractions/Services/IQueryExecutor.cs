using QueryBuilder.Shared.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBuilder.Shared.Infrastucture.Abstractions.Services
{
    public interface IQueryExecutor
    {
        Task<List<dynamic>> ExecuteQueryAsync(string connectionString, DataBaseTypes dbType, string sqlQuery, CancellationToken cancellationToken);
    }
}
