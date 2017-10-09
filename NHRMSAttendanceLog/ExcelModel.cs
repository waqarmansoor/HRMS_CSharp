using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHRMSAttendanceLog
{
    class ExcelModel
    {
        
        String no;
        String hours;
        String status;
        String date;
        String check_in;
        String check_out;
        String name;
        List<String> objectList = new List<String>();

        public ExcelModel(String no, String hours, String status, String date, String check_in,String check_out)
        {
            
            this.no = no;
            this.hours = hours;
            this.Status = status;
            this.date = date;
            this.check_in = check_in;
            this.check_out = check_out;
        }

        public ExcelModel(String no,String name, String hours, String status, String date, String check_in, String check_out)
        {
            this.name = name;
            this.no = no;
            this.hours = hours;
            this.Status = status;
            this.date = date;
            this.check_in = check_in;
            this.check_out = check_out;
        }

        public string Date { get => date; set => date = value; }
        public string Hours { get => hours; set => hours = value; }
        public string No { get => no; set => no = value; }
        public string Check_in { get => check_in; set => check_in = value; }
        public string Check_out { get => check_out; set => check_out = value; }
        public string Status { get => status; set => status = value; }

        public List<String> getList()
        {
            objectList.Add(this.no);
            objectList.Add(this.name);
            objectList.Add(DateTime.Parse(this.date).Month.ToString() + "-" + DateTime.Parse(this.date).Day.ToString() + "-" + DateTime.Parse(this.date).Year.ToString());
            objectList.Add(TimeDate.UnixTimeStampToDateTime(Double.Parse(this.check_in)));
            objectList.Add(TimeDate.UnixTimeStampToDateTime(Double.Parse(this.check_out)));   
            objectList.Add(this.hours);
            objectList.Add(this.status); 
            return objectList;
        }
    }
}
