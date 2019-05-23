using System;
using System.Collections.Generic;

namespace Biluthyrning.Models.Entities
{
    public partial class Customers
    {
        public int Id { get; set; }
        public string PersonNr { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int NrOfBookings { get; set; }
        public int TotalKmDriven { get; set; }
        public int BonusLevel { get; set; }
    }
}
