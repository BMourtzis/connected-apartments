using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnApsDomain
{
    public interface IBooking
    {
        int Id { get; }
        int FacilityId { get;}
        int PersonId { get; }
        DateTime StartTime { get; }
        DateTime EndTime { get; }
        IFacility Facility { get; }
        IPerson Person { get; }
    }
}
