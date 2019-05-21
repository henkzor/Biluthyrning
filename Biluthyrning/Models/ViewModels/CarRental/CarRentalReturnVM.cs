using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Biluthyrning.Models.ViewModels
{
    public class CarRentalReturnVM
    {
        [Display(Name="Booking number")]
        public int BookingNr { get; set; }

        [Display(Name = "Return date")]
        public DateTime ReturnDate { get; set; }
        [Display(Name="Mileage on return")]
        public int MileageReturnKm { get; set; }

    }
}
