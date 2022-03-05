using BHMS.CORE.Contract;
using BHMS.CORE.Models;
using BHMS.CORE.ViewModels;
using BHMS.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BHMS.Controllers
{
    public class HostelRegistrationController : Controller

    {
        IRepository<HostelRegistration> context;
        IRepository<Hostel> hostelcon;

        ApplicationDbContext fcontext;
        

        public HostelRegistrationController(IRepository<HostelRegistration> hocontext, IRepository<Hostel> hostelcontext,ApplicationDbContext usercontext)
        {
            context = hocontext;
            hostelcon = hostelcontext;
            fcontext = new ApplicationDbContext();

           


        }
        // GET: HostelRegistration
        public ActionResult Index()
        {
            var use = fcontext.Users.ToList(); 
           
          List<Hostel> hostels = hostelcon.Collection().ToList();


            var currentUserGender = from t in use

                                    where t.Email == User.Identity.Name //if you are using a string guid, otherwise remove ToString()
                                    select t;
            IEnumerable<BHMS.CORE.Models.Hostel> hostels1 = Enumerable.Empty<BHMS.CORE.Models.Hostel>();
            IEnumerable<BHMS.CORE.Models.Hostel> hostels2 = Enumerable.Empty<BHMS.CORE.Models.Hostel>();
            foreach (var check in currentUserGender)
            {

                if (check.Gender == 0)
                {
                    var gender0 = from t in hostels
                                  where (Convert.ToInt32(t.Gender) == 0)//if you are using a string guid, otherwise remove ToString()
                                  select t;
                    hostels1 = gender0;

                }
                else
                {
                    var gender1 = from t in hostels
                                  where (Convert.ToInt32(t.Gender) == 1) //if you are using a string guid, otherwise remove ToString()
                                  select t;
                    hostels1 = gender1;

                }


            }



            //List<RegisterViewModel> reg = registercon.Collection().ToList();
            //var itemss = from x in reg
            //             where x.Gender == 0 //if you are using a string guid, otherwise remove ToString()
            //             select x;

            //foreach(var check in itemss)
            //{
            //    var male = from x in reg
            //                 where x.Gender == 0 //if you are using a string guid, otherwise remove ToString()
            //                 select x;
            //}

            return View(hostels1);

        }


        [Authorize]
        public ActionResult Create(string Id)
        {
           
            Hostel hostelss = hostelcon.Find(Id);

            
            dynamic f= JsonConvert.DeserializeObject(hostelss.rooms);


            //  var room = from t in hostels

            //                        where t.Id == Id //if you are using a string guid, otherwise remove ToString()
            //                         select t;
            //foreach(var check in room)
            //{

            //}
            //HostelViewModel hostelViewModel = new HostelViewModel();

            //dynamic myModel
            /*hostelViewModel.Hostels= hostelcon.Collection();
            var f= hostelViewModel.Hostels;
            f.
            hostelViewModel.HostelRegistration = new HostelRegistration();


*/
            ViewBag.room = f;
            return View();
        }

        [HttpPost]
        public ActionResult Create(HostelRegistration hostelRegistration,string Id)
        {




            if (!ModelState.IsValid)
            {
                return View(hostelRegistration);
            }
            else
            {


                var gg = hostelRegistration.room;


            }



          

            return View();

        }
        //        [Authorize]
        //        public ActionResult Edit(string Id)
        //        {
        //            Hostel hostel = context.Find(Id);
        //            if (hostel == null)
        //            {
        //                return HttpNotFound();
        //            }
        //            else
        //            {


        //                return View(hostel);
        //            }
        //        }
        //        [HttpPost]
        //        public ActionResult Edit(Hostel hostel, string Id, HttpPostedFileBase file)
        //        {
        //            Hostel hostelToEdit = context.Find(Id);
        //            if (hostelToEdit == null)
        //            {
        //                return HttpNotFound();
        //            }
        //            else
        //            {

        //                if (!ModelState.IsValid)
        //                {
        //                    return View(hostel);
        //                }
        //                if (file != null)
        //                {

        //                    hostelToEdit.HostelImage = hostelToEdit.Id + Path.GetExtension(file.FileName);
        //                    file.SaveAs(Server.MapPath("//Content//HostelImages//") + hostelToEdit.HostelImage);



        //                    if (Convert.ToInt32(hostel.HostelCategory) == 0)
        //                    {
        //                        hostelToEdit.Roomsperblock = (hostel.Capacity / 2) / hostel.Hostelblocks;
        //                    }
        //                    else if (Convert.ToInt32(hostel.HostelCategory) == 1)
        //                    {
        //                        hostelToEdit.Roomsperblock = (hostel.Capacity / 4) / hostel.Hostelblocks;
        //                    }
        //                    else
        //                    {
        //                        hostelToEdit.Roomsperblock = (hostel.Capacity / 6) / hostel.Hostelblocks;
        //                    }

        //                }


        //                hostelToEdit.Halladmin = hostel.Halladmin;
        //                hostelToEdit.Hostelname = hostel.Hostelname;
        //                hostelToEdit.Gender = hostel.Gender;
        //                hostelToEdit.HostelCategory = hostel.HostelCategory;
        //                hostelToEdit.Capacity = hostel.Capacity;
        //                hostelToEdit.Hostelblocks = hostel.Hostelblocks;

        //                context.Commit();

        //                return RedirectToAction("Index");

        //            }
        //        }
        //        [Authorize]
        //        public ActionResult Delete(string Id)
        //        {
        //            Hostel hostelToDelete = context.Find(Id);

        //            if (hostelToDelete == null)
        //            {
        //                return HttpNotFound();
        //            }
        //            else
        //            {
        //                return View(hostelToDelete);
        //            }
        //        }

        //        [HttpPost]
        //        [ActionName("Delete")]
        //        public ActionResult confirmDelete(string Id)
        //        {
        //            Hostel hostelToDelete = context.Find(Id);
        //            if (hostelToDelete == null)
        //            {
        //                return HttpNotFound();
        //            }
        //            else
        //            {
        //                context.Delete(Id);
        //                context.Commit();
        //                return RedirectToAction("index");
        //            }
        //        }
    }
}