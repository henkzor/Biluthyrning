using Biluthyrning.Models.Entities;
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


    }
}
