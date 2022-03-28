using BHMS.CORE.Contract;
using BHMS.CORE.Models;
using BHMS.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BHMS.Controllers
{
    public class AdminController : Controller
    {

        IRepository<HostelRegistration> context;
      
        public AdminController(IRepository<HostelRegistration> hocontext)
        {

            context = hocontext;










        }
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult StudentsThatAreSignedUp()
        {
            var UsersContext = new ApplicationDbContext();
            
            var tes=UsersContext.Users.ToList();

            List<HostelRegistration> hostels = context.Collection().ToList();

            var currentUserGender = from t in tes

                                   join a in hostels  on t.MatricNo equals a.User //if you are using a string guid, otherwise remove ToString()
                                   where t.MatricNo == a.User
                                   
                                  select new
                                 {
                                 r=a.room,
                                 f=t.MatricNo
                                 };
           
            

            return View(tes);

          
        }
        public ActionResult registeredStudents()
        {
            List<HostelRegistration> registeredStudents = context.Collection().ToList();
            return View(registeredStudents);
        }
        //[Authorize]
        //public ActionResult Edit(string Id)
        //{
        //    var UsersContext = new ApplicationDbContext();
        //    RegisterViewModel modelToEdit  = (RegisterViewModel)new ApplicationDbContext().Users.Where(x => x.Id != null).Select(x => x.Id);
        //    if (modelToEdit == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    else
        //    {


        //        return View(modelToEdit);
        //    }
        //}
        //[HttpPost]
        //public ActionResult Edit(RegisterViewModel model, string Id, HttpPostedFileBase file)
        //{
        //    RegisterViewModel modelToEdit = (RegisterViewModel)new ApplicationDbContext().Users.Where(x => x.Id != null).Select(x => x.Id);
        //    if (modelToEdit == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    else
        //    {

        //        if (!ModelState.IsValid)
        //        {
        //            return View(modelToEdit);
        //        }
        //        if (file != null)
        //        {

        //            modelToEdit.Passport = modelToEdit.Id + Path.GetExtension(file.FileName);
        //            file.SaveAs(Server.MapPath("//Content//PassportImages//") + modelToEdit.Passport);








        //            modelToEdit.FirstName = model.FirstName;
        //            modelToEdit.LastName = model.LastName;
        //            modelToEdit.Gender = model.Gender;
        //            modelToEdit.Level = model.Level;
        //            modelToEdit.Course = model.Course;
                   

        //            context.;
        //        }

        //        return RedirectToAction("Index");

        //    }
        //}
        //[Authorize]
        //public ActionResult Delete(string Id)
        //{
        //    Hostel hostelToDelete = context.Find(Id);

        //    if (hostelToDelete == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    else
        //    {
        //        return View(hostelToDelete);
        //    }
        //}

        //[HttpPost]
        //[ActionName("Delete")]
        //public ActionResult confirmDelete(string Id)
        //{
        //    Hostel hostelToDelete = context.Find(Id);
        //    if (hostelToDelete == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    else
        //    {
        //        context.Delete(Id);
        //        context.Commit();
        //        return RedirectToAction("index");
        //    }
        //}
    }

}