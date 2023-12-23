using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBuilder.Shared.Common.Models.ConnectionResult
{
    public class Database
    {
        public string? Name { get; set; } = default!;
        public List<Table?> Tables { get; set; } = new();
    }
}
