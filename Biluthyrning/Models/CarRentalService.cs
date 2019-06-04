using Biluthyrning.Models.Data;
using Biluthyrning.Models.Entities;
using Biluthyrning.Models.ViewModels;
using Biluthyrning.Models.ViewModels.CarRental;
using Biluthyrning.Models.ViewModels.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biluthyrning.Models
{
    public class CarRentalService
    {
        readonly BiluthyrningDBContext context;
        EventRepository eventRepository;
        public CarRentalService(BiluthyrningDBContext context)
        {
            this.context = context;
            eventRepository = new EventRepository(context);
        }

        public CarRentalIndexVM GetAllBookings()
        {
            CarRentalIndexVM CRVM = new CarRentalIndexVM();
            CRVM.BookingBoxVMList = new List<BookingBoxVM>();

            foreach (var item in context.Bookings)
            {
                CRVM.BookingBoxVMList.Add(new BookingBoxVM
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

            return CRVM;
        }

        public int CreateBooking(CarRentalBookingVM CRBVM)
        {
            Bookings booking = new Bookings();

            booking.BookingNr = context.Bookings
                .Select(c => c.BookingNr)
                .LastOrDefault() + 1;

            if (!context.Customers.Any(o => o.PersonNr == CRBVM.PersonNr))
            {
                context.Customers
                    .Add(new Customers()
                    {
                        PersonNr = CRBVM.PersonNr,
                        FirstName = CRBVM.FirstName,
                        LastName = CRBVM.LastName,
                        TotalKmDriven = 0,
                        NrOfBookings = 0
                    });
                context.SaveChanges();
            }
            booking.CustomerId = context.Customers
                .Where(c => c.PersonNr == CRBVM.PersonNr)
                .Select(c => c.Id)
                .FirstOrDefault();
            booking.CarId = context.Cars
                .Where(c => c.Id == CRBVM.CarId)
                .Select(c => c.Id)
                .FirstOrDefault();
            booking.BookingPlaced = DateTime.Now;
            booking.BookingStart = CRBVM.StartDate;
            booking.BookingEnd = CRBVM.EndDate;
            booking.MileageBeforeKm = context.Cars
                .Where(c => c.Id == booking.CarId)
                .Select(c => c.MileageKm)
                .FirstOrDefault();
            booking.IsReturned = false;

            context.Bookings.Add(booking);


            context.Events.Add(new Events
            {
                BookingId = booking.Id,
                CarId = booking.CarId,
                CustomerId = booking.CustomerId,
                EventType = "Created Booking",
                Date = DateTime.Now
            });

            context.SaveChanges();

            return booking.BookingNr;
        }

        

        public CarRentalShowBookingVM GetCarForShowBooking(int BookingNr)
        {
            return context.Bookings
                .Where(b => b.Id == BookingNr)
                .Select(b => new CarRentalShowBookingVM
                {
                    BookingNr = b.BookingNr,
                    BookingStartTime = b.BookingStart,
                    BookingEndTime = b.BookingEnd,
                    CarRegNr = b.Car.RegnNr,
                    CarType = b.Car.Cartype,
                    Cost = b.Cost,
                    MileageBefore = b.MileageBeforeKm,
                    MileageAfter = b.MileageAfterKm,
                    CustomerPersonNr = b.Customer.PersonNr,
                    isReturned = b.IsReturned
                }).FirstOrDefault();

        }

        public void ReturnCar(CarRentalReturnVM CRRVM)
        {
            Bookings booking = context.Bookings
                .Where(b => b.BookingNr == CRRVM.BookingNr)
                .Select(b => b)
                .FirstOrDefault();

            booking.MileageAfterKm = CRRVM.MileageReturnKm;
            booking.ReturnDate = CRRVM.ReturnDate;
                
            Cars bookingCar = context.Cars
                .Where(c => c.Id == booking.CarId)
                .Select(c => c)
                .FirstOrDefault();

            bookingCar.FlaggedForCleaning = true;
            bookingCar.BookingsSinceService++;
            bookingCar.FlaggedForService = (bookingCar.BookingsSinceService % 3 == 0);
            bookingCar.MileageKm = (int)booking.MileageAfterKm;
            bookingCar.FlaggedForRemoval = (bookingCar.MileageKm > 15000);

            Customers customer = context.Customers
               .Where(c => c.Id == booking.CustomerId)
               .Select(c => c)
               .FirstOrDefault();

            decimal? cost = 0;
            int baseDayRental = 500;
            if (customer.BonusLevel >= 1)
            {
                baseDayRental = baseDayRental / 2;
            }
            int kmPrice = 2;
            int numberOfDays = (booking.BookingEnd - booking.BookingStart).Days;
            if (customer.BonusLevel >= 2)
            {
                if (numberOfDays == 3)
                {
                    numberOfDays = 2;
                }
                else if (numberOfDays >= 4)
                {
                    numberOfDays = numberOfDays - 2;
                }
            }

            int KmDrivenThisBooking = ((int)booking.MileageAfterKm - booking.MileageBeforeKm);
            if (customer.BonusLevel == 3)
            {
                KmDrivenThisBooking = KmDrivenThisBooking - 20;
                if (KmDrivenThisBooking < 0)
                {
                    KmDrivenThisBooking = 0;
                }
            }

            switch (context.Cars
                .Where(c => c.Id == context.Bookings
                    .Where(b => b.BookingNr == CRRVM.BookingNr)
                    .Select(b => b.CarId)
                    .FirstOrDefault())
                .Select(c => c.Cartype)
                .FirstOrDefault())
            {
                case "Small Car":
                    cost = baseDayRental * numberOfDays;
                    break;

                case "Van":
                    cost = baseDayRental * numberOfDays * (decimal)1.2
                        + kmPrice * KmDrivenThisBooking;
                    break;

                case "Minibus":
                    cost = baseDayRental * numberOfDays * (decimal)1.7
                        + kmPrice * KmDrivenThisBooking * (decimal)1.5;
                    break;
            }

            booking.Cost = Math.Round(cost.Value, 2);
            booking.IsReturned = true;

            customer.NrOfBookings++;
            customer.TotalKmDriven += (int)booking.MileageAfterKm - booking.MileageBeforeKm;
            eventRepository.UpdateCustomerBonus(customer.Id);

            context.Bookings.Update(booking);
            eventRepository.AddEvent(new Events
            {
                BookingId = booking.Id,
                CarId = booking.CarId,
                CustomerId = booking.CustomerId,
                EventType = "Returned Car",
                Date = DateTime.Now
            });
            context.SaveChanges();
        }

        public List<CarBoxVM> CheckCarAvailability(CarRentalCheckAvailabilityVM CRCAVM)
        {
            List<CarBoxVM> CarBoxVMList = new List<CarBoxVM>();

            CarBoxVMList.AddRange(context.Cars
               .Where(c => c.Active)
               .Select(c => new CarBoxVM
               {
                   Cartype = c.Cartype,
                   RegnNr = c.RegnNr,
                   MileageKm = c.MileageKm,
                   Id = c.Id
               }));

            foreach (var booking in context.Bookings)
            {
                if (CRCAVM.StartDate < booking.BookingEnd && booking.BookingStart < CRCAVM.EndDate)
                {
                    CarBoxVMList.Remove(CarBoxVMList
                        .Select(c => c)
                        .Where(c => c.Id == booking.CarId)
                        .FirstOrDefault());
                }
            }

            return CarBoxVMList;
        }

        public CheckCustomerDM CheckCustomer(CheckCustomerDM data)
        {
            if(context.Customers.Any(c => c.PersonNr == data.PersonNr))
            {
                return context.Customers
                    .Where(c => c.PersonNr == data.PersonNr)
                    .Select(c => new CheckCustomerDM
                    {
                        CustomerFound = true,
                        FirstName = c.FirstName,
                        LastName = c.LastName,
                    }).FirstOrDefault();
            }
            data.CustomerFound = false;
            return data;

        }

        public CarRentalGetBookingInfoVM GetBookingInfo(int BookingNr)
        {
            return context.Bookings
                .Where(o => o.Id == BookingNr)
                .Select(o => new CarRentalGetBookingInfoVM
                {
                    BookingNr = o.BookingNr,
                    BookingEndTime = o.BookingEnd,
                    BookingStartTime = o.BookingStart,
                    CarRegNr = o.Car.RegnNr,
                    MileageBefore = o.MileageBeforeKm
                }).FirstOrDefault();
        }
    }
}
