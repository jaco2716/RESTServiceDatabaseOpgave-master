using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HotelModels;
using RESTServiceDatabaseOpgave.DBUtil;

namespace RESTServiceDatabaseOpgave.Controllers
{
    public class HotelController : ApiController
    {
        ManageHotel manageHotel = new ManageHotel();

        public IEnumerable<Hotel> Get()
        {
            return manageHotel.GetAllHotel();
        }

        public Hotel Get(int id)
        {
            return manageHotel.GetHotelFromID(id);
        }

        public bool Post([FromBody]Hotel value)
        {
            return manageHotel.CreateHotel(value);
        }

        public bool Put(int id, [FromBody]Hotel value)
        {
            return manageHotel.UpdateHotel(value, id);
        }

        public bool Delete(int id)
        {
            return manageHotel.DeleteHotel(id);
        }
    }
}
