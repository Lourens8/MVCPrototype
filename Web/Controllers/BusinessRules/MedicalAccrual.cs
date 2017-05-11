using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Models;

namespace Web.Controllers.BusinessRules
{
    public class MedicalAccrual : IAccrual
    {
        public decimal Amount = 30m;

        public List<Leave> AccrueFromEmploymentStartDate(DateTime employmentStartDate)
        {
            List<Leave> medical = new List<Leave>();

            //Every 2 years
            for(DateTime cycleDate = employmentStartDate.Date; cycleDate < DateTime.Now; cycleDate = cycleDate.AddYears(2))
            {
                medical.Add(new MedicalLeave()
                {
                    StartDate = cycleDate,
                    EndDate = cycleDate.AddYears(2),
                    Description = "Medical Leave Accrual",
                    Expires = true,
                    Amount = Amount
                });
            }

            return medical;
        }

        public Type Handles()
        {
            return typeof(MedicalLeave);
        }
    }
}