using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHMS.CORE.Models
{
    public class ObjectDetector : BaseEntity
    {
        [Display(Name = "Missing item name")]
        public string name { get; set; }
        [Display(Name = "Missing Item Confidence")]
        public double confidence { get; set; }
        [Display(Name = "Missing Item Start time")]
        public string startTime { get; set; }
        [Display(Name = "Missing Item End time")]
        public string endTime { get; set; }
        [Display(Name = "Video Identification Number")]
        public string vidUploadId { get; set; }

        [Display(Name = "Video Player Widget Link")]
        public string playerWidgetLink { get; set; }
        [Display(Name = "Insights Widget Link")]
        public string insightsWidgetLink { get; set; }


        









    }
}