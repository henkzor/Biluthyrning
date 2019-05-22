using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Biluthyrning.Models.ViewModels.Shared
{
    public class CarBoxVM
    {
        [Display(Name ="Car type")]
        public string Cartype { get; set; }

        [Display(Name = "Registration number")]
        public string RegnNr { get; set; }

        [Display(Name = "Mileage")]
        public int MileageKm { get; set; }

        [Display(Name = "Car Id")]
        public int Id { get; set; }

        [Display(Name = "Flagged for cleaning")]
        public bool FlaggedForCleaning { get; set; }

        [Display(Name = "Flagged for service")]
        public bool FlaggedForService { get; set; }

        [Display(Name = "Flagged for removal")]
        public bool FlaggedForRemoval { get; set; }

    }
}
