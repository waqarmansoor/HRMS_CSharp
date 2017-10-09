using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHRMSAttendanceLog
{
    class TimeDate
    {


        public static long ConvertToUnixTime(DateTime datetime)
        {
            DateTime sTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            return (long)(datetime - sTime).TotalSeconds;
        }

        public static String UnixTimeStampToDateTime(double unixTimeStamp)
        {
            
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp);
            return dtDateTime.TimeOfDay.ToString();
        }



        public static String getHours(String startTime, String endTime)
        {
            double hours;
            double minutes;

           
                hours = DateTime.Parse(endTime).Hour - DateTime.Parse(startTime).Hour;
                minutes = DateTime.Parse(endTime).Minute - DateTime.Parse(startTime).Minute;
                 //if (hours < 0)
                 //{
                 //   hours=24- DateTime.Parse(startTime).Hour;
                 //   hours += DateTime.Parse(endTime).Hour;
                 //}
                if ((minutes) < 1)
                {
                    hours -= 1;
                    minutes += 60;
                    
                }
                minutes *= 0.016;
                hours += minutes;
                return hours.ToString();


        }
            
           
        }
    
}
