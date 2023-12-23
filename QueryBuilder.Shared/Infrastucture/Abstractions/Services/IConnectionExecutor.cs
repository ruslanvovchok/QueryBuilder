using Microsoft.Data.SqlClient;
using Npgsql;
using QueryBuilder.Shared.Common.Enums;
using QueryBuilder.Shared.Common.Models.ConnectionResult;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBuilder.Shared.Infrastucture.Abstractions.Services
{
    public interface IConnectionExecutor
    {
        Task<DbDataReader> ConnectAsync(string connectionString, DataBaseTypes dbType, string query, CancellationToken cancellationToken);
        Task<Database?> ExecuteAsync(string connectionString, DataBaseTypes dbType, CancellationToken cancellationToken);
    }
}
