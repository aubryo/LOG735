using Log735Schedule.Helper;
using Log735Schedule.Models;
using PrivateRoomDomain;
using PrivateRoomDomain.Helper;
using PrivateRoomDomain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Log735Schedule.Controllers
{
    [Authorize]
    public class PrivateRoomController : Controller
    {

        public ActionResult LoginPrivateRoom(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        public void LoginPrivateRoom(PrivateRooms model, string returnUrl)
        {
       
            PrivateRoomSchedule(model, returnUrl);
        }
        [HttpPost]
        public ActionResult PrivateRoomSchedule(PrivateRooms model, string returnUrl)
        {
         
            var returnModel = new PrivateRoomModel();
            var bdModel = DbContextHelper.RoomExistBD(model);
            //Room n'exist pas dans bd
            if (bdModel.RoomId == 0)
            {
                ModelHelper.PrivateRoomModel(model);
                ConnectToAvailableHub();
                returnModel = CommunicationHandler.CreatePrivateRoom(model);
                
            }
            else //Regarde si la room est en cours
            {
                var hubUrl = GetHubRoomExist(model.RoomName);
                if (hubUrl != "no")
                    ConnectToHub(hubUrl); //Connexion au room 
                else
                    ConnectToAvailableHub(); //Prend le hub le plus libre pour la room

                returnModel = CommunicationHandler.GetPrivateRoom(model.RoomName);
            }
          
            returnModel.UserName = System.Web.HttpContext.Current.User.Identity.Name; //User name de l'utilisateur connecter
            returnModel.SetListCourse(); //La liste des cours pour le dropdown de cours
            CommunicationHandler.DisposeConnection(); //Arrete la connexion, inutile de la garder ouverte.
            return View(returnModel);

        }

        //Population du dropdownList pour les groups pour un cours
        public JsonResult GetCourseGroup(string id)
        {
            
            var list =DbContextHelper.GetCourseFromAcronym(id);
            List<SelectListItem> ListCourses = new List<SelectListItem>();
            foreach (var course in list)
            {
                ListCourses.Add(new SelectListItem {
                    Text = course.CourseName + " (" +course.GroupNumber+ " )",
                    Value = course.CourseId.ToString()

                });
                
            }
            return Json(new SelectList(ListCourses, "Value", "Text"));
        }

        //Information du cours selon le groupe du cours choisit
        public string GetCourseInfo(string id)
        {
            if (id == "")
            {
                return "";
            }
            var list = DbContextHelper.GetCourseFromId(Int32.Parse(id));
            var courseTitle = $"{list.CourseAcronym} ({list.GroupNumber})";
            var courseModel = new CalendarModel(list.CourseInfo, courseTitle, list.CourseName);
            
            var labModel = new CalendarModel(list.CourseInfo1, courseTitle, list.CourseName);
            JavaScriptSerializer serialize = new JavaScriptSerializer();
            var listCalendarModel = new List<CalendarModel>();
            listCalendarModel.Add(courseModel);
            listCalendarModel.Add(labModel);
            var listSerialized = serialize.Serialize(listCalendarModel);
            return listSerialized;
        }
        private void ConnectToHub(string hubUrl)
        {
            CommunicationHandler.SetCurrentHubConnection(hubUrl);
        }

        public string GetHubRoomExist(string roomName)
        {
            var exist = CommunicationHandler.GlobalInvokeFindRightHub("GetExistingRoom", roomName);

               return exist;
        }
        public void ConnectToAvailableHub()
        {

            CommunicationHandler.SetAvailableHubConnection();


        }

    }
}