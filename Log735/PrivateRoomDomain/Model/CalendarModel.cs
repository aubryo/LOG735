using PrivateRoomDomain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateRoomDomain
{
    public class CalendarModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public string BackgroundColor { get; set; }
        public string BorderColor { get; set; }
        public int EventId { get; set; }
 
        public CalendarModel(CourseInfo data, string courseTitle, string courseName)
        {
         
            Title = courseTitle;
            Description = courseName;
            Start = SetDateFormat(data.StartTime, data.WDay);
            End = SetDateFormat(data.EndTime, data.WDay);
            if (data.BIsLab)
            {
                BackgroundColor = "#00cadb";
                BorderColor = "#000000";
            }
            else
            {
                BackgroundColor = "#007c6d";
                BorderColor = "#000000";
            }

        }

        private string SetDateFormat(string start, string weekDay)
        {
            var week = DateTime.Today.DayOfWeek;
            var diffDay = Int32.Parse(weekDay) - (int)week;

            var courseDay=DateTime.Today.AddDays(diffDay);
            var stringDate = courseDay.ToString("yyyy-MM-dd");
            stringDate = stringDate + "T" + start+":00";

            return stringDate;
        }
    }
}
