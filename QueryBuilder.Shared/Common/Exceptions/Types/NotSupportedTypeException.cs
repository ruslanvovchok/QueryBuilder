using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBuilder.Shared.Common.Exceptions.Types
{
    public class NotSupportedTypeException : DomainException
    {
        public NotSupportedTypeException() 
            : base("The type of data you want to retrieve is not support."){ }
    }
}
