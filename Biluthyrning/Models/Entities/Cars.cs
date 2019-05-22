using System;
using System.Collections.Generic;

namespace Biluthyrning.Models.Entities
{
    public partial class Cars
    {
        public int Id { get; set; }
        public string Cartype { get; set; }
        public string RegnNr { get; set; }
        public int MileageKm { get; set; }
        public bool FlaggedForCleaning { get; set; }
        public bool FlaggedForService { get; set; }
        public bool FlaggedForRemoval { get; set; }
        public int BookingsSinceService { get; set; }
    }
}
