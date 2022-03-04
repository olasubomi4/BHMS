using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BHMS.CORE.Models;
using BHMS.CORE.Contract;
using BHMS.Models;

namespace BHMS.Controllers
{
    public class ObjectDetectorController : Controller
    {
        // GET: ObjectDetector
        IRepository<ObjectDetector> context;
        IRepository<VidUpload> vidUploadcon;
        public ObjectDetectorController(IRepository<ObjectDetector> context, IRepository<VidUpload> vidContext)
        {
            this.context = context;
            vidUploadcon = vidContext;

        }
        public ActionResult Index()
        {
            List<VidUpload> vidUploads = vidUploadcon.Collection().ToList();
            return View(vidUploads);
        }

        public ActionResult ObjectDetector(string Id, VidUpload vidUpload)
        {
            VidUpload videoToinvesigate = vidUploadcon.Find(Id);
            ObjectDetector objectDetector = new ObjectDetector();
            if (videoToinvesigate == null)
            {
                return HttpNotFound();
            }
            else
            {

                var apiUrl = "https://api.videoindexer.ai";
                var accountId = "b5832b6d-62f5-40a8-b1a4-956c57fcb81c";
                var location = "trial"; // replace with the account's location, or with “trial” if this is a trial account
                var apiKey = "fd053c46333e4bfd92c447c53fa06053";
                System.Net.ServicePointManager.SecurityProtocol = System.Net.ServicePointManager.SecurityProtocol | System.Net.SecurityProtocolType.Tls12;

                // create the http client
                var handler = new HttpClientHandler();
                handler.AllowAutoRedirect = false;
                var client = new HttpClient(handler);
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", apiKey);

                // obtain account access token
                var accountAccessTokenRequestResult = client.GetAsync($"{apiUrl}/auth/{location}/Accounts/{accountId}/AccessToken?allowEdit=true").Result;
                var accountAccessToken = accountAccessTokenRequestResult.Content.ReadAsStringAsync().Result.Replace("\"", "");

                client.DefaultRequestHeaders.Remove("Ocp-Apim-Subscription-Key");

                // upload a video
                var content = new MultipartFormDataContent();
                Debug.WriteLine("Uploading...");
                // get the video from URL

                var videoUrl = "https://res.cloudinary.com/df68mnbrt/video/upload/v1646218132/bhms/students/items/IMG_5709.MOV.mov";
            
                //videoToinvesigate.UploadURl; // replace with the video URL

                // as an alternative to specIMG_5709.MOV.movifying video URL, you can upload a file.
                // remove the videoUrl parameter from the query string below and add the following lines:
                //FileStream video =File.OpenRead(Globals.VIDEOFILE_PATH);
                //byte[] buffer = new byte[video.Length];
                //video.Read(buffer, 0, buffer.Length);
                //content.Add(new ByteArrayContent(buffer));

                var uploadRequestResult = client.PostAsync($"{apiUrl}/{location}/Accounts/{accountId}/Videos?accessToken={accountAccessToken}&name={videoToinvesigate.ItemName}&description={videoToinvesigate.ItemDescription}&privacy=private&partition=some_partition&videoUrl={videoUrl}", content).Result;
                var uploadResult = uploadRequestResult.Content.ReadAsStringAsync().Result;

                var videoId = JsonConvert.DeserializeObject<dynamic>(uploadResult)["id"];
                Debug.WriteLine("Uploaded");


                // obtain video access token
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", apiKey);
                var videoTokenRequestResult = client.GetAsync($"{apiUrl}/auth/{location}/Accounts/{accountId}/Videos/{videoId}/AccessToken?allowEdit=true").Result;
                var videoAccessToken = videoTokenRequestResult.Content.ReadAsStringAsync().Result.Replace("\"", "");

                client.DefaultRequestHeaders.Remove("Ocp-Apim-Subscription-Key");

                // wait for the video index to finish
                while (true)
                {
                    Thread.Sleep(10000);

                    var videoGetIndexRequestResult = client.GetAsync($"{apiUrl}/{location}/Accounts/{accountId}/Videos/{videoId}/Index?accessToken={videoAccessToken}&language=English").Result;
                    var videoGetIndexResult = videoGetIndexRequestResult.Content.ReadAsStringAsync().Result;

                    // get insights widget url

                    var insightsWidgetRequestResult = client.GetAsync($"{apiUrl}/{location}/Accounts/{accountId}/Videos/{videoId}/InsightsWidget?accessToken={videoAccessToken}&widgetType=Keywords&allowEdit=true").Result;
                    var insightsWidgetLink = insightsWidgetRequestResult.Headers.Location;
                    Debug.WriteLine("Insights Widget url:");
                    Debug.WriteLine(insightsWidgetLink);

                    ViewBag.k = insightsWidgetLink;

                    // get player widget url
                    var playerWidgetRequestResult = client.GetAsync($"{apiUrl}/{location}/Accounts/{accountId}/Videos/{videoId}/PlayerWidget?accessToken={videoAccessToken}").Result;
                    var playerWidgetLink = playerWidgetRequestResult.Headers.Location;
                    Debug.WriteLine("");
                    Debug.WriteLine("Player Widget url:");
                    Debug.WriteLine(playerWidgetLink);

                    ViewBag.playerWidgetLink = playerWidgetLink;

                    var processingState = JsonConvert.DeserializeObject<dynamic>(videoGetIndexResult)["state"];

                    Debug.WriteLine("");
                    Debug.WriteLine("State:");


                    // job is finished
                    if (processingState != "Uploaded" && processingState != "Processing")
                    {
                        Debug.WriteLine("");
                        Debug.WriteLine("Full JSON:");


                        dynamic stuff = JsonConvert.DeserializeObject(videoGetIndexResult);




                        SummarizedInsights deserializedDetectedModels = JsonConvert.DeserializeObject<SummarizedInsights>(videoGetIndexResult);
                        //string f = "";
                        //double confidence = 0.00 ;
                        //string startTime = "";
                        //string endTime = "";





                        var blogPosts = stuff.summarizedInsights.labels;
                        foreach (dynamic d in blogPosts)
                        {
                            if (d.name == videoToinvesigate.ItemName)
                            {
                                objectDetector.vidUploadId = videoToinvesigate.Id;
                                ViewBag.name = d.name;
                                objectDetector.name = d.name;
                                dynamic stufff = d.appearances;
                                foreach (dynamic g in stufff)
                                {
                                    ViewBag.confidence = g.confidence;
                                    ViewBag.startTime = g.startTime;
                                    ViewBag.endTime = g.endTime;
                                    objectDetector.confidence = g.confidence;

                                    objectDetector.startTime = g.startTime;
                                    objectDetector.endTime = g.endTime;
                                    objectDetector.insightsWidgetLink = insightsWidgetLink.ToString();
                                    objectDetector.playerWidgetLink = playerWidgetLink.ToString();
                                    ViewBag.ok = "OK";


                                }

                                var vidUploadId = objectDetector.vidUploadId;
                                ObjectDetector itemToDelete = context.Find(vidUploadId);
                                List<ObjectDetector> objectDetectors = context.Collection().ToList();
                                var itemss = from x in objectDetectors
                                             where x.vidUploadId == vidUploadId //if you are using a string guid, otherwise remove ToString()
                                             select x;
                                var resultt = 0;

                                foreach(var itemlength in itemss )
                                {
                                    var hh = itemlength;
                                    if (itemlength.vidUploadId == vidUploadId)
                                    {
                                         resultt= 1;

                                    }
                                    else
                                    {
                                        resultt = 0;
                                    }
                                }

                                
                                
                                
                               
                                
                                if (resultt == 0)
                                {
                                    context.Insert(objectDetector);
                                    context.Commit();
                                }
                                else
                                {


                                    context.Commit();
                                }
                            }



                        }










                        break;
                    }
                }

                // search for the video
                var searchRequestResult = client.GetAsync($"{apiUrl}/{location}/Accounts/{accountId}/Videos/Search?accessToken={accountAccessToken}&id={videoId}").Result;
                var searchResult = searchRequestResult.Content.ReadAsStringAsync().Result;
                Debug.WriteLine("");
                Debug.WriteLine("Search:");
                Debug.WriteLine(searchResult);

                // get insights widget url





                // get player widget url


                return View();
            }
        }


        public ActionResult Detected(string Id)
        {
            VidUpload vidUpload = vidUploadcon.Find(Id);
            List<ObjectDetector> objectDetectors = context.Collection().ToList();
            var itemss = from x in objectDetectors
                         where x.vidUploadId == Id//if you are using a string guid, otherwise remove ToString()
                         select x;
            var resultt = 0;

            foreach (var vid in itemss)
            {


                if (Id == vid.vidUploadId)
                {
                    resultt = 1;
                    var vidlin = vidUpload.UploadURl;
                    ViewBag.linkk = vidlin;


                }
                else
                {
                    resultt = 0;
                }

            }
           

           
                if (resultt == 0)
                {
                    ViewBag.missing = "MISSING";
                    return View();
                }
                else
                {
                    return View(itemss);
                }
            
        }
        public ActionResult Delete(string Id)
        {


            ObjectDetector item = context.Find(Id);


            if (item == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(item);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult confirmDelete(string Id, string vidUploadId)
        {

            ObjectDetector item = context.Find(Id);

            if (item == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(vidUploadId);
                context.Commit();
                return RedirectToAction("index");
            }
        }
    }
}