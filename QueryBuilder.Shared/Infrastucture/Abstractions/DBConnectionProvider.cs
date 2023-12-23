using QueryBuilder.Shared.Common.Models.ConnectionResult;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBuilder.Shared.Infrastucture.Abstractions
{
    public abstract class DBConnectionProvider<T> where T : IDbConnection
    {
        public abstract Task<Database> GetDatabaseInfoAsync(string connectionString, CancellationToken cancellationToken);
        public abstract Task<T> CreateConnectionAsync(string connectionString, CancellationToken cancellationToken);
    }
}
