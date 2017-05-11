using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Models;

namespace Web.Controllers.BusinessRules
{
    public static class Accrual
    {
        static List<IAccrual> AccrualRules;

        static Accrual()
        {
            AccrualRules = new List<IAccrual>();

            var results = from type in System.Reflection.Assembly.GetExecutingAssembly().GetTypes()
                          where typeof(IAccrual).IsAssignableFrom(type)
                          where !(type == typeof(IAccrual))
                          select type;

            foreach (var item in results)
            {
                AccrualRules.Add(Activator.CreateInstance(item) as IAccrual);
            }
        }

        public static List<Leave> AddAccrual(DateTime employmentStartDate, string name, List<Leave> leaves)
        {
            List<Leave> accruals = new List<Leave>();

            foreach (IAccrual accrual in AccrualRules)
            {
                List<Leave> accrualByType = accrual.AccrueFromEmploymentStartDate(employmentStartDate);
                List<Leave> leavesByType = leaves.Where(tbl => accrual.Handles().IsAssignableFrom(tbl.GetType())).ToList();

                foreach (Leave leave in accrualByType)
                {
                    if (leave.StartDate > DateTime.Now)
                    {
                        leave.IsFuture = true;
                    }
                }

                leavesByType.AddRange(accrualByType);

                List<Leave> balances = CalculateRunningBalance(leavesByType, name);

                Leave currentLeave = balances.OrderByDescending(tbl => tbl.StartDate).Where(tbl => tbl.StartDate < DateTime.Now).FirstOrDefault();

                if (currentLeave != null)
                    currentLeave.IsCurrent = true;

                accruals.AddRange(balances);
            };

            accruals = accruals.OrderByDescending(tbl => tbl.StartDate).ToList();

            return accruals;
        }

        public static List<Leave> CalculateRunningBalance(List<Leave> leaves, string name)
        {
            leaves = leaves.OrderBy(tbl => tbl.StartDate).ToList();

            List<Leave> balances = new List<Leave>();

            decimal runningTotal = 0;
            bool expires = false;

            foreach (Leave leave in leaves)
            {
                leave.TypeName = leave.GetType().ToString().Split('.').Last();
                leave.Name = name;

                if (leave.LeaveID > 0)
                {
                    leave.Description = $"{leave.TypeName} Request";
                }

                //Expiry
                if (expires && leave.LeaveID == 0)
                {

                    Leave expiry = (Leave)Activator.CreateInstance(leave.GetType());
                    expiry.StartDate = leave.StartDate.AddDays(-1);
                    expiry.EndDate = leave.StartDate;
                    expiry.Description = "Expiry";
                    expiry.TypeName = leave.TypeName;
                    expiry.Amount = -runningTotal;

                    balances.Add(expiry);

                    runningTotal = 0;
                }

                expires = leave.Expires;

                runningTotal += leave.Amount;

                leave.RunningTotal = runningTotal;

                balances.Add(leave);
            }

            return balances;
        }

        public static decimal Prorate(DateTime startDate, DateTime cycleStartDate, DateTime endDate, decimal amount)
        {
            TimeSpan cycle = endDate - startDate;
            TimeSpan actual = endDate - cycleStartDate;

            if (cycle.Days == 0)
                return 0;

            decimal prorated = amount * actual.Days / cycle.Days;

            prorated = Math.Ceiling(prorated);

            if (prorated > amount)
                return amount;

            return prorated;
        }
    }
}