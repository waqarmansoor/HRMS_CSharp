
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHRMSAttendanceLog
{
    class AttendenceLoaderDAO
    {
        
        static MySqlConnection con = DataBaseConnector.getConnection();
        static MySqlCommand cmd;
        static List<Attendence> attendenceList;
        public static  void insertAttendence(String query)
        {   
            cmd = new MySqlCommand(query, con);
            cmd.ExecuteNonQuery();
        }

        public static void openConnection()
        {
            con.Open();
        }

        public static void closeConnection()
        {
            con.Close();
        }

        public static List<Attendence> getAttendence(String date)
        {
            
            attendenceList = new List<Attendence>();
            MySqlCommand command = con.CreateCommand();
            String query = "SELECT employee_code,attendance_date,attendance_time,check_in_out FROM attendence_loader where str_to_date(concat(attendance_date,' ','12:00:00 AM'),'%m-%d-%Y %h:%i:%s') > str_to_date('" + date + "','%m/%d/%Y %h:%i:%s') ORDER BY employee_code ASC,attendance_date ASC";
            command.CommandText =query;
            con.Open();
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                attendenceList.Add(new Attendence(reader.GetString(0),reader.GetString(1),reader.GetString(2),reader.GetString(3)));
               // Console.Write(reader.GetString(0)+" "+reader.GetString(2)+" "+" "+ reader.GetString(3)+" \t"+reader.GetString(1)+"\n");
            }
            con.Close();
            return attendenceList;
        }


        

        public static string getLastRow()
        {
            String query = "SELECT * FROM attendence_loader ORDER BY id DESC LIMIT 1";
            String date;
            cmd = con.CreateCommand();
            cmd.CommandText = query;
            con.Open();
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                date=reader.GetString(2)+" "+reader.GetString(3);
                
                con.Close();
                return TimeDate.ConvertToUnixTime(DateTime.Parse(date)).ToString();
            }
            else
            {
                con.Close();
                return "false";
            }
        }
    }
}
