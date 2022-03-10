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


        public HostelRegistrationController(IRepository<HostelRegistration> hocontext, IRepository<Hostel> hostelcontext, ApplicationDbContext usercontext)
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


            dynamic f = JsonConvert.DeserializeObject(hostelss.rooms);


            //var room = from t in hostels

            //           where t.Id == Id //if you are using a string guid, otherwise remove ToString()
            //           select t;
            //foreach (var check in room)
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
            ViewBag.HostelId = Id;
            return View();
        }

        [HttpPost]
        public ActionResult Create(HostelRegistration hostelRegistration,string Id)
        {
            Hostel hostelToEdit = hostelcon.Find(Id);
            if (hostelToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {



                HostelRegistration hostelRegistrationForRoomMate = new HostelRegistration();
                List<HostelRegistration> hostelregistrations = context.Collection().ToList();
                var findMatricNoList = fcontext.Users.ToList();
                hostelRegistration.Id = Guid.NewGuid().ToString();
                var findExactUser = from t in findMatricNoList

                                    where t.Email == User.Identity.Name //if you are using a string guid, otherwise remove ToString()
                                    select t;
                foreach (var finduser in findExactUser)
                {
                    hostelRegistration.User = finduser.MatricNo;
                }
                var testing = 0;
                var testing2 = 0;


                if (!ModelState.IsValid)
                {
                    return View(hostelRegistration);
                }
                else
                {
                    var checkifcurrentmatric = from t in hostelregistrations

                                               where t.User == hostelRegistration.User //if you are using a string guid, otherwise remove ToString()
                                               select t;
                    foreach (var finduser in checkifcurrentmatric)
                    {
                        testing = testing + 1;
                    }
                    if (testing == 0)
                    {


                        hostelToEdit.Availablespace = hostelToEdit.Availablespace - 1;

                        context.Insert(hostelRegistration);
                        context.Commit();
                        




                        var countRoomMates = 0;



                        if (hostelRegistration.roommateNo != null)
                        {


                            var use = fcontext.Users.ToList();
                            var findmatric = from t in findMatricNoList

                                             where t.MatricNo == hostelRegistration.roommateNo  //if you are using a string guid, otherwise remove ToString()
                                             select t;
                            foreach (var findNo in findmatric)
                            {
                                if (findNo.MatricNo == hostelRegistration.roommateNo)
                                {

                                    var checkifcurrentmatric2 = from t in hostelregistrations

                                                                where t.User == hostelRegistration.roommateNo //if you are using a string guid, otherwise remove ToString()
                                                                select t;
                                    foreach (var finduser in checkifcurrentmatric2)
                                    {
                                        testing2 = testing2 + 1;
                                    }
                                    if (testing2 == 0)
                                    {

                                        Hostel hostelss = hostelcon.Find(hostelRegistration.Hostelid);
                                        var checkroom = from t in hostelregistrations

                                                        where t.room == hostelRegistration.room  //if you are using a string guid, otherwise remove ToString()
                                                        select t;
                                        if (Convert.ToInt32(hostelss.HostelCategory) == 0)
                                        {

                                            foreach (var checkava in checkroom)
                                            {
                                                countRoomMates = countRoomMates + 1;
                                            }
                                            if (countRoomMates > 1)
                                            {
                                                ViewBag.Error = " Insufficent space Cant add roomMate";
                                            }
                                            else
                                            {
                                                hostelRegistrationForRoomMate.User = hostelRegistration.roommateNo;
                                                hostelRegistrationForRoomMate.roommateNo = null;
                                                hostelRegistrationForRoomMate.Id = Guid.NewGuid().ToString();
                                                hostelRegistrationForRoomMate.Halladmin = hostelRegistration.Halladmin;
                                                hostelRegistrationForRoomMate.room = hostelRegistration.room;
                                                hostelToEdit.Availablespace = hostelToEdit.Availablespace - 2;




                                                context.Insert(hostelRegistrationForRoomMate);
                                                context.Commit();
                                                hostelcon.Commit();


                                            }
                                        }
                                        else if (Convert.ToInt32(hostelss.HostelCategory) == 1)
                                        {

                                            foreach (var checkava in checkroom)
                                            {
                                                countRoomMates = countRoomMates + 1;
                                            }
                                            if (countRoomMates > 3)
                                            {
                                                ViewBag.Error = " Insufficent space Cant add roomMate";
                                            }
                                            else
                                            {
                                                hostelRegistrationForRoomMate.User = hostelRegistration.roommateNo;
                                                hostelRegistrationForRoomMate.roommateNo = null;
                                                hostelRegistrationForRoomMate.Id = Guid.NewGuid().ToString();
                                                hostelRegistrationForRoomMate.Halladmin = hostelRegistration.Halladmin;
                                                hostelRegistrationForRoomMate.room = hostelRegistration.room;
                                                hostelToEdit.Availablespace = hostelToEdit.Availablespace - 2;

                                                context.Insert(hostelRegistration);
                                                context.Commit();
                                                hostelcon.Commit();


                                            }
                                        }
                                        else
                                        {
                                            foreach (var checkava in checkroom)
                                            {
                                                countRoomMates = countRoomMates + 1;
                                            }
                                            if (countRoomMates > 5)
                                            {
                                                ViewBag.Error = " Insufficent space Cant add roomMate";
                                            }
                                            else
                                            {
                                                hostelRegistrationForRoomMate.User = hostelRegistration.roommateNo;
                                                hostelRegistrationForRoomMate.roommateNo = null;
                                                hostelRegistrationForRoomMate.Id = Guid.NewGuid().ToString();
                                                hostelRegistrationForRoomMate.Halladmin = hostelRegistration.Halladmin;
                                                hostelRegistrationForRoomMate.room = hostelRegistration.room;
                                                hostelToEdit.Availablespace = hostelToEdit.Availablespace - 2;

                                                context.Insert(hostelRegistration);
                                                context.Commit();
                                                hostelcon.Commit();


                                            }
                                        }
                                    }
                                    else
                                    {
                                        ViewBag.Error = "This user has a room";
                                    }

                                }
                                else
                                {
                                    ViewBag.Error = "Matric number doesn't exist ";
                                }




                            }

                        }

                        else
                        {
                            ViewBag.Error = "THis user has a room already ";
                        }



                    }


                    else
                    {
                        ViewBag.Error = "you can only have one room ";
                    }





                }
            }


          return RedirectToAction("index");


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