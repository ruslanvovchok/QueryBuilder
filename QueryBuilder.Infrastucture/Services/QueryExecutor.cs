using Microsoft.Data.SqlClient;
using QueryBuilder.Shared.Common.Enums;
using QueryBuilder.Shared.Common.Exceptions.Database;
using QueryBuilder.Shared.Common.Exceptions.Types;
using QueryBuilder.Shared.Infrastucture.Abstractions.Services;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace QueryBuilder.Infrastucture.Services
{
    public class QueryExecutor : IQueryExecutor
    {
        private readonly IConnectionExecutor connectionExecutor;
        private ConcurrentDictionary<string, Delegate> mapperFuncs;

        public QueryExecutor(IConnectionExecutor connectionExecutor)
        {
            this.connectionExecutor = connectionExecutor;
            this.mapperFuncs = new();
        }

        public async Task<List<dynamic>> ExecuteQueryAsync(string connectionString, DataBaseTypes dbType, string sqlQuery, CancellationToken cancellationToken)
        {
            List<dynamic> result = new();

            var reader = await connectionExecutor.ConnectAsync(connectionString, dbType, sqlQuery, cancellationToken);
            var lambda = (Func<DbDataReader, dynamic>)mapperFuncs.GetOrAdd(sqlQuery, dynamic => BuildLambda(reader));

            while (await reader.ReadAsync(cancellationToken))
                result.Add(lambda(reader));

            return result;
        }

        private Func<DbDataReader, dynamic> BuildLambda(DbDataReader reader)
        {
            var readerParam = Expression.Parameter(typeof(DbDataReader));
            var createObjectMethod = typeof(ExpandoObject).GetConstructor(Type.EmptyTypes);
            var addPropertyMethod = typeof(IDictionary<string, object>).GetMethod("Add");

            var newExpando = Expression.New(createObjectMethod);
            List<ElementInit> bindings = GetBindings(reader, addPropertyMethod, readerParam);
            var initExpando = Expression.ListInit(newExpando, bindings);

            return Expression.Lambda<Func<DbDataReader, dynamic>>(initExpando, readerParam).Compile();
        }

        private List<ElementInit> GetBindings(DbDataReader reader, MethodInfo? addPropertyMethod, ParameterExpression? readerParam)
        {
            List<ElementInit> bindings = new();

            for (int i = 0; i < reader.FieldCount; i++)
            {
                string name = reader.GetName(i);
                var addProprtyExp = Expression.ElementInit(addPropertyMethod,
                    Expression.Constant(name),
                    Expression.Convert(
                            Expression.Call(readerParam, typeof(DbDataReader).GetMethod("GetValue"), Expression.Constant(i)),
                            typeof(object)
                        ));

                bindings.Add(addProprtyExp);
            }

            return bindings;
        }

    }
}
