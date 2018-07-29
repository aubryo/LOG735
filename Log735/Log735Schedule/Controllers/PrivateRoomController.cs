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
        public void LoginPrivateRoom(PrivateRoomDomain.Model.PrivateRooms model, string returnUrl)
        {
            PrivateRoomSchedule(model, returnUrl);
        }
        [HttpPost]
        public ActionResult PrivateRoomSchedule(PrivateRoomDomain.Model.PrivateRooms model, string returnUrl)
        {
            return View(model);

        }
    }
}