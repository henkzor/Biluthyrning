using Biluthyrning.Models.Entities;
using Biluthyrning.Models.ViewModels.Event;
using Biluthyrning.Models.ViewModels.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biluthyrning.Models
{
    public class EventService
    {

        readonly BiluthyrningDBContext context;
        public EventService(BiluthyrningDBContext context)
        {
            this.context = context;
        }

        public EventIndexVM GetAllEvents()
        {
            EventIndexVM EIVM = new EventIndexVM();
            EIVM.EventBoxVMList = new List<EventBoxVM>();

            foreach (var item in context.Events)
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


                EIVM.EventBoxVMList.Add(EBVM);
            }

            return EIVM;
        }
    }
}
