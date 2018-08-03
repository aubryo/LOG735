using PrivateRoomDomain.Helper;
using PrivateRoomDomain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Log735Schedule.Models
{
    public class PrivateRoomModel : PrivateRooms
    {
        //public PrivateRoomModel(PrivateRooms privateRoom)
        //{
        //    this.RoomName = privateRoom.RoomName;
        //    this.RoomPassword = privateRoom.RoomPassword;
        //    var name = HttpContext.Current.User.Identity.Name;
        //    var user = DbContextHelper.GetUser(name);
        //    this.OwnerId = user.Id;


        //}
        public string HubUrl { get; set; }

        public PrivateRoomModel()
        {


        }
        public void From(PrivateRooms model)
        {
            
            Courses = model.Courses;
            OwnerId = model.OwnerId;
            RoomId = model.RoomId;
            RoomName = model.RoomName;
            RoomPassword = model.RoomPassword;
            AspNetUsers = model.AspNetUsers;



        }
        public PrivateRooms To(PrivateRoomModel model)
        {
            PrivateRooms pR = new PrivateRooms();
            pR.Courses = model.Courses;
            pR.OwnerId = model.OwnerId;
            pR.RoomId = model.RoomId;
            pR.RoomName = model.RoomName;
            pR.RoomPassword = model.RoomPassword;
            pR.AspNetUsers = model.AspNetUsers;

            return pR;

        }

    }
}