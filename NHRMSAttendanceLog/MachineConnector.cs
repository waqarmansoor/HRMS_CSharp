using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHRMSAttendanceLog
{
    class MachineConnector
    {
        static int iMachineNumber = 1;
        static string sdwEnrollNumber = "";
        //static int idwTMachineNumber = 0;
        //static int idwEMachineNumber = 0;
        static int idwVerifyMode = 0;
        static int idwInOutMode = 0;
        static int idwYear = 0;
        static int idwMonth = 0;
        static int idwDay = 0;
        static int idwHour = 0;
        static int idwMinute = 0;
        static int idwSecond = 0;
        static int idwWorkcode = 0;

        //static int idwErrorCode = 0;
        static int iGLCount = 0;
        static int iIndex = 0;
        

        public static void storeData(String ip)
        {


            String query=null;
            String ipAddress = "";
            String port = "4370";
            bool bIsConnected = false;
            //Create Standalone SDK class dynamicly.
            zkemkeeper.CZKEMClass axCZKEM1 = new zkemkeeper.CZKEMClass();
            ipAddress = ip;

            //if (args != null && args.Length > 0)
            //    ipAddress = args[0];
            //else
            //{
            //    Console.WriteLine("No IP Address provided. Application will exit!");
            //    System.Environment.Exit(0);

            //}

            Console.WriteLine("Connecting to  " + ipAddress + " using port " + port + " ....");
            bIsConnected = axCZKEM1.Connect_Net(ipAddress, Convert.ToInt32(port));
            string lastDate = AttendenceLoaderDAO.getLastRow();
            String date;
            String time;
            
            

            if (bIsConnected)
            {
                Console.WriteLine("Connected successfully to the device. ");
                Console.WriteLine("Retrieving attendance logs ... ");
                AttendenceLoaderDAO.openConnection();
                

                if (axCZKEM1.ReadGeneralLogData(iMachineNumber))//read all the attendance records to the memory
                {
                    while (axCZKEM1.SSR_GetGeneralLogData(iMachineNumber, out sdwEnrollNumber, out idwVerifyMode,
                               out idwInOutMode, out idwYear, out idwMonth, out idwDay, out idwHour, out idwMinute, out idwSecond, ref idwWorkcode))//get records from the memory
                    {
                        iGLCount++;
                        date = idwMonth.ToString() + "-" + idwDay.ToString() + "-" + idwYear.ToString();
                        time = idwHour.ToString() + ":" + idwMinute.ToString() + ":" + idwSecond.ToString();


                        //Console.WriteLine("Success");
                        if (lastDate == "false" && TimeDate.ConvertToUnixTime(DateTime.Parse(date + " " + time)) < TimeDate.ConvertToUnixTime(DateTime.Now.Date))
                        {
                            Console.WriteLine("New");
                            query = "insert into attendence_loader(employee_code,attendance_date,attendance_time,check_in_out) Values('" + sdwEnrollNumber + "','" +date+"','" +time + "'," + idwInOutMode + ")";
                            AttendenceLoaderDAO.insertAttendence(query);
                        }
                        else if(lastDate!="false" && TimeDate.ConvertToUnixTime(DateTime.Parse(date + " " + time))>long.Parse(lastDate)&& TimeDate.ConvertToUnixTime(DateTime.Parse(date + " " + time))<TimeDate.ConvertToUnixTime(DateTime.Now.Date))
                        {
                            Console.WriteLine("After");
                            query = "insert into attendence_loader(employee_code,attendance_date,attendance_time,check_in_out) Values('" + sdwEnrollNumber + "','" +date + "','" +time + "'," + idwInOutMode + ")";
                            AttendenceLoaderDAO.insertAttendence(query);
                        }

                            
                            
                        
                       
                        
                        Console.WriteLine(iGLCount.ToString() + "---" + sdwEnrollNumber +
                                "---" + idwVerifyMode +
                                "---" + idwInOutMode +
                                "---" + idwYear.ToString() + "-" + idwMonth.ToString() + "-" + idwDay.ToString() + " " + idwHour.ToString() + ":" + idwMinute.ToString() + ":" + idwSecond.ToString() +
                                "---" + idwWorkcode);
                        



                        iIndex++;
                    }
                }
            }
            AttendenceLoaderDAO.closeConnection();
            axCZKEM1.Disconnect();
            //Console.WriteLine("Press any key to exit...");
            //Console.ReadLine();

        }
    }
}
