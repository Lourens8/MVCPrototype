using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Models;

namespace Web.Controllers.BusinessRules
{
    public class StudyAccrual : IAccrual
    {
        public decimal Amount = 10m;

        public List<Leave> AccrueFromEmploymentStartDate(DateTime employmentStartDate)
        {
            List<Leave> study = new List<Leave>();

            DateTime cycleStart = FiscalYearStart(employmentStartDate);
            DateTime cycleEnd = FiscalYearStart(DateTime.Now);

            for (DateTime date = cycleStart; date <= cycleEnd; date = date.AddYears(1) )
            {
                study.Add(new StudyLeave()
                {
                    Amount = Amount,
                    Description = "Study Leave Accrual",
                    StartDate = date,
                    EndDate = date.AddYears(1),
                    Expires = true
                });
            }

            return study;
        }

        public Type Handles()
        {
            return typeof(StudyLeave);
        }

        private DateTime FiscalYearStart(DateTime input)
        {
            DateTime output;

            if (input.Month < 3)
                output = new DateTime(input.Year - 1, 3, 1);
            else
                output = new DateTime(input.Year, 3, 1);

            return output;
        }
    }
}