using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using HotelModel;

namespace RESTServiceDatabaseOpgave.DBUtil
{
    public class ManageBooking : IManageBooking
    {
        private SqlConnection connection = Connection.MyConnection();
        public List<Booking> GetAllBookings()
        {
            List<Booking> bookings = new List<Booking>();
            string queryString = "SELECT * FROM Booking";

            using (connection)
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                try
                {
                    while (reader.Read())
                    {
                        Booking booking = new Booking();
                        booking.BookingID = reader.GetInt32(0);
                        booking.Hotel_No = reader.GetInt32(1);
                        booking.Guest_No = reader.GetInt32(2);
                        booking.Date_From = reader.GetDateTime(3);
                        booking.Date_To = reader.GetDateTime(4);
                        booking.Room_No = reader.GetInt32(5);

                        bookings.Add(booking);
                    }
                }
                finally
                {
                    reader.Close();
                    command.Connection.Close();
                }
            }

            return bookings;
        }

        public Booking GetBookingFromID(int bookingId)
        {
            string queryString = $"SELECT * FROM Booking WHERE Booking_Id = {bookingId}";
            Booking booking = new Booking();

            using (connection)
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                try
                {
                    while (reader.Read())
                    {
                        booking.BookingID = reader.GetInt32(0);
                        booking.Hotel_No = reader.GetInt32(1);
                        booking.Guest_No = reader.GetInt32(2);
                        booking.Date_From = reader.GetDateTime(3);
                        booking.Date_To = reader.GetDateTime(4);
                        booking.Room_No = reader.GetInt32(5);
                    }
                }
                finally
                {
                    reader.Close();
                    command.Connection.Close();
                }

                return booking;
            }
        }

        public bool CreateBooking(Booking booking)
        {
            string queryString =
                "INSERT INTO Booking (Hotel_No, Guest_No, Date_From, Date_To, Room_No) VALUES (@HotelNumber, @GuestNumber, @DateFrom, @DateTo, @RoomNumber)";

            using (connection)
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.AddWithValue("@BookingId", booking.BookingID);
                command.Parameters.AddWithValue("@HotelNumber", booking.Hotel_No);
                command.Parameters.AddWithValue("@GuestNumber", booking.Guest_No);
                command.Parameters.AddWithValue("@DateFrom", booking.Date_From);
                command.Parameters.AddWithValue("@DateTo", booking.Date_To);
                command.Parameters.AddWithValue("@RoomNumber", booking.Room_No);

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
                    command.Connection.Close();
                }
            }
        }

        public bool UpdateBooking(Booking booking, int bookingId)
        {
            string queryString =
                $"UPDATE Booking SET Hotel_No = @HotelNumber, Guest_No = @GuestNumber, Date_From = @DateFrom, Date_To = @DateTo, Room_No = @RoomNumber WHERE Booking_id = {bookingId}";

            using (connection)
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.AddWithValue("@HotelNumber", booking.Hotel_No);
                command.Parameters.AddWithValue("@GuestNumber", booking.Guest_No);
                command.Parameters.AddWithValue("@DateFrom", booking.Date_From);
                command.Parameters.AddWithValue("@DateTo", booking.Date_To);
                command.Parameters.AddWithValue("@RoomNumber", booking.Room_No);

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
                    command.Connection.Close();
                }
            }
        }

        public bool DeleteBooking(int bookingId)
        {
            string queryString = $"DELETE FROM Booking WHERE Booking_id = {bookingId}";

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
                    command.Connection.Close();
                }
            }
        }
    }
}