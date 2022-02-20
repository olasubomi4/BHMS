using BHMS.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Web.Mvc;


namespace BHMS.Controllers
{
    public class ObjectDetectorController : Controller
    {
        private const int V = 8;

        //    // GET: ObjectDetector
        //    IRepository<ObjectDetector> context;

        //    public ObjectDetectorController(IRepository<ObjectDetector> context)
        //    {
        //        this.context = context;
        //    }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ObjectDetector()
        {
            var apiUrl = "https://api.videoindexer.ai";
            var accountId = "b957b20c-a572-407d-83cc-26f69f597c3a";
            var location = "trial"; // replace with the account's location, or with “trial” if this is a trial account
            var apiKey = "6758c8d19c1742f48f8323afe02e1331";

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
            var videoUrl = "https://res.cloudinary.com/df68mnbrt/video/upload/v1645188566/bhms/students/items/IMG_5709.MOV.mov"; // replace with the video URL

            // as an alternative to specifying video URL, you can upload a file.
            // remove the videoUrl parameter from the query string below and add the following lines:
            //FileStream video =File.OpenRead(Globals.VIDEOFILE_PATH);
            //byte[] buffer = new byte[video.Length];
            //video.Read(buffer, 0, buffer.Length);
            //content.Add(new ByteArrayContent(buffer));
            var name = "laptop";
            var uploadRequestResult = client.PostAsync($"{apiUrl}/{location}/Accounts/{accountId}/Videos?accessToken={accountAccessToken}&name={name}&description=some_description&privacy=private&partition=some_partition&videoUrl={videoUrl}", content).Result;
            var uploadResult = uploadRequestResult.Content.ReadAsStringAsync().Result;

            // get the video id from the upload result
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

                    
              
                    var blogPosts = stuff.summarizedInsights.labels;
                   
                    dynamic blogPost = blogPosts[8];
                    string title = blogPost.name;
                   
                    var a=JsonConvert.DeserializeObject<dynamic>(blogPosts)[0];
                  

                    string nmee = stuff.summarizedInsights.labels.name;
                   
                    


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