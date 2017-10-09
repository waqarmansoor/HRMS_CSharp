using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHRMSAttendanceLog
{
    class Attendence
    {
        String emp_no;
        String date;
        String time;
        String status;

        public Attendence(String emp_no,String date,String time,String status)
        {
            this.Emp_no=emp_no;
            this.Date = date;
            this.Time = time;
            this.Status = status;
        }

        public string Emp_no { get => emp_no; set => emp_no = value; }
        public string Date { get => date; set => date = value; }
        public string Time { get => time; set => time = value; }
        public string Status { get => status; set => status = value; }
    }
}
