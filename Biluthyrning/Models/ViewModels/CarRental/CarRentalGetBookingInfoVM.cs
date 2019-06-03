using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Biluthyrning.Models.ViewModels.CarRental
{
    public class CarRentalGetBookingInfoVM
    {
        [Display(Name = "Booking number")]
        public int BookingNr { get; set; }

        [Display(Name = "Car registration number")]
        public string CarRegNr { get; set; }

        [Display(Name = "Booking starts at")]
        public DateTime? BookingStartTime { get; set; }

        [Display(Name = "Booking ends at")]
        public DateTime? BookingEndTime { get; set; }

        [Display(Name = "Mileage at start")]
        public int? MileageBefore { get; set; }
    }
}
