using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHRMSAttendanceLog
{
    class Service
    {
        static List<Attendence> empList = new List<Attendence>();
        static List<Attendence> empListByDate = new List<Attendence>();
        public static List<ExcelModel> employeeWriteList = new List<ExcelModel>();

       

        public static void listParser(List<Attendence> empReadList)
        {
            
            for (int i = 0; i < empReadList.Count(); i++)
            {

                if (empList.Count() == 0)
                {
                    
                    empList.Add(empReadList[i]);

                }
                else if (empList[0].Emp_no==(empReadList[i].Emp_no))
                {
                    empList.Add(empReadList[i]);
                }
                else
                {
                    dateParser();
                    empList.Clear();
                    i--;
                }

            }
        }

      

        private static void dateParser()
        {

            for (int i = 0; i < empList.Count(); i++)
            {

                if (empListByDate.Count() == 0)
                {
                    empListByDate.Add(empList[i]);
                }
                else if (empListByDate[0].Date==(empList[i].Date))
                {
                    empListByDate.Add(empList[i]);
                }
                else
                {
                    
                    objectGenerator();
                    empListByDate.Clear();
                    i--;
                   

                }
            }
           
        }


        private static void objectGenerator()
        {

            String hours = null;
            String status ;
            String check_in = null;
            String check_out = null;
            Attendence attendence;
            attendence = empListByDate[0];

            
           
            if (getCheckIn() != -1)
            {
                check_in = empListByDate[getCheckIn()].Time;
                if (getCheckOut() != -1)
                {
                    check_out = empListByDate[getCheckOut()].Time;
                    hours = TimeDate.getHours(empListByDate[getCheckIn()].Time, empListByDate[getCheckOut()].Time);
                    status = (Double.Parse(hours) < 8.5) ? "Less Hours" : "Normal Hours";
                }
                else
                {
                    check_out = "0:0:0";
                    status = "No check out";
                    hours = "0";
                }

            }
            else
            {
                check_in = "0:0:0";
                status = "No Check In";
                hours = "0";
            }

           
            employeeWriteList.Add(new ExcelModel(attendence.Emp_no, hours, status, attendence.Date,check_in,check_out));


        }

        /**
         * 
         * 
         * @return id of first check in
         */

        private static int getCheckIn()
        {
            

            for (int i = 0; i < empListByDate.Count(); i++)
            {
                if (empListByDate[i].Status=="0")
                    return i;
            }
            return -1;
        }

        /**
         * 
         * 
         * @return id of last checkout
         */
        private static int getCheckOut()
        {
            
            for (int i = empListByDate.Count()-1 ; i > 0; i--)
            {
                
                if (empListByDate[i].Status=="1")
                    return i;
            }
            return -1;
        }

    }
 }
