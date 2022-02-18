using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Windows;

namespace BHMS.Models
{
    public class DetectedModels
    {
        public string name { get; set; }
        public string id { get; set; }
        public string privacyMode { get; set; }
        public Duration duration { get; set; }
        public string thumbnailVideoId { get; set; }
        public string thumbnailId { get; set; }
        public List<object> faces { get; set; }
        public List<object> keywords { get; set; }
        public List<object> sentiments { get; set; }
        public List<object> emotions { get; set; }
        public List<object> audioEffects { get; set; }
        public List<Label> labels { get; set; }
        public List<object> framePatterns { get; set; }
        public List<object> brands { get; set; }
        public List<object> namedLocations { get; set; }
        public List<object> namedPeople { get; set; }
        public List<object> topics { get; set; }
    }
}