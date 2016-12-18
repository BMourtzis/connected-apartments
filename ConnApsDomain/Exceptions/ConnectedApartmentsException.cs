using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnApsDomain.Exceptions
{
    public class ConnectedApartmentsException: Exception
    {
        protected ConnectedApartmentsException(string message): base(message) { }
    }
}
