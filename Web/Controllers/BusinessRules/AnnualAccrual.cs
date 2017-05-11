using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Models;

namespace Web.Controllers.BusinessRules
{
    public class AnnualAccrual : IAccrual
    {
        public decimal Amount = 25m;

        public List<Leave> AccrueFromEmploymentStartDate(DateTime employmentStartDate)
        {
            List<Leave> annual = new List<Leave>();
            int skip = 0;

            DateTime cycleStartDate = new DateTime(employmentStartDate.Year, employmentStartDate.Month, 1);

            if (employmentStartDate.Day != 1)
            {
                skip = 1;

                decimal leave = Accrual.Prorate(employmentStartDate, cycleStartDate, cycleStartDate.AddMonths(1), Amount);

                annual.Add(new AnnualLeave()
                {
                    StartDate = employmentStartDate,
                    EndDate = cycleStartDate.AddMonths(1),
                    Description = "Annual Leave Accrual"
                });
            }

            decimal monthlyAmount = Amount / 12;

            //Foreach month from employment start date untill next year june
            //The latest time you may request leave is 6 months into the next cycle
            for (DateTime startDate = cycleStartDate.AddMonths(skip); startDate <= new DateTime(DateTime.Now.Year + 1, 6, 1); startDate = startDate.AddMonths(1))
            {
                annual.Add(new AnnualLeave()
                {
                    StartDate = startDate,
                    EndDate = startDate.AddMonths(1),
                    Description = "Annual Leave Accrual",
                    Amount = monthlyAmount
                });
            }

            return annual;
        }

        public Type Handles()
        {
            return typeof(AnnualLeave);
        }
    }
}