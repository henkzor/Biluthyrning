using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biluthyrning.Models.ViewModels
{
    public class BookingBoxVM
    {
        public int BookingNr { get; set; }
        public string CarType { get; set; }
        public string CarRegNr { get; set; }
        public DateTime? BookingStartTime { get; set; }
        public DateTime? BookingEndTime { get; set; }
        public bool IsReturned { get; set; }
    }
}
