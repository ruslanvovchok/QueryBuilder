using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBuilder.Shared.Common.Exceptions.Database
{
    public class NotValidConnectionStringException : DomainException
    {
        public NotValidConnectionStringException()
            : base("Your connection string does not valid. Please, check your connection string and make sure login and password was not empty or invalid.")
        { }
    }
}
