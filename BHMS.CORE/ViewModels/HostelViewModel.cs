using BHMS.CORE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHMS.CORE.ViewModels
{
    public class HostelViewModel
    {

        public HostelRegistration HostelRegistration { get; set; }
        public IEnumerable<Hostel> Hostels { get; set; }
    }
}
