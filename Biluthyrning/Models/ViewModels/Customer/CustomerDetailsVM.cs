using Biluthyrning.Models.ViewModels.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Biluthyrning.Models.ViewModels
{
    public class CustomerDetailsVM
    {
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Person Number")]
        public string PersonNr { get; set; }

        public List<BookingBoxVM> BookingBoxVMList { get; set; }

        public List<EventBoxVM> EventBoxVMList { get; set; }

        public int Id { get; set; }

    }
}
