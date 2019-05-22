using Biluthyrning.Models.Entities;
using Biluthyrning.Models.ViewModels.Car;
using Biluthyrning.Models.ViewModels.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biluthyrning.Models
{
    public class CarService
    {

        readonly BiluthyrningDBContext context;
        public CarService(BiluthyrningDBContext context)
        {
            this.context = context;
        }

        public void AddCar(CarAddCarVM CACVM)
        {
            Cars car = new Cars();

            car.Cartype = CACVM.Cartype;
            car.MileageKm = CACVM.MileageKm;
            car.RegnNr = CACVM.RegNr;
            car.FlaggedForCleaning = false;
            car.FlaggedForService = false;
            car.FlaggedForRemoval = false;
            car.BookingsSinceService = 0;
            car.Active = true;
            
            context.Cars.Add(car);
            context.SaveChanges();
        }

        public CarIndexVM GetAllCars()
        {
            CarIndexVM CIVM = new CarIndexVM();
            CIVM.CarBoxVMList = new List<CarBoxVM>();

            CIVM.CarBoxVMList.AddRange(context.Cars
                .Where(c => c.Active)
                .Select(c => new CarBoxVM
                {
                    Id = c.Id,
                    Cartype = c.Cartype,
                    RegnNr = c.RegnNr,
                    MileageKm = c.MileageKm,
                    FlaggedForCleaning = c.FlaggedForCleaning,
                    FlaggedForService = c.FlaggedForService,
                    FlaggedForRemoval = c.FlaggedForRemoval   
                }));

            return CIVM;
        }

        

        public List<CarBoxVM> GetCarsByID(List<int> CarIDs)
        {

            List<CarBoxVM> CarBoxVMList = new List<CarBoxVM>();

            CarBoxVMList.AddRange(context.Cars
               .Where(c => CarIDs.Contains(c.Id))
               .Select(c => new CarBoxVM
               {
                   Cartype = c.Cartype,
                   RegnNr = c.RegnNr,
                   MileageKm = c.MileageKm
               }));

            return CarBoxVMList;
        }

        public void CleanCar (CarPerformActionVM CPAVM)
        {
            Cars carToClean = context.Cars
                .Select(c => c)
                .Where(c => c.Id == CPAVM.CarId)
                .FirstOrDefault();

            carToClean.FlaggedForCleaning = false;

            context.Events.Add(new Events
            {
                CarId = CPAVM.CarId,
                Date = DateTime.Now,
                EventType = "Cleaning"
            });
            context.SaveChanges();
        }

        public void ServiceCar(CarPerformActionVM CPAVM)
        {
            Cars carToClean = context.Cars
                .Select(c => c)
                .Where(c => c.Id == CPAVM.CarId)
                .FirstOrDefault();

            carToClean.FlaggedForService= false;

            context.Events.Add(new Events
            {
                CarId = CPAVM.CarId,
                Date = DateTime.Now,
                EventType = "Service"
            });

            context.SaveChanges();
        }
        public void RemoveCar(CarPerformActionVM CPAVM)
        {
            //Lös detta genom att sätta bilen till en "retired"
            //context.Cars.Remove(
            //        context.Cars
            //        .Select(c => c)
            //        .Where(c => c.Id == CPAVM.CarId)
            //        .FirstOrDefault()
            //    );

            Cars carToRetire = context.Cars
                .Select(c => c)
                .Where(c => c.Id == CPAVM.CarId)
                .FirstOrDefault();

            carToRetire.Active = false;

            context.Events.Add(new Events
            {
                CarId = CPAVM.CarId,
                Date = DateTime.Now,
                EventType = "Removal"
            });

            context.SaveChanges();
        }
    }
}
