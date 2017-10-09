using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHRMSAttendanceLog
{
    class AttendenceLogDAO
    {
        static MySqlConnection con = DataBaseConnector.getConnection();
        //static MySqlDataReader reader = null;
        static MySqlCommand cmd;
        static String query = null;
        static List<ExcelModel> excelList;
        static List<String> dates;

        public static void saveData(ExcelModel excelModel)
        {
            
            query = "Insert into attendance_log(employee_id,attendance_date,time_in,time_out,time_spent,status,is_wfh) values((select id from employees where employee_code='" + excelModel.No+ "'),str_to_date('" + excelModel.Date+"','%m-%d-%Y'),"+TimeDate.ConvertToUnixTime(DateTime.Parse(excelModel.Date+" "+excelModel.Check_in))+","+TimeDate.ConvertToUnixTime(DateTime.Parse(excelModel.Date+" "+excelModel.Check_out)) +","+Double.Parse(excelModel.Hours)+",'"+excelModel.Status+"',0)";
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

        public static List<String> getDates()
        {
            dates = new List<string>();
            MySqlCommand command = con.CreateCommand();
            command.CommandText = "SELECT  distinct attendance_date FROM nisumhrdb.attendance_log ORDER BY attendance_date ASC";
            con.Open();
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                dates.Add(reader.GetString(0));
            }
            con.Close();

            return dates;
        }

        public static List<ExcelModel> getData(String date)
        {
            excelList = new List<ExcelModel>();
            MySqlCommand command = con.CreateCommand();
            command.CommandText = "SELECT e.employee_code,e.name ,al.attendance_date, al.time_spent, al.status, al.time_in,al.time_out from attendance_log al JOIN employees e ON e.id=al.employee_id where attendance_date=str_to_date('"+date+"','%m/%d/%Y %h:%i:%s')" ;
            con.Open();
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                excelList.Add(new ExcelModel(reader.GetString(0), reader.GetString(1), reader.GetString(3), reader.GetString(4), reader.GetString(2), reader.GetString(5), reader.GetString(6)));
            }
            con.Close();
            return excelList;
        }

        public static String getLastDate()
        {
            String query = "SELECT attendance_date FROM attendance_log ORDER BY attendance_date DESC LIMIT 1";
            String date;
            cmd = con.CreateCommand();
            cmd.CommandText = query;
            con.Open();
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                date = reader.GetString(0);
                con.Close();
                return date;
            }
            else
            {
                con.Close();
                return "0/0/0 12:0:0 AM";
            }
        }

        public static int getId(String employee_code)
        {
            int id;
            MySqlDataReader reader=null;
            String query = "select id from employees where employee_code='" + employee_code + "'";
            MySqlCommand command = con.CreateCommand();
            cmd.CommandText = query;
            //con.Open();
            
            
            if (reader == null)
            {
                reader = cmd.ExecuteReader();
            }
            if (reader.Read())
            {
                id = reader.GetInt32(0);
                //con.Close();
                reader.Close();
                return id;
            }
            else
            {
                //con.Close();
                reader.Close();
                return -1;
            }

        }

    }
}
