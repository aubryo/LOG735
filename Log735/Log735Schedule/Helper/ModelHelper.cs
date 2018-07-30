using PrivateRoomDomain.Helper;
using PrivateRoomDomain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Log735Schedule.Helper
{
    public static class ModelHelper
    {

        public static PrivateRooms PrivateRoomModel(PrivateRooms privateRoom)
        {
            
            var name = HttpContext.Current.User.Identity.Name;
            var user = DbContextHelper.GetUser(name);
            privateRoom.OwnerId = user.Id;

         

            return privateRoom;
        }

        
    }
}