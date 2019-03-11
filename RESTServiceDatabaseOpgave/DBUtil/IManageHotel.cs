using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelModel;

namespace RESTServiceDatabaseOpgave.DBUtil
{
    interface IManageHotel
    {
        List<Hotel> GetAllHotel();

        Hotel GetHotelFromID(int hotelNo);

        bool CreateHotel(Hotel hotel);

        bool UpdateHotel(Hotel hotel, int hotelNo);

        bool DeleteHotel(int hotelNo);
    }
}
