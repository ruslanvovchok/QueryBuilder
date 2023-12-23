using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBuilder.Shared.Common.Exceptions.Database
{
    public class NotSupportedDBTypeException : DomainException
    {
        public NotSupportedDBTypeException()
            : base("") 
        { }
    }
}
