using BHMS.CORE.Contract;
using BHMS.CORE.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BHMS.Controllers
{
    public class HostelController : Controller
    {
        IRepository<Hostel> context;
        

        public HostelController(IRepository<Hostel> Context)
        {
            this.context = Context;
           

        }
        // GET: Hostel
        public ActionResult Index()
        {
            List<Hostel> hostels = context.Collection().ToList();
            return View(hostels);
        }
        [Authorize]
        public ActionResult Create()
        {

            Hostel hostel = new Hostel();
           



            return View(hostel);
        }

        [HttpPost]
        public ActionResult Create(Hostel hostel, HttpPostedFileBase file)
        {

            if (!ModelState.IsValid)
            {
                return View(hostel);
            }
            else
            {
                if (file != null)
                {
             
                    hostel.HostelImage = hostel.Id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//Content//HostelImages//") + hostel.HostelImage);
                    hostel.Availablespace = hostel.Capacity;
                        if (Convert.ToInt32(hostel.HostelCategory) == 0)
                        {
                            hostel.Roomsperblock = (hostel.Capacity / 2) / hostel.Hostelblocks;
                        }
                        else if (Convert.ToInt32(hostel.HostelCategory) == 1)
                        {
                            hostel.Roomsperblock = (hostel.Capacity / 4) / hostel.Hostelblocks;
                        }
                        else
                        {
                            hostel.Roomsperblock = (hostel.Capacity / 6) / hostel.Hostelblocks;
                        }
                }

            }
                
               
                context.Insert(hostel);
                context.Commit();

                return RedirectToAction("index");
            
        }
        [Authorize]
        public ActionResult Edit(string Id)
        {
            Hostel hostel = context.Find(Id);
            if (hostel == null)
            {
                return HttpNotFound();
            }
            else
            {


                return View(hostel);
            }
        }
        [HttpPost]
        public ActionResult Edit(Hostel hostel, string Id, HttpPostedFileBase file)
        {
            Hostel hostelToEdit = context.Find(Id);
            if (hostelToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {

                if (!ModelState.IsValid)
                {
                    return View(hostel);
                }
                if (file != null)
                {

                    hostelToEdit.HostelImage = hostelToEdit.Id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//Content//HostelImages//") + hostelToEdit.HostelImage);

                    

                    if (Convert.ToInt32(hostel.HostelCategory) == 0)
                    {
                        hostelToEdit.Roomsperblock = (hostel.Capacity / 2) / hostel.Hostelblocks;
                    }
                    else if (Convert.ToInt32(hostel.HostelCategory) == 1)
                    {
                        hostelToEdit.Roomsperblock = (hostel.Capacity / 4) / hostel.Hostelblocks;
                    }
                    else
                    {
                        hostelToEdit.Roomsperblock = (hostel.Capacity / 6) / hostel.Hostelblocks;
                    }

                }


                hostelToEdit.Halladmin = hostel.Halladmin;
                hostelToEdit.Hostelname = hostel.Hostelname;
                hostelToEdit.Gender = hostel.Gender;
                hostelToEdit.HostelCategory = hostel.HostelCategory;
                hostelToEdit.Capacity = hostel.Capacity;
                hostelToEdit.Hostelblocks = hostel.Hostelblocks;
                
                context.Commit();

                return RedirectToAction("Index");

            }
        }
        [Authorize]
        public ActionResult Delete(string Id)
        {
            Hostel hostelToDelete = context.Find(Id);

            if (hostelToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(hostelToDelete);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult confirmDelete(string Id)
        {
            Hostel hostelToDelete = context.Find(Id);
            if (hostelToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(Id);
                context.Commit();
                return RedirectToAction("index");
            }
        }
    }
}