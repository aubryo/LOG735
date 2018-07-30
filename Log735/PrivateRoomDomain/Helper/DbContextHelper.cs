using PrivateRoomDomain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateRoomDomain.Helper
{
    public static class DbContextHelper
    {

        public static AspNetUsers GetUser(string userName)
        {
            var dbcontext = new LOG735Entities();
            var user = dbcontext.AspNetUsers.Where(v => v.UserName == userName).First();
            return user;
        }

        public static PrivateRooms RoomExistBD(PrivateRooms model)
        {
            var dbContext = new LOG735Entities();
            var pR = dbContext.PrivateRooms.Where(v => v.RoomName == model.RoomName).FirstOrDefault();
            if (pR == null)
                pR = model;


            return pR;
        }

        public static ICollection<Courses> GetCourseInfo(ICollection<Courses> courses)
        {
            var dbContext = new LOG735Entities();
            dbContext.Configuration.LazyLoadingEnabled = false;
            foreach (var course in courses)
            {
                var CourseInfo = dbContext.CourseInfo.Where(v => v.CourseInfoId == course.CourseInfoId).First();
                var courseinfoLab = dbContext.CourseInfo.Where(v => v.CourseInfoId == course.CourseInfoLabId).First();
                course.CourseInfo = CourseInfo;
                course.CourseInfo1 = courseinfoLab;
            }

            return courses;
            
        }
    }
}
