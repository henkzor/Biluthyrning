using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biluthyrning.Models.Data
{
    public class CheckCustomerDM
    {
        public string PersonNr { get; set; }
        public bool CustomerFound { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
