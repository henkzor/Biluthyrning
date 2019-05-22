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
            

            context.Cars.Add(car);
            context.SaveChanges();
        }

        public CarIndexVM GetAllCars()
        {
            CarIndexVM CIVM = new CarIndexVM();
            CIVM.CarBoxVMList = new List<CarBoxVM>();

            CIVM.CarBoxVMList.AddRange(context.Cars
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
            context.SaveChanges();
        }

        public void ServiceCar(CarPerformActionVM CPAVM)
        {
            Cars carToClean = context.Cars
                .Select(c => c)
                .Where(c => c.Id == CPAVM.CarId)
                .FirstOrDefault();

            carToClean.FlaggedForService= false;
            context.SaveChanges();
        }
        public void RemoveCar(CarPerformActionVM CPAVM)
        {
            //context.Cars.Remove(c => c)
            //    .Where(c => c.Id == CPAVM.CarId)
            //    .FirstOrDefault();

            context.Cars.Remove(
                    context.Cars
                    .Select(c => c)
                    .Where(c => c.Id == CPAVM.CarId)
                    .FirstOrDefault()
                );

            //Cars carToClean = context.Cars
            //    .Select(c => c)
            //    .Where(c => c.Id == CPAVM.CarId)
            //    .FirstOrDefault();

            //carToClean.FlaggedForRemoval = false;
            context.SaveChanges();
        }
    }
}
