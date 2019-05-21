using Biluthyrning.Models.Entities;
using Biluthyrning.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biluthyrning.Models
{
    public class HomeService
    {
        readonly BiluthyrningDBContext context;
        public HomeService(BiluthyrningDBContext context)
        {
            this.context = context;

        }

        public HomeIndexVM CreateNewPerson()
        {
            return new HomeIndexVM { Name = "Stefan", Age = 40 };
        }

        
    }
}
