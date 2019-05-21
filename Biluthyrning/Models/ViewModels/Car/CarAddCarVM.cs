using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Biluthyrning.Models.ViewModels.Car
{
    public class CarAddCarVM
    {
        [Required]
        [Display(Name ="Registration number")]
        public string RegNr { get; set; }

        [Required]
        [Display(Name = "Mileage")]
        public int MileageKm { get; set; }

        [Required]
        [Display(Name = "Car type")]
        public string Cartype { get; set; }
    }
}
