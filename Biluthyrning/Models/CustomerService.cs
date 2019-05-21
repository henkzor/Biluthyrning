using Biluthyrning.Models.Entities;
using Biluthyrning.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biluthyrning.Models
{
    public class CustomerService
    {
        readonly BiluthyrningDBContext context;
        public CustomerService(BiluthyrningDBContext context)
        {
            this.context = context;
        }

        public CustomerIndexVM GetAllCustomers()
        {
            CustomerIndexVM CIVM = new CustomerIndexVM();
            CIVM.CustomerBoxVMList = new List<CustomerBoxVM>();

            foreach (var item in context.Customers)
            {
                CIVM.CustomerBoxVMList.Add(new CustomerBoxVM
                {
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    PersonNr = item.PersonNr,
                    Id = item.Id
                });
            }

            


            return CIVM;
        }

        public CustomerDetailsVM GetCustomerByID(int id)
        {
            CustomerDetailsVM CDVM = new CustomerDetailsVM();
            CDVM.BookingBoxVMList = new List<BookingBoxVM>();

            CDVM.FirstName = context.Customers
                .Where(c => c.Id == id)
                .Select(c => c.FirstName)
                .FirstOrDefault();

            CDVM.LastName = context.Customers
                .Where(c => c.Id == id)
                .Select(c => c.LastName)
                .FirstOrDefault();

            CDVM.PersonNr = context.Customers
                .Where(c => c.Id == id)
                .Select(c => c.PersonNr)
                .FirstOrDefault();

            foreach (var item in context.Bookings)
            {
                if (item.CustomerId == id)
                {
                    CDVM.BookingBoxVMList.Add(new BookingBoxVM
                    {
                        BookingNr = item.BookingNr,
                        BookingStartTime = item.BookingStart,
                        BookingEndTime = item.BookingEnd,
                        CarType = context.Cars
                        .Where(c => c.Id == item.CarId)
                        .Select(c => c.Cartype)
                        .FirstOrDefault(),
                        CarRegNr = context.Cars
                        .Where(c => c.Id == item.CarId)
                        .Select(c => c.RegnNr)
                        .FirstOrDefault(),
                        IsReturned = item.IsReturned
                    });
                }
            }

            return CDVM;
        }

    }
}
