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
        IRepository<VidUpload> context;

        public ObjectDetectorController(IRepository<VidUpload> context)
        {
            this.context = context;
        }
        public ActionResult Index()
        {
            List<VidUpload> vidUploads = context.Collection().ToList();
            return View(vidUploads);
        }
       
        public ActionResult ObjectDetector(string Id,VidUpload vidUpload)
        {
             VidUpload videoToinvesigate = context.Find(Id);

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

                var videoUrl = videoToinvesigate.UploadURl;
                    //videoToinvesigate.UploadURl; // replace with the video URL

                // as an alternative to specifying video URL, you can upload a file.
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
                        string f = "";
                        double confidence = 0.00 ;
                        string startTime = "";
                        string endTime = "";




                        var blogPosts = stuff.summarizedInsights.labels;
                        foreach (dynamic d in blogPosts)
                        {
                            if (d.name == videoToinvesigate.ItemName)

                                f = d.name;
                                
                                dynamic stufff =d.appearances;
                                
                                foreach (dynamic g in stufff)
                                {
                                confidence = g.confidence;
                                startTime = g.startTime;
                                endTime   =g.endTime;

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
                var insightsWidgetRequestResult = client.GetAsync($"{apiUrl}/{location}/Accounts/{accountId}/Videos/{videoId}/InsightsWidget?accessToken={videoAccessToken}&widgetType=Keywords&allowEdit=true").Result;
                var insightsWidgetLink = insightsWidgetRequestResult.Headers.Location;
                Debug.WriteLine("Insights Widget url:");
                Debug.WriteLine(insightsWidgetLink);
                ViewBag.k = insightsWidgetRequestResult;




                // get player widget url
                var playerWidgetRequestResult = client.GetAsync($"{apiUrl}/{location}/Accounts/{accountId}/Videos/{videoId}/PlayerWidget?accessToken={videoAccessToken}").Result;
                var playerWidgetLink = playerWidgetRequestResult.Headers.Location;
                Debug.WriteLine("");
                Debug.WriteLine("Player Widget url:");
                Debug.WriteLine(playerWidgetLink);

                return View();
            }
        }
    }
}