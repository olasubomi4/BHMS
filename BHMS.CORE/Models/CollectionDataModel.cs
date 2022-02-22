using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHMS.CORE.Models
{
    public class CollectionDataModel
    {
        public List<VidUpload> vidUploads { get; set; }
        public List<ObjectDetector> objectDetectors { get; set; }
    }
}
