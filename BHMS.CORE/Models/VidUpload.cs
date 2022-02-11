using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHMS.CORE.Models
{
    public class VidUpload : BaseEntity
    {
        public string ItemName { get;set; }
        public string ItemDescription { get; set; }
        public string UploadResult { get; set; }
        public string UploadURl { get; set; }

        



    }
}