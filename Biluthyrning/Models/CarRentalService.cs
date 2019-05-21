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
        public CarRentalService(BiluthyrningDBContext context)
        {
            this.context = context;
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
                        LastName = CRBVM.LastName
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
            context.SaveChanges();

            return booking.BookingNr;
        }

         

        public CarRentalShowBookingVM GetCarForShowBooking(int BookingNr)
        {
            CarRentalShowBookingVM CRSBVM = new CarRentalShowBookingVM();

            CRSBVM.BookingNr = BookingNr;

            CRSBVM.CustomerPersonNr = context.Customers
                .Where(c => c.Id == context.Bookings
                    .Where(b => b.BookingNr == BookingNr)
                    .Select(b => b.CustomerId)
                    .First())
                .Select(c => c.PersonNr)
                .FirstOrDefault();

            CRSBVM.CarType = context.Cars
                .Where(c => c.Id == context.Bookings
                    .Where(b => b.BookingNr == BookingNr)
                    .Select(b => b.CarId)
                    .FirstOrDefault())
                .Select(c => c.Cartype)
                .FirstOrDefault();

            CRSBVM.CarRegNr = context.Cars
                .Where(c => c.Id == context.Bookings
                    .Where(b => b.BookingNr == BookingNr)
                    .Select(b => b.CarId)
                    .FirstOrDefault())
                .Select(c => c.RegnNr)
                .FirstOrDefault();

            CRSBVM.BookingStartTime = context.Bookings
                .Where(b => b.BookingNr == BookingNr)
                .Select(b => b.BookingStart)
                .FirstOrDefault();

            CRSBVM.BookingEndTime = context.Bookings
                .Where(b => b.BookingNr == BookingNr)
                .Select(b => b.BookingEnd)
                .FirstOrDefault();

            CRSBVM.MileageBefore = context.Cars
                .Where(c => c.Id == context.Bookings
                    .Where(b => b.BookingNr == BookingNr)
                    .Select(b => b.CarId)
                    .FirstOrDefault())
                .Select(c => c.MileageKm)
                .FirstOrDefault();

            CRSBVM.MileageAfter = context.Bookings
                .Where(b => b.BookingNr == BookingNr)
                .Select(b => b.MileageAfterKm)
                .FirstOrDefault();

            CRSBVM.Cost = context.Bookings
                .Where(b => b.BookingNr == BookingNr)
                .Select(b => b.Cost)
                .FirstOrDefault();

            return CRSBVM;
        }

        public void ReturnCar(CarRentalReturnVM CRRVM)
        {
            Bookings booking = context.Bookings
                .Where(b => b.BookingNr == CRRVM.BookingNr)
                .Select(b => b)
                .FirstOrDefault();

            booking.MileageAfterKm = CRRVM.MileageReturnKm;
            booking.ReturnDate = CRRVM.ReturnDate;

            decimal? cost = 0;
            int baseDayRental = 500;
            int kmPrice = 2;
            TimeSpan numberOfDays = booking.BookingEnd - booking.BookingStart;


            switch (context.Cars
                .Where(c => c.Id == context.Bookings
                    .Where(b => b.BookingNr == CRRVM.BookingNr)
                    .Select(b => b.CarId)
                    .FirstOrDefault())
                .Select(c => c.Cartype)
                .FirstOrDefault())
            {
                case "Small Car":
                    cost = baseDayRental * numberOfDays.Days;
                    break;

                case "Van":
                    cost = baseDayRental * numberOfDays.Days * (decimal)1.2
                        + kmPrice * (booking.MileageAfterKm - booking.MileageBeforeKm);
                    break;

                case "Minibus":
                    cost = baseDayRental * numberOfDays.Days * (decimal)1.7
                        + kmPrice * (booking.MileageAfterKm - booking.MileageBeforeKm) * (decimal)1.5;
                    break;
            }

            booking.Cost = Math.Round(cost.Value, 2);
            booking.IsReturned = true;

            context.Bookings.Update(booking);
            context.SaveChanges();
                    
        }

        public List<CarBoxVM> CheckCarAvailability(CarRentalCheckAvailabilityVM CRCAVM)
        {
            List<CarBoxVM> CarBoxVMList = new List<CarBoxVM>();
            //List<int> UnAvailibleCarsList = new List<int>();
            //List<int> AvailibleCarsList = new List<int>();

            CarBoxVMList.AddRange(context.Cars
               .Select(c => new CarBoxVM
               {
                   Cartype = c.Cartype,
                   RegnNr = c.RegnNr,
                   MileageKm = c.MileageKm,
                   Id = c.Id
               }));

            foreach (var booking in context.Bookings)
            {
                if ( CRCAVM.StartDate < booking.BookingEnd && booking.BookingStart < CRCAVM.EndDate)
                {
                    CarBoxVMList.Remove(CarBoxVMList
                        .Select(c => c)
                        .Where(c => c.Id == booking.CarId)
                        .FirstOrDefault());
                    //UnAvailibleCarsList.Add(booking.CarId);
                    //if (AvailibleCarsList.Contains(booking.CarId))
                    //{
                    //    AvailibleCarsList.Remove(booking.CarId); 
                    //}
                }
            }

            //return AvailibleCarsList;
            return CarBoxVMList;
        }

    }
}
