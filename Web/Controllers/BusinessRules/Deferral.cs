using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Models;

namespace Web.Controllers.BusinessRules
{
    public static class Deferral
    {
        public static List<Leave> AddDeferral(List<Leave> leaves, List<PublicHoliday> holidays)
        {
            foreach(Leave leave in leaves)
            {
                int days = 0;

                if (leave.LeaveStateID == 3)
                    continue;

                for(DateTime startDate = leave.StartDate.Date; startDate<= leave.EndDate.Date; startDate = startDate.AddDays(1))
                {
                    if(!holidays.Any(tbl=>tbl.HolidayDate == startDate) && startDate.DayOfWeek != DayOfWeek.Saturday && startDate.DayOfWeek != DayOfWeek.Sunday)
                    {
                        days++;
                    }
                }

                leave.Amount = -days;
            }

            return leaves;
        }
    }
}