using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBuilder.Shared.Common.Models.ConnectionResult
{
    public class Column
    {
        public string Name { get; set; } = default!;
        public string Type { get; set; } = default!;
    }
}
