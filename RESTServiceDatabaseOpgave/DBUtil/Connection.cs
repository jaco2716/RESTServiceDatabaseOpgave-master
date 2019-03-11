using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RESTServiceDatabaseOpgave.DBUtil
{
    public static class Connection
    {
        private const string ConnectionString = @"Data Source=jacobdbserver.database.windows.net;Initial Catalog=Jacob;User ID=jacob;Password=Jaco2716;Connect Timeout=60;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        //public static readonly SqlConnection MyConnection = new SqlConnection(ConnectionString);
        private static SqlConnection _sqlConnection;
        public static SqlConnection MyConnection()
        {
            if (_sqlConnection == null) return new SqlConnection(ConnectionString);
            else return _sqlConnection;
        }
    }
}