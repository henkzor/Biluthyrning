using Biluthyrning.Models.Entities;
using Biluthyrning.Models.ViewModels;
using Biluthyrning.Models.ViewModels.Shared;
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
            CustomerDetailsVM CDVM = context.Customers
                .Where(c => c.Id == id)
                .Select(c => new CustomerDetailsVM
                {
                    Id = id,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    BookingBoxVMList = new List<BookingBoxVM>(),
                    EventBoxVMList = new List<EventBoxVM>(),
                    PersonNr = c.PersonNr
                }).FirstOrDefault();

            CDVM.BookingBoxVMList.AddRange(context.Bookings
                .Where(b => b.Customer.Id == id)
                .Select(b => new BookingBoxVM
                {
                    BookingNr = b.BookingNr,
                    BookingStartTime = b.BookingStart,
                    BookingEndTime = b.BookingEnd,
                    CarType = b.Car.Cartype,
                    CarRegNr = b.Car.RegnNr,
                    IsReturned = b.IsReturned
                }));

            
            foreach (var item in context.Events)
            {
                if (item.CustomerId == id)
                {
                    EventBoxVM EBVM = new EventBoxVM();

                    if (item.EventType == "Created Booking" || item.EventType == "Returned Car")
                    {
                        EBVM.BookingId = item.BookingId;
                        EBVM.CustomerId = item.CustomerId;
                    }

                    EBVM.EventId = item.Id;
                    EBVM.CarId = item.CarId;
                    EBVM.EventType = item.EventType;
                    EBVM.Date = item.Date;

                    CDVM.EventBoxVMList.Add(EBVM);

                }
            }

            return CDVM;
        }

    }
}
