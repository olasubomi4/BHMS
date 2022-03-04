using BHMS.CORE.Contract;
using BHMS.CORE.Models;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;


namespace BHMS.Controllers
{
    public class VideoUploadController : Controller
    {

        IRepository<VidUpload> context;

        public VideoUploadController(IRepository<VidUpload> context)
        {
            this.context = context;
        }

        // GET: VideoUpload
        public ActionResult Index()
        {


            List<VidUpload> vidUpload = context.Collection().ToList();
            var validd = 0;
            foreach( var check in vidUpload)
            {
                
                    ViewBag.resultt = "yes";
                
                
            }

            return View(vidUpload);
        }

        public static Cloudinary cloudinary;

        public const string CloudName = "subomi";
        public const string APIKey = "425351418729339";
        public const string APISecret = "nDTb_W6CpE5TQj4_lgt1_dmQGQ0";



        [HttpGet]
        public ActionResult uploadVideo()
        {
            return View();
        }

        public ActionResult uploadVideo(VidUpload vidupload,HttpPostedFileBase file)
        {
            
            Account account = new Account(CloudName, APIKey, APISecret);
            cloudinary = new Cloudinary(account);
            cloudinary.Api.Secure = true;

            /*var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(@"C:\Users\yuung\Downloads\cruisin.jpg")
            };

            var uploadResult = cloudinary.Upload(uploadParams).StatusCode;
            if (model != null)
            {
                model.UploadResult = uploadResult.ToString();
                ViewBag.Result = model.UploadResult;
                return View();
            }
            else
            {
                return HttpNotFound();
            }*/

            /*var uploadParams = new VideoUploadParams()
            {
                File = new FileDescription(@"C:\Users\yuung\Downloads\apex.mp4"),
                PublicId = "",
                EagerTransforms = new List<Transformation>()
                {
                    new EagerTransformation().Width(300).Height(300).Crop("pad").AudioCodec("none"),
                    new EagerTransformation().Width(160).Height(100).Crop("crop").Gravity("south").AudioCodec("none"),
                },
                EagerAsync = true,
                EagerNotificationUrl = "https://mysite.example.com/my_notification_endpoint"
                
            };*/

            try
            {
                if (file.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(file.FileName);
                    string _path = Path.Combine(Server.MapPath("~/assets/img/uploadedImgs"), _FileName);
                    file.SaveAs(_path);

                    var uploadParams = new VideoUploadParams()
                    {
                        File = new FileDescription(_path),
                        PublicId = $"bhms/students/items/{_FileName}",
                        Overwrite = true,
                        NotificationUrl = "https://mysite.example.com/my_notification_endpoint"
                    };
                

                var uploadResult = cloudinary.Upload(uploadParams).StatusCode;
                var uploadResultt = cloudinary.Upload(uploadParams).SecureUrl;




                vidupload.UploadURl = uploadResultt.ToString();
                var uploadResultThumbnail = uploadResultt.ToString();
                uploadResultThumbnail = uploadResultThumbnail.Remove(uploadResultThumbnail.Length - 4);
                uploadResultThumbnail = uploadResultThumbnail.Insert(uploadResultThumbnail.Length, ".jpg");
                vidupload.Uploadtumb = uploadResultThumbnail;



                if (vidupload != null)
                {
                    vidupload.UploadResult = uploadResult.ToString();
                    ViewBag.Result = vidupload.UploadResult;


                    context.Insert(vidupload);
                    context.Commit();

                    return View();

                }
                else
                {
                    return HttpNotFound();
                }
            }

                ViewBag.Message = "File Uploaded Successfully!!";


            return View();
        }
            catch
            {
                ViewBag.Message = "File upload failed!!";
                return View();
    }
}
[Authorize]
        public ActionResult Edit(string Id)
        {
            VidUpload vidupload = context.Find(Id);
            if (vidupload == null)
            {
                return HttpNotFound();
            }
            else
            {



                return View(vidupload);
            }
        }
        [HttpPost]
        public ActionResult Edit(VidUpload vidupload, string Id, HttpPostedFileBase file)
        {
            VidUpload vidToEdit = context.Find(Id);
            if (vidToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {

                if (!ModelState.IsValid)
                {
                    return View(vidupload);
                }




                vidToEdit.ItemDescription = vidupload.ItemDescription;
                vidToEdit.ItemName = vidupload.ItemName;




                context.Commit();

                return RedirectToAction("Index");

            }
        }
        [Authorize]
        public ActionResult Delete(string Id)
        {
            VidUpload vidToDelete = context.Find(Id);

            if (vidToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(vidToDelete);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult confirmDelete(string Id)
        {
            VidUpload vidToDelete = context.Find(Id);
            if (vidToDelete == null)
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