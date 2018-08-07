using PrivateRoomDomain.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            var user = dbcontext.AspNetUsers.Where(v => v.UserName == userName).AsNoTracking().First();
            return user;
        }

        public static PrivateRooms RoomExistBD(PrivateRooms model)
        {
            var dbContext = new LOG735Entities();
            var pR = dbContext.PrivateRooms.Where(v => v.RoomName == model.RoomName).AsNoTracking().FirstOrDefault();
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
                var CourseInfo = dbContext.CourseInfo.Where(v => v.CourseInfoId == course.CourseInfoId).AsNoTracking().First();
                var courseinfoLab = dbContext.CourseInfo.Where(v => v.CourseInfoId == course.CourseInfoLabId).AsNoTracking().First();
                course.CourseInfo = CourseInfo;
                course.CourseInfo1 = courseinfoLab;
            }

            return courses;

        }

        public static List<Courses> GetCourseFromAcronym(string acronym)
        {
            var dbContext = new LOG735Entities();
            dbContext.Configuration.LazyLoadingEnabled = false;
            var listCourse = dbContext.Courses.Where(v => v.CourseAcronym == acronym).AsNoTracking().ToList();

            return listCourse;
        }

        public static Courses GetCourseFromId(int id)
        {
            var dbContext = new LOG735Entities();
            dbContext.Configuration.LazyLoadingEnabled = false;

            var course =dbContext.Courses.Where(v => v.CourseId == id).Include(v=>v.CourseInfo).Include(v=>v.CourseInfo1).AsNoTracking().FirstOrDefault();
        

            return course;
        }
    }
}
