using Microsoft.AspNet.SignalR;
using PrivateRoomDomain;
using PrivateRoomDomain.Helper;
using PrivateRoomDomain.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
namespace SignalRPrivateRoomServices
{
    public class PrivateRoomHub : Hub
    {
        public static class UserHandler
        {
            public static HashSet<string> ConnectedIds = new HashSet<string>();
            public static Dictionary<string,int> ActiveGroups = new Dictionary<string, int>();
        }
       
        public bool GetExistingRoom(string roomName)
        {
            if (UserHandler.ActiveGroups.Where(v => v.Key == roomName).Any())
            {
                return true;
            }

            return false ;
        }
       public void newEvent(string events)
       {
         
            var r = events;
            Clients.All.newEvent(events);
       }
        public Task SubcribeToRoom(string roomName)
        {
            if (!UserHandler.ActiveGroups.Where(v => v.Key == roomName).Any())
            {
                UserHandler.ActiveGroups.Add(roomName, 1);
               
            }
            else
            {
                UserHandler.ActiveGroups[roomName] = UserHandler.ActiveGroups[roomName] + 1;
            }
            Groups.Add(Context.ConnectionId, roomName);

            var listCalendarEvent = GetPrivateRoomCourseEventCalendar(roomName);
            return Clients.Caller.initiateFullCalendar(listCalendarEvent);

        }
        private List<CalendarModel> GetPrivateRoomCourseEventCalendar(string roomName)
        {
            var privateRoom = GetPrivateRoom(roomName);
            var fullCourse =  DbContextHelper.GetCourseInfo(privateRoom.Courses);
            List<CalendarModel> listCalendarEvent = new List<CalendarModel>();
            foreach (var course in fullCourse)
            {
                var courseTitle = $"{course.CourseAcronym} ({course.GroupNumber})";
                var courseEvent = new CalendarModel(course.CourseInfo, courseTitle, course.CourseName);
                courseEvent.EventId = course.CourseId;
                var labEvent = new CalendarModel(course.CourseInfo1, courseTitle, course.CourseName);
                labEvent.EventId = course.CourseId;
                listCalendarEvent.Add(courseEvent);
                listCalendarEvent.Add(labEvent);
            }
            return listCalendarEvent;
        }
        public Task LeaveRoom(string roomName)
        {
            if (UserHandler.ActiveGroups.Where(v => v.Key == roomName).First().Value == 1)
            {
                UserHandler.ActiveGroups.Remove(roomName);

            }
            else
            {
                UserHandler.ActiveGroups[roomName] = UserHandler.ActiveGroups[roomName] - 1;
            }
            Groups.Remove(Context.ConnectionId, roomName);
            var userName = Context.QueryString["userName"];
            return Clients.Group(roomName).notifyLeavingRoom(userName);

        }

        public Task RemoveEventCourse(string courseId)
        {

            var roomName = Context.QueryString["roomName"];
            var dbContext = new LOG735Entities();
            var pr = dbContext.PrivateRooms.Where(v => v.RoomName == roomName).AsNoTracking().FirstOrDefault();
            if (pr == null)
                throw new Exception("No privateroom with name");
            if (roomName != null)
            {
                var id = Int32.Parse(courseId);
                var course = dbContext.Courses.Where(v => v.CourseId == id).AsNoTracking().FirstOrDefault();

                if (course != null)
                {
                    dbContext.Courses.Attach(course);
                    dbContext.PrivateRooms.Attach(pr);
                    pr.Courses.Remove(course);
                    dbContext.SaveChanges();
                    dbContext.Entry(course).State = EntityState.Detached;
                    dbContext.Entry(pr).State = EntityState.Detached;
                    return Clients.Group(roomName).removeEvent(course.CourseId, course.CourseName);



                }
            }

            return Clients.Group(roomName).errorEvent("Course n'existe pas");
        }
        public Task AddEventCourse(string eventCourseId)
        {

            var roomName = Context.QueryString["roomName"];
            var dbContext = new LOG735Entities();
            var pr = dbContext.PrivateRooms.Where(v => v.RoomName == roomName).AsNoTracking().FirstOrDefault();
            if (pr == null)
                throw new Exception("No privateroom with name");
            if (roomName != null)
            {
                var id = Int32.Parse(eventCourseId);
                var course = dbContext.Courses.Where(v => v.CourseId == id).Include(v => v.CourseInfo).Include(v => v.CourseInfo1).AsNoTracking().FirstOrDefault();
                if (course != null)
                {
                    var courseTitle = $"{course.CourseAcronym} ({course.GroupNumber})";
                    var eventCalendarCourse = GetModelEventCalendarFromCourseInfo(course.CourseInfo, courseTitle, course.CourseName);
                    eventCalendarCourse.EventId = course.CourseId;
                    var eventCalendarLab = GetModelEventCalendarFromCourseInfo(course.CourseInfo1, courseTitle, course.CourseName);
                    eventCalendarLab.EventId = course.CourseId;

                    if (!pr.Courses.Where(v => v.CourseId == id).Any())
                    {
                        dbContext.Courses.Attach(course);
                        dbContext.PrivateRooms.Attach(pr);
                        pr.Courses.Add(course);

                        dbContext.SaveChanges();
                        dbContext.Entry(course).State = EntityState.Detached;
                        dbContext.Entry(pr).State = EntityState.Detached;
                        return Clients.Group(roomName).newEvent(eventCalendarCourse, eventCalendarLab);
                    }
                    return Clients.Group(roomName).notifyWrongAction("Le cours est présent dans le calendrier");

                }
            }

            return Clients.Group(roomName).errorEvent("Course n'existe pas");

        }
        private CalendarModel GetModelEventCalendarFromCourseInfo(CourseInfo info,string courseTitle, string courseName)
        {
            var calendarModel = new CalendarModel(info, courseTitle, courseName);
       
            return calendarModel;


        }
        public PrivateRooms GetPrivateRoom(string roomName)
        {
            var dbContext = new LOG735Entities();

            dbContext.Configuration.LazyLoadingEnabled = false;
            var res = dbContext.Courses.Where(v => v.PrivateRooms.Where(v2 => v2.RoomName == roomName).Any()).AsNoTracking().ToList();
         
            var pr = dbContext.PrivateRooms.Where(v => v.RoomName == roomName).AsNoTracking().FirstOrDefault();
            pr.Courses = res;
            return pr;
            
        }
      

        public PrivateRooms CreatePrivateRoom(PrivateRooms privateRoom)
        {
            var dbContext = new LOG735Entities();
            dbContext.PrivateRooms.Add(privateRoom);
            dbContext.SaveChanges();
            dbContext.Entry(privateRoom).State = EntityState.Detached;
            return privateRoom;

        }

        public bool CreateActiveRoom(ActiveRooms activeRoom)
        {
            var dbContext = new LOG735Entities();
            dbContext.ActiveRooms.Add(activeRoom);
            dbContext.SaveChanges();

            return true;

        }
        public int GetNumberClientOnline()
        {

            return UserHandler.ConnectedIds.Count;

        }

        


        public override Task OnDisconnected(bool stoped)
        {
            UserHandler.ConnectedIds.Remove(Context.ConnectionId);
            var roomName = Context.QueryString["roomName"];
            if (roomName != null)
            {
                LeaveRoom(roomName);
            }
               
            return base.OnDisconnected(stoped);
        }

        
        public override Task OnConnected()
        {
            UserHandler.ConnectedIds.Add(Context.ConnectionId);
     
            var roomName = Context.QueryString["roomName"];
            if (roomName != null)
            {
                var userName = Context.QueryString["userName"];
                SubcribeToRoom(roomName);
                Clients.Group(roomName).NotifyConnectionToRoom(userName);

          
            }

           
           
            return base.OnConnected();
        }


  
       
    }
}
