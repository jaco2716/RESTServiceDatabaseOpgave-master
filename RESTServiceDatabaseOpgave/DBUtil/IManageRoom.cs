using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelModels;

namespace RESTServiceDatabaseOpgave.DBUtil
{
    interface IManageRoom
    {
        List<Room> GetAllRooms();

        Room GetRoomFromID(int roomNo, int hotelNo);

        bool CreateRoom(Room room);

        bool UpdateRoom(Room room, int roomNo, int hotelNo);

        bool DeleteRoom(int roomNo, int hotelNo);
    }
}
