using Microsoft.AspNetCore.SignalR;
using QueryBuilder.API.Hubs.Abstractions;

namespace QueryBuilder.API.Hubs
{
    internal class ReportsDataHub : Hub<IReportsDataClient>
    {
    }
}
