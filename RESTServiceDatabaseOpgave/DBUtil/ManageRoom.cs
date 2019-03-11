using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using HotelModel;

namespace RESTServiceDatabaseOpgave.DBUtil
{
    public class ManageRoom : IManageRoom
    {
        private SqlConnection connection = Connection.MyConnection();

        public List<Room> GetAllRooms()
        {

            List<Room> rooms = new List<Room>();
            string queryString = "SELECT * FROM Room";

            using (connection)
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                try
                {
                    while (reader.Read())
                    {
                        Room room = new Room();
                        room.Room_No = reader.GetInt32(0);
                        room.Hotel_No = reader.GetInt32(1);
                        room.Type = reader.GetString(2).First();
                        room.Price = reader.GetDouble(3);

                        rooms.Add(room);
                    }
                }
                finally
                {
                    reader.Close();
                    command.Connection.Close();
                }
            }
            return rooms;
        }

        public Room GetRoomFromID(int roomNo, int hotelNo)
        {
            string queryString = $"SELECT * FROM Room WHERE Room_No = {roomNo} AND Hotel_No = {hotelNo}";
            Room room = new Room();

            using (connection)
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                try
                {
                    while (reader.Read())
                    {
                        room.Room_No = reader.GetInt32(0);
                        room.Hotel_No = reader.GetInt32(1);
                        room.Type = reader.GetString(2).First();
                        room.Price = reader.GetDouble(3);
                    }
                }
                finally
                {
                    reader.Close();
                    command.Connection.Close();
                }

                return room;
            }
        }

        public bool CreateRoom(Room room)
        {
            string queryString = "INSERT INTO Room (Room_No, Hotel_No, Types, Price) VALUES (@RoomNumber, @HotelNumber, @Type, @Price)";

            using (connection)
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.AddWithValue("@RoomNumber", room.Room_No);
                command.Parameters.AddWithValue("@HotelNumber", room.Hotel_No);
                command.Parameters.AddWithValue("@Type", room.Type);
                command.Parameters.AddWithValue("@Price", room.Price);

                connection.Open();
                try
                {
                    return command.ExecuteNonQuery() == 0 ? false : true;
                }
                catch
                {
                    return false;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public bool UpdateRoom(Room room, int roomNo, int hotelNo)
        {
            string queryString = $"UPDATE Room SET Room_No = @RoomNumber, Hotel_No = @HotelNumber, Types = @Types, Price = @Price WHERE Room_No = {roomNo} AND hotel_No = {hotelNo}";

            using (connection)
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.AddWithValue("@RoomNumber", room.Room_No);
                command.Parameters.AddWithValue("@HotelNumber", room.Hotel_No);
                command.Parameters.AddWithValue("@Types", room.Type);
                command.Parameters.AddWithValue("@Price", room.Price);

                connection.Open();
                try
                {
                    return command.ExecuteNonQuery() == 0 ? false : true;
                }
                catch
                {
                    return false;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public bool DeleteRoom(int roomNo, int hotelNo)
        {
            string queryString = $"DELETE FROM Room WHERE Room_No = {roomNo} AND Hotel_No = {hotelNo}";

            using (connection)
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                try
                {
                    return command.ExecuteNonQuery() == 0 ? false : true;
                }
                catch
                {
                    return false;
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}