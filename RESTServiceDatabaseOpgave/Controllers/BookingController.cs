using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HotelModel;
using RESTServiceDatabaseOpgave.DBUtil;

namespace RESTServiceDatabaseOpgave.Controllers
{
    public class BookingController : ApiController
    {
        ManageBooking mngBooking = new ManageBooking();

        public IEnumerable<Booking> Get()
        {
            return mngBooking.GetAllBookings();
        }

        public Booking Get(int id)
        {
            return mngBooking.GetBookingFromID(id);
        }

        public bool Post([FromBody]Booking value)
        {
            return mngBooking.CreateBooking(value);
        }

        public bool Put([FromBody]Booking value, int id)
        {
            return mngBooking.UpdateBooking(value, id);
        }

        public bool Delete(int id)
        {
            return mngBooking.DeleteBooking(id);
        }
    }
}
