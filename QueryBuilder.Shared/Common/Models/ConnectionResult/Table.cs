using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBuilder.Shared.Common.Models.ConnectionResult
{
    public class Table
    {
        public string Name { get; set; } = default!;
        public List<Column?> Columns { get; set; } = new();
    }
}
