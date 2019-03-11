using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using HotelModel;

namespace RESTServiceDatabaseOpgave.DBUtil
{
    public class ManageGuest : IManageGuest
    {
        private SqlConnection connection = Connection.MyConnection();


        public List<Guest> GetAllGuest()
        {
            List<Guest> GuestList = new List<Guest>();
            string queryString = "SELECT * FROM Guest";
           

            using (connection)
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                //command.ExecuteNonQuery();   //alternativt command.ExecuteReader   //ved SELECT
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        Guest guest = new Guest();
                        guest.Guest_No = reader.GetInt32(0);
                        guest.Name = reader.GetString(1);
                        guest.Address = reader.GetString(2);
                        GuestList.Add(guest);
                    }
                }
                finally
                {
                    reader.Close();
                    connection.Close();
                }
                
            }

            return GuestList;
        }

        public Guest GetGuestFromId(int Guest_No)
        {
            string queryString = "SELECT * FROM Guest WHERE Guest_No = " + Guest_No;

            Guest guest = new Guest();

            using (connection)
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                //command.ExecuteNonQuery();   //alternativt command.ExecuteReader   //ved SELECT
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        guest.Guest_No = reader.GetInt32(0);
                        guest.Name = reader.GetString(1);
                        guest.Address = reader.GetString(2);
                    }
                }
                finally
                {
                    reader.Close();
                    connection.Close();
                }

            }

            return guest;
        }

        public bool CreateGuest(Guest guest)
        {
            string queryString = "INSERT INTO Guest (Guest_No, Name, Address) VALUES ( @Number , @Name, @Address)";

            using (connection)
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.AddWithValue("@Number", guest.Guest_No);
                command.Parameters.AddWithValue("@Name", guest.Name);
                command.Parameters.AddWithValue("@Address", guest.Address);

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

        public bool UpdateGuest(Guest guest, int Guest_No)
        {
            //string queryString = "INSERT INTO Guest (Guest_No, Name, Address) VALUES ( @Number , @Name, @Address)";
            string queryString = "UPDATE Guest SET Guest_No = @Number, Name = @Name, Address = @Address WHERE Guest_No = " + Guest_No;

            using (connection)
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.AddWithValue("@Number", guest.Guest_No);
                command.Parameters.AddWithValue("@Name", guest.Name);
                command.Parameters.AddWithValue("@Address", guest.Address);

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

        public bool DeleteGuest(int Guest_No)
        {
            string queryString = "DELETE FROM Guest WHERE Guest_No = " + Guest_No;
            //string queryString = "SELECT * FROM Guest WHERE Guest_No = " + Guest_No;

            Guest guest = new Guest();

            using (connection)
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                //command.ExecuteNonQuery();   //alternativt command.ExecuteReader   //ved SELECT
                SqlDataReader reader = command.ExecuteReader();
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