using Biluthyrning.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biluthyrning.Models
{
    public class EventRepository
    {
        readonly BiluthyrningDBContext context;
        public EventRepository(BiluthyrningDBContext context)
        {
            this.context = context;
        }

        public void UpdateCustomerBonus(int inputCustomerId)
        {
            Customers customer = context.Customers
                .Select(c => c)
                .Where(c => c.Id == inputCustomerId)
                .FirstOrDefault();

            if (customer.BonusLevel == 0 && customer.NrOfBookings >= 3)
            {
                customer.BonusLevel = 1;
                context.Events.Add(new Events
                {
                    CustomerId = customer.Id,
                    EventType = "Upgraded bonus level",
                    Date = DateTime.Now
                });
            }
            else if ((customer.BonusLevel == 1 || customer.BonusLevel == 2)
                && customer.NrOfBookings >= 5 && customer.TotalKmDriven >= 1000)
            {
                customer.BonusLevel = 3;
                context.Events.Add(new Events
                {
                    CustomerId = customer.Id,
                    EventType = "Upgraded bonus level",
                    Date = DateTime.Now
                });
            }
            else if (customer.BonusLevel == 1 && customer.NrOfBookings >= 5)
            {
                customer.BonusLevel = 2;
                context.Events.Add(new Events
                {
                    CustomerId = customer.Id,
                    EventType = "Upgraded bonus level",
                    Date = DateTime.Now
                });
            }
        }

        public void AddEvent(Events inputEvent)
        {
            context.Events.Add(inputEvent);
        }

    }
}
