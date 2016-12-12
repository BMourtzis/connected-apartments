using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnApsDomain.Exceptions
{
    public class NotFoundException: ConnectedApartmentsException
    {
        public NotFoundException() : base("The Item you requested was not found. Please search again.") { }

        public NotFoundException(string item): base("The "+item+" you requested was not found. Please search again.") { }
    }
}
