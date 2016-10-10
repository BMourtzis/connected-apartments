using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnApsDomain.Exceptions
{
    public class BookingOverlapingException: Exception
    {
        public BookingOverlapingException() : base("Your Booking is overlaping another booking. Please pick another time") { }
    }
}
