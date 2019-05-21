using System;
using System.Collections.Generic;

namespace Biluthyrning.Models.Entities
{
    public partial class Bookings
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public int CustomerId { get; set; }
        public int BookingNr { get; set; }
        public DateTime BookingPlaced { get; set; }
        public DateTime BookingStart { get; set; }
        public DateTime BookingEnd { get; set; }
        public DateTime? ReturnDate { get; set; }
        public decimal? Cost { get; set; }
        public int MileageBeforeKm { get; set; }
        public int? MileageAfterKm { get; set; }
        public bool IsReturned { get; set; }
    }
}
