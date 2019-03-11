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
    public class FacilityController : ApiController
    {
        ManageFacility mngFacility = new ManageFacility();

        public IEnumerable<Facility> Get()
        {
            return mngFacility.GetAllFacilities();
        }

        public Facility Get(int id)
        {
            return mngFacility.GetFacilityFromID(id);
        }
        public bool Post([FromBody]Facility value)
        {
            return mngFacility.CreateFacility(value);
        }
        public bool Put(int id, [FromBody]Facility value)
        {
            return mngFacility.UpdateFacility(value, id);
        }
        public bool Delete(int id)
        {
            return mngFacility.DeleteFacility(id);
        }
    }

}
