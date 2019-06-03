using System;
using System.Collections.Generic;

namespace Biluthyrning.Models.Entities
{
    public partial class Customers
    {
        public Customers()
        {
            Bookings = new HashSet<Bookings>();
            Events = new HashSet<Events>();
        }

        public int Id { get; set; }
        public string PersonNr { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int NrOfBookings { get; set; }
        public int TotalKmDriven { get; set; }
        public int BonusLevel { get; set; }

        public virtual ICollection<Bookings> Bookings { get; set; }
        public virtual ICollection<Events> Events { get; set; }
    }
}
