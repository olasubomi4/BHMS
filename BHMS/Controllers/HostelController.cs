using BHMS.CORE.Contract;
using BHMS.CORE.Models;
using Newtonsoft.Json;
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
        //IRepository gives me access to dynamic functions like commit, insert, find,collection,and so more <t> which can be used by any model has long as it inherites the baseEntity model.
        IRepository<Hostel> context;
        

        public HostelController(IRepository<Hostel> Context)
        {
            this.context = Context;
           

        }
        // GET: Hostel
        public ActionResult Index()
        {
            //This fuction returns a list of all the data in hostel database.
            List<Hostel> hostels = context.Collection().ToList();
            return View(hostels);
        }
        [Authorize]
        public ActionResult Create()
        {

            // I used the first create function to display elements that I want in the create.cshtml before any action is taken.
            Hostel hostel = new Hostel();
           



            return View(hostel);
        }

        [HttpPost]
        public ActionResult Create(Hostel hostel, HttpPostedFileBase file)
        {

            // I used the second create funtion to perform submit the data that as been filed into create.cshtml .

            if (!ModelState.IsValid)
            {
                //This condition statement will return an error because the model state is not vaild.
                ViewBag.Error = "Model is not valid";
                return View(hostel);
            }
            else
            {

                if (file != null)
                {
                    //This conditions statement checks if the image file was passed into the create.cshtml form.
             
                    hostel.HostelImage = hostel.Id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//Content//HostelImages//") + hostel.HostelImage);
                    hostel.Availablespace = hostel.Capacity;

                    //Hostel category 0= classic hall, Hostel category 1= premium hall Hostel cateogory 1= regular hall.
                        if (Convert.ToInt32(hostel.HostelCategory) == 0)
                        {
                            //This condition calculates the number of room that will be in a Classic hostel based on the hostel capacity and number of blocks
                            hostel.Roomsperblock = (hostel.Capacity / 2) / hostel.Hostelblocks;
                            
                        }

                        else if (Convert.ToInt32(hostel.HostelCategory) == 1)
                        {
                        //This condition calculates the number of room that will be in a Premium hostel based on the hostel capacity and number of blocks
                        hostel.Roomsperblock = (hostel.Capacity / 4) / hostel.Hostelblocks;
                        }
                        else
                        {
                        //This condition calculates the number of room that will be in a Regular hostel based on the hostel capacity and number of blocks
                        hostel.Roomsperblock = (hostel.Capacity / 6) / hostel.Hostelblocks;
                        }

                    //This variable will be used to generate room alphbets 
                    var alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    
                    List<string> roomalpha = new List<string>();
                    List<string> roomlist = new List<string>();
                    for (int i = 0; i < hostel.Hostelblocks; i++)
                    {
                        
                        
                        for (int j = 1; j <= hostel.Roomsperblock; j++)
                        {

                            roomlist.Add(alphabets[i] + "F" +j);


                        }
                        //This nested forloop will generate a list of rooms in the order of "Af1,AF2,AF3....... NfN"

                    }

                   

                    //I converted the list into a json file because the database cannot accept a list.
                    var listString = JsonConvert.SerializeObject(roomlist);

                    hostel.rooms = listString;
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