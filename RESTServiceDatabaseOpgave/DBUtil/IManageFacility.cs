using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelModel;

namespace RESTServiceDatabaseOpgave.DBUtil
{
    interface IManageFacility
    {
        List<Facility> GetAllFacilities();


        Facility GetFacilityFromID(int hotelNo);


        bool CreateFacility(Facility facility);


        bool UpdateFacility(Facility facility, int hotelNo);


        bool DeleteFacility(int hotelNo);
    }
}
