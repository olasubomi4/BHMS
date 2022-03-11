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
        public ActionResult Create(HostelRegistration hostelRegistration, string Id)
        {
            Hostel hostelToEdit = hostelcon.Find(Id);
            dynamic f = JsonConvert.DeserializeObject(hostelToEdit.rooms);
            
            ViewBag.room = f;

            if (hostelToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {



                HostelRegistration hostelRegistrationForRoomMate = new HostelRegistration();
                List<HostelRegistration> hostelregistrations = context.Collection().ToList();
                var findMatricNoList = fcontext.Users.ToList();
                hostelRegistration.Hostelid = Id;
                hostelRegistration.Halladmin = hostelToEdit.Halladmin;
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

                                        Hostel hostelss = hostelcon.Find(hostelRegistration.Id);
                                        var checkroom = from t in hostelregistrations

                                                        where t.room == hostelRegistration.room  //if you are using a string guid, otherwise remove ToString()
                                                        select t;
                                        if (Convert.ToInt32(hostelToEdit.HostelCategory) == 0)
                                        {

                                            foreach (var checkava in checkroom)
                                            {
                                                countRoomMates = countRoomMates + 1;
                                            }
                                            if (countRoomMates >= 1)
                                            {
                                                ViewBag.Error = " Insufficent space cant add roomMate";
                                            }
                                            else
                                            {
                                                context.Insert(hostelRegistration);
                                                context.Commit();

                                                hostelRegistrationForRoomMate.User = hostelRegistration.roommateNo;
                                                hostelRegistrationForRoomMate.roommateNo = null;
                                                hostelRegistrationForRoomMate.Id = Guid.NewGuid().ToString();
                                                hostelRegistrationForRoomMate.Halladmin = hostelRegistration.Halladmin;
                                                hostelRegistrationForRoomMate.room = hostelRegistration.room;
                                                hostelToEdit.Availablespace = hostelToEdit.Availablespace - 2;




                                                context.Insert(hostelRegistrationForRoomMate);
                                                context.Commit();
                                                hostelcon.Commit();
                                                ViewBag.Success = "Registered for two users";


                                            }
                                        }
                                        else if (Convert.ToInt32(hostelss.HostelCategory) == 1)
                                        {

                                            foreach (var checkava in checkroom)
                                            {
                                                countRoomMates = countRoomMates + 1;
                                            }
                                            if (countRoomMates >= 3)
                                            {
                                                ViewBag.Error = " Insufficent space cant add roomMate";
                                            }
                                            else
                                            {
                                                context.Insert(hostelRegistration);
                                                context.Commit();
                                                hostelRegistrationForRoomMate.User = hostelRegistration.roommateNo;
                                                hostelRegistrationForRoomMate.roommateNo = null;
                                                hostelRegistrationForRoomMate.Id = Guid.NewGuid().ToString();
                                                hostelRegistrationForRoomMate.Halladmin = hostelRegistration.Halladmin;
                                                hostelRegistrationForRoomMate.room = hostelRegistration.room;
                                                hostelToEdit.Availablespace = hostelToEdit.Availablespace - 2;

                                                context.Insert(hostelRegistrationForRoomMate);
                                                context.Commit();
                                                hostelcon.Commit();
                                                ViewBag.Success = "Registered for two users";


                                            }
                                        }
                                        else
                                        {
                                            foreach (var checkava in checkroom)
                                            {
                                                countRoomMates = countRoomMates + 1;
                                            }
                                            if (countRoomMates >= 5)
                                            {
                                                ViewBag.Error = " Insufficent space cant add roomMate";
                                            }
                                            else
                                            {
                                                context.Insert(hostelRegistration);
                                                context.Commit();
                                                hostelRegistrationForRoomMate.User = hostelRegistration.roommateNo;
                                                hostelRegistrationForRoomMate.roommateNo = null;
                                                hostelRegistrationForRoomMate.Id = Guid.NewGuid().ToString();
                                                hostelRegistrationForRoomMate.Halladmin = hostelRegistration.Halladmin;
                                                hostelRegistrationForRoomMate.room = hostelRegistration.room;
                                                hostelToEdit.Availablespace = hostelToEdit.Availablespace - 2;

                                                context.Insert(hostelRegistrationForRoomMate);
                                                context.Commit();
                                                hostelcon.Commit();
                                                ViewBag.Success = "Registered for two users";


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
                            var checkroom = from t in hostelregistrations

                                            where t.room == hostelRegistration.room  //if you are using a string guid, otherwise remove ToString()
                                            select t;
                            if (Convert.ToInt32(hostelToEdit.HostelCategory) == 0)
                            {

                                foreach (var checkava in checkroom)
                                {
                                    countRoomMates = countRoomMates + 1;
                                }
                                if (countRoomMates >= 1)
                                {
                                    ViewBag.Error = " Room is full ";
                                }
                                else
                                {
                                    hostelToEdit.Availablespace = hostelToEdit.Availablespace - 1;
                                    context.Insert(hostelRegistration);
                                    context.Commit();
                                    hostelcon.Commit();
                                    ViewBag.Success = "Registered for one user";


                                }
                            }
                            else if (Convert.ToInt32(hostelToEdit.HostelCategory) == 1)
                            {

                                foreach (var checkava in checkroom)
                                {
                                    countRoomMates = countRoomMates + 1;
                                }
                                if (countRoomMates >= 4)
                                {
                                    ViewBag.Error = " Room is full";
                                }
                                else
                                {
                                    hostelToEdit.Availablespace = hostelToEdit.Availablespace - 1;
                                    context.Insert(hostelRegistration);
                                    context.Commit();
                                    hostelcon.Commit();
                                    ViewBag.Success = "Registered for one user";


                                }
                            }
                            else
                            {
                                foreach (var checkava in checkroom)
                                {
                                    countRoomMates = countRoomMates + 1;
                                }
                                if (countRoomMates >= 6)
                                {
                                    ViewBag.Error = " Room is full";
                                }
                                else
                                {
                                    hostelToEdit.Availablespace = hostelToEdit.Availablespace - 1;
                                    context.Insert(hostelRegistration);
                                    context.Commit();
                                    hostelcon.Commit();
                                    ViewBag.Success = "Registered for one user";


                                }
                            }
                           
                        }


                    }
                    else
                    {
                       
                        ViewBag.Error = "you can only have one room ";
                    }





                 return View();
                }

               




           
            }
            
        }
    }

             
}



       