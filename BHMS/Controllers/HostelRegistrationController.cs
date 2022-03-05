using BHMS.CORE.Contract;
using BHMS.CORE.Models;
using BHMS.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace BHMS.Controllers
{
    public class HostelRegistrationController : Controller
    {
        IRepository<HostelRegistration> context;
        IRepository<Hostel> hostelcon;
        IRepository<RegisterViewModel> registercon;


        public HostelRegistrationController(IRepository<HostelRegistration> Context, IRepository<Hostel> hostelcontext, IRepository<RegisterViewModel> registercontext)
        {
            this.context = Context;
            hostelcon = hostelcontext;
            registercon = registercontext;



        }
        // GET: HostelRegistration
        public ActionResult Index()
        {
            //List<RegisterViewModel> registerViewModels = registercon.Collection().ToList();

            var user = User.Identity.GetUserId();
            

            List<Hostel> hostels = hostelcon.Collection().ToList();

           /*var currentUserGender = from t in registerViewModels
                                  where t.Email == User.Identity.Name //if you are using a string guid, otherwise remove ToString()
                                  select t;*/

            /*foreach (var check in currentUserGender)
            {
             
                if (check.Gender == 0)
                {
                    *//*var gender = from t in hostels
                                 where (Convert.ToInt32(t.Gender) == 0)//if you are using a string guid, otherwise remove ToString()
                                 select t;*//*
                    hostels = hostels.Where<Hostel>(x => x.Gender == CORE.Models.Gender.Male).ToList();

                }
                else
                {
                    var gender = from t in hostels
                                 where (Convert.ToInt32(t.Gender) == 1) //if you are using a string guid, otherwise remove ToString()
                                 select t;


                }

            }*/



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

            return View(hostels);

        }


        //        [Authorize]
        //        public ActionResult Create()
        //        {

        //            HostelRegistration hostelRegistration = new HostelRegistration();




        //            return View(hostelRegistration);
        //        }

        //        [HttpPost]
        //        public ActionResult Create(HostelRegistration hostelRegistration)
        //        {
        //            List<Hostel> hostels = hostelcon.Collection().ToList();
        //            if (!ModelState.IsValid)
        //            {
        //                return View(hostelRegistration);
        //            }
        //            else
        //            {


        //            }


        //            context.Insert(hostelRegistration);
        //            context.Commit();

        //            return RedirectToAction("index");

        //        }
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