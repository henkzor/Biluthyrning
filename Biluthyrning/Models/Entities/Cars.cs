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
    }
}
