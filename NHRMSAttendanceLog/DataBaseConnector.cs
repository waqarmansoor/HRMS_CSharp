using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHRMSAttendanceLog
{
    class DataBaseConnector

    {

        //static string connectionString = "server=localhost;user id=root;password=root;database=nisumhrdb";
        static string connectionString = System.Configuration.ConfigurationManager.AppSettings["connectionString"];
        static MySqlConnection con = new MySqlConnection(connectionString);

        public static MySqlConnection getConnection()
        {
            
            return con;
        }

       

    }
}
