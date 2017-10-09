using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Globalization;
using System.IO;

namespace NHRMSAttendanceLog
{
    class Program
    {


        static void Main(string[] args)
        {
            /*
             
             Fetching Data from Machine
             
             */


            //if (args != null && args.Length > 0 )
            //{
                //MachineConnector.storeData(args[0]);
                MachineConnector.storeData(System.Configuration.ConfigurationManager.AppSettings["ip"]);
            //}
            //else
            //{
            //    Console.Write("No IP is Provided\n");
            //}


            /*
             * Creating Attendence Sheet Directory
             * 
             * 
             */

            string root = @"C:\Attendence Sheets";
            if (!Directory.Exists(root))
            {
                Directory.CreateDirectory(root);
            }


            /*
             List Parser
             
             */

            List<Attendence> list = AttendenceLoaderDAO.getAttendence(AttendenceLogDAO.getLastDate());

            Service.listParser(list);


            /*
             Saving Records in Attendence Log
             
             */

            AttendenceLogDAO.openConnection();
            List<ExcelModel> mylist = Service.employeeWriteList;
            foreach (ExcelModel model in mylist)
            {
                if (AttendenceLogDAO.getId(model.No) != -1)
                {

                    AttendenceLogDAO.saveData(model);

                    Console.Write("Inserted Record\n");
                }


            }

            AttendenceLogDAO.closeConnection();


            



            

            /*
             Excel Generation
             
             
             */

            



            List<String> dates = AttendenceLogDAO.getDates();

           

            List<ExcelModel> finalList;
            foreach (String date in dates)
            {
                if (!File.Exists("c:\\Attendence Sheets\\" + DateTime.Parse(date).Month.ToString() + "-" + DateTime.Parse(date).Day.ToString() + "-" + DateTime.Parse(date).Year.ToString() + ".xls") )
                {
                    finalList = AttendenceLogDAO.getData(date);
                    ExcelSheetController.ExcelGenerator(finalList);
                }
                else
                {
                    Console.Write(date + " Already Exists\n");
                }

            }
            Console.Read();


        }
    }
    
   
}

