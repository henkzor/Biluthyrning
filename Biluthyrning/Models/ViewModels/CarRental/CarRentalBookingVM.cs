using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Biluthyrning.Models.ViewModels
{
    public class CarRentalBookingVM
    {
        [Required]
        [Display(Name = "Customers Person Number")]
        public string PersonNr { get; set; }

        //[Required]
        [Display(Name = "Customers First Name")]
        public string FirstName{ get; set; }

        //[Required]
        [Display(Name = "Customers Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Starting Date For Booking")]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "End Date For Booking")]
        public DateTime EndDate { get; set; }

        [Required]
        [Display(Name = "Car Type")]
        public string Cartype { get; set; }

        [Required]
        [Display(Name = "Car Id")]
        public int CarId { get; set; }
    }
}
