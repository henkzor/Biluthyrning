using System;
using System.Collections.Generic;

namespace Biluthyrning.Models.Entities
{
    public partial class Events
    {
        public int Id { get; set; }
        public string EventType { get; set; }
        public int? CarId { get; set; }
        public int? CustomerId { get; set; }
        public int? BookingId { get; set; }
        public DateTime Date { get; set; }

        public virtual Bookings Booking { get; set; }
        public virtual Cars Car { get; set; }
        public virtual Customers Customer { get; set; }
    }
}
