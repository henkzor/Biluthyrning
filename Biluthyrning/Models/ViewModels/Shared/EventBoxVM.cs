using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biluthyrning.Models.ViewModels.Shared
{
    public class EventBoxVM
    {
        public int EventId { get; set; }
        public string EventType { get; set; }
        public int? CarId { get; set; }
        public int? CustomerId { get; set; }
        public int? BookingId { get; set; }
        public DateTime Date { get; set; }

    }
}
