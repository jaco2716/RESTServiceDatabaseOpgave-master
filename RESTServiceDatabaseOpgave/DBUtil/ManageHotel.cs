using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using HotelModels;

namespace RESTServiceDatabaseOpgave.DBUtil
{
    public class ManageHotel : IManageHotel
    {
        private SqlConnection connection = Connection.MyConnection();

        public List<Hotel> GetAllHotel()
        {
            List<Hotel> hotels = new List<Hotel>();
            string queryString = "SELECT * FROM Hotel";

            using (connection)
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                try
                {
                    while (reader.Read())
                    {
                        Hotel hotel = new Hotel();
                        hotel.Hotel_No = reader.GetInt32(0);
                        hotel.Name = reader.GetString(1);
                        hotel.Address = reader.GetString(2);

                        hotels.Add(hotel);
                    }
                }
                finally
                {
                    reader.Close();
                    command.Connection.Close();
                }
            }

            return hotels;
        }

        public Hotel GetHotelFromID(int hotelNo)
        {
            string queryString = $"SELECT * FROM Hotel WHERE Hotel_No = {hotelNo}";
            Hotel hotel = new Hotel();

            using (connection)
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                try
                {
                    while (reader.Read())
                    {
                        hotel.Hotel_No = reader.GetInt32(0);
                        hotel.Name = reader.GetString(1);
                        hotel.Address = reader.GetString(2);
                    }
                }
                finally
                {
                    reader.Close();
                    command.Connection.Close();
                }
            }

            return hotel;
        }

        public bool CreateHotel(Hotel hotel)
        {
            string queryString = "INSERT INTO Hotel (Hotel_No, Name, Address) VALUES ( @Number , @Name, @Address)";

            using (connection)
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.AddWithValue("@Number", hotel.Hotel_No);
                command.Parameters.AddWithValue("@Name", hotel.Name);
                command.Parameters.AddWithValue("@Address", hotel.Address);

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

        public bool UpdateHotel(Hotel hotel, int hotelNo)
        {
            string queryString = $"UPDATE Hotel SET Hotel_No = @Number, Name = @Name, Address = @Address WHERE Hotel_No = {hotelNo}";

            using (connection)
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.AddWithValue("@Number", hotel.Hotel_No);
                command.Parameters.AddWithValue("@Name", hotel.Name);
                command.Parameters.AddWithValue("@Address", hotel.Address);

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

        public bool DeleteHotel(int hotelNo)
        {
            string queryString = $"DELETE FROM Hotel WHERE Hotel_No = {hotelNo}";

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