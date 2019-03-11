using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelModel;

namespace RESTServiceDatabaseOpgave.DBUtil
{
    interface IManageBooking
    {
        List<Booking> GetAllBookings();

        Booking GetBookingFromID(int bookingId);

        bool CreateBooking(Booking booking);

        bool UpdateBooking(Booking booking, int bookingId);

        bool DeleteBooking(int bookingId);
    }
}
