using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Biluthyrning.Models.ViewModels
{
    public class CarRentalShowBookingVM
    {

        [Display(Name = "Booking number")]
        public int BookingNr { get; set; }

        [Display(Name = "Person number")]
        public string CustomerPersonNr { get; set; }

        [Display(Name = "Car type")]
        public string CarType { get; set; }

        [Display(Name = "Car registration number")]
        public string CarRegNr { get; set; }

        [Display(Name = "Booking starts at")]
        public DateTime? BookingStartTime { get; set; }

        [Display(Name = "Booking ends at")]
        public DateTime? BookingEndTime { get; set; }

        [Display(Name = "Mileage at start")]
        public int? MileageBefore { get; set; }

        [Display(Name = "Mileage at return")]
        public int? MileageAfter { get; set; }

        [Display(Name = "Total cost")]
        public decimal? Cost { get; set; }

    }
}
