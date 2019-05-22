using Biluthyrning.Models.ViewModels.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Biluthyrning.Models.ViewModels.Car
{
    public class CarDetailsVM
    {
        public int CarId { get; set; }
        public string Cartype { get; set; }
        public string RegnNr { get; set; }
        public int MileageKm { get; set; }
        public bool FlaggedForCleaning { get; set; }
        public bool FlaggedForService { get; set; }
        public bool FlaggedForRemoval { get; set; }
        public int BookingsSinceService { get; set; }
        public bool Active { get; set; }
        public List<BookingBoxVM> BookingBoxVMList { get; set; }

        public List<EventBoxVM> EventBoxVMList { get; set; }
    }
}
