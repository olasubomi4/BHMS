using BHMS.CORE.Contract;
using BHMS.CORE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BHMS.Controllers
{
    public class UserComplaintController : Controller
    {
        IRepository<UserComplaints> context;

        public UserComplaintController(IRepository<UserComplaints> Context)
        {
            this.context = Context;
        }

        // GET: UserComplaint
        public ActionResult Index()
        {
            List<UserComplaints> userComplaints = context.Collection().ToList();
            return View(userComplaints);
        }

        public ActionResult ShowSuccess()
        {

            return View();
        }

        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {

            UserComplaints userComplaints = new UserComplaints();

            return View(userComplaints);
        }

        [HttpPost]
        public ActionResult Create(UserComplaints userComplaints)
        {
            if (!ModelState.IsValid)
            {
                return View(userComplaints);
            }
            else
            {
                userComplaints.studentEmail = User.Identity.Name;

                context.Insert(userComplaints);
                context.Commit();

                return RedirectToAction("ShowSuccess");
            }
        }
    }
}