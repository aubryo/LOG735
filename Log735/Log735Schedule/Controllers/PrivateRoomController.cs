using Log735Schedule.Helper;
using Log735Schedule.Models;
using PrivateRoomDomain.Helper;
using PrivateRoomDomain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

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
      
            var returnModel = model;
            var bdModel = DbContextHelper.RoomExistBD(model);
            //Room exist pas dans bd
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
                    ConnectToHub(hubUrl); //Connect au room 
                else
                    ConnectToAvailableHub(); //Prend le hub le plus libre pour la room

                returnModel = CommunicationHandler.GetPrivateRoom(model.RoomName);
            }

           var fullCourses =  DbContextHelper.GetCourseInfo(returnModel.Courses);
                 
            
            return View(returnModel);

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

        public ActionResult AddCourse()
        {
            return null;
        }


        public ActionResult DeleteCourse()
        {
            return null;
        }
    }
}