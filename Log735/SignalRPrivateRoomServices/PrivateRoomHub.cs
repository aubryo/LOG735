using Microsoft.AspNet.SignalR;
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

            return Groups.Add(Context.ConnectionId, roomName);

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

            return Groups.Remove(Context.ConnectionId, roomName);

        }
        public bool AddCourses(Courses courses)
        {
           var dbContext = new LOG735Entities();
            dbContext.Courses.Add(courses);
            dbContext.SaveChanges();
            return true;
        }


        public Task AddEventCourse(string eventCourse,string eventLab)
        {
            var roomName = Context.QueryString["roomName"];
            if (roomName != null)
            {

            }
            return Clients.Group(roomName).newEvent(eventCourse);
           
        }
        public PrivateRooms GetPrivateRoom(string roomName)
        {
            var dbContext = new LOG735Entities();

            dbContext.Configuration.LazyLoadingEnabled = false;
            var res = dbContext.Courses.Where(v => v.PrivateRooms.Where(v2 => v2.RoomName == roomName).Any()).ToList();
         
            var pr = dbContext.PrivateRooms.Where(v => v.RoomName == roomName).FirstOrDefault();
            pr.Courses = res;
            return pr;
            
        }
        //public bool CreateSchedule(Schedules schedule)
        //{
        //    var dbContext = new Log735Context();
        //    dbContext.Schedules.Add(schedule);
        //    dbContext.SaveChanges();

        //    return true;
        //}

        public PrivateRooms CreatePrivateRoom(PrivateRooms privateRoom)
        {
            var dbContext = new LOG735Entities();
            dbContext.PrivateRooms.Add(privateRoom);
            dbContext.SaveChanges();

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

           
            using (var db = new LOG735Entities())
            {
                //// Retrieve user.
                //var user = db.AspNetUsers
                //    .Include(u => u.Rooms)
                //    .SingleOrDefault(u => u.UserName == Context.User.Identity.Name);

                //// If user does not exist in database, must add.
                //if (user == null)
                //{
                //    user = new User()
                //    {
                //        UserName = Context.User.Identity.Name
                //    };
                //    db.Users.Add(user);
                //    db.SaveChanges();
                //}
                //else
                //{
                //    // Add to each assigned group.
                //    foreach (var item in user.Rooms)
                //    {
                //        Groups.Add(Context.ConnectionId, item.RoomName);
                //    }
                //}
            }
            return base.OnConnected();
        }


  
       
    }
}
