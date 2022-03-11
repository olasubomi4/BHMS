using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHMS.CORE.Models
{
    public class VidUpload : BaseEntity
    {
        [Display(Name = "Missing Item name")]
        public string ItemName { get; set; }
        [Display(Name = "Missing Item Description")]
        public string ItemDescription { get; set; }
        [Display(Name = "Video upload result")]
        public string UploadResult { get; set; }
        [Display(Name = "Video URL")]
        public string UploadURl { get; set; }

        [Display(Name = "Video thumbnail")]
        public string Uploadtumb { get; set; }






    }
}