using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnApsDomain
{
    public interface IFacility
    {
        int Id { get; }
        string Level { get; }
        string Number { get; }
        int BuildingId { get; }
        IBuilding Building { get; }
        IEnumerable<IBooking> Bookings { get; }
    }
}
