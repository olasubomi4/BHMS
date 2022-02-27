using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHMS.CORE.Models
{
    public class ObjectDetector : BaseEntity
    {

        public string name { get; set; }
        public double confidence { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }

        public string vidUploadId { get; set; }

        public string playerWidgetLink { get; set; }

        public string insightsWidgetLink { get; set; }









    }
}