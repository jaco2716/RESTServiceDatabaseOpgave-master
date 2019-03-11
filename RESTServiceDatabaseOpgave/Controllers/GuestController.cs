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
    public class GuestController : ApiController
    {
        ManageGuest mngGuest = new ManageGuest();
        // GET: api/Guest
        public IEnumerable<Guest> Get()
        {
            return mngGuest.GetAllGuest();
            // return new string[] { "value1", "value2" };
        }

        // GET: api/Guest/5
        public Guest Get(int id)
        {
            return mngGuest.GetGuestFromId(id);
            // return "value";
        }

        // POST: api/Guest
        public bool Post([FromBody]Guest value)
        {
            return mngGuest.CreateGuest(value);
        }

        // PUT: api/Guest/5
        public bool Put(int id, [FromBody]Guest value)
        {
            return mngGuest.UpdateGuest(value, id);
        }

        // DELETE: api/Guest/5
        public bool Delete(int id)
        {
            return mngGuest.DeleteGuest(id);
        }
    }
}
