namespace Web.Migrations
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Validation;
    using System.Linq;
    using System.Security.Claims;
    internal sealed class Configuration : DbMigrationsConfiguration<Web.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            SeedUsers(context);
            SeedPublicHolidays(context);
            SeedStates(context);
            context.SaveChanges(context);
            SeedLeaves(context);
            context.SaveChanges(context);
        }

        private void SeedUsers(ApplicationDbContext context)
        {
            ApplicationUser Linda = new ApplicationUser() { FullName = "Linda Jenkins", EmployeeCode = "0001", Email = "seefooterforemail@acme.com", UserName = "seefooterforemail@amce.com", Cell = "+36 55 217 736", EmploymentStartDate = new DateTime(2015, 01, 01), SecurityStamp = Guid.NewGuid().ToString() };

            ApplicationUser Milton = new ApplicationUser() { Manager = Linda, FullName = "Milton Coleman", EmployeeCode = "0002", Email = "miltoncoleman@amce.com", UserName = "miltoncoleman@amce.com", Cell = "+36 55 937 274", EmploymentStartDate = new DateTime(2016, 09, 01), SecurityStamp = Guid.NewGuid().ToString() };
            ApplicationUser Colin = new ApplicationUser() { Manager = Linda, FullName = "Colin Horton", EmployeeCode = "0003", Email = "colinhorton@amce.com", UserName = "colinhorton@amce.com", Cell = "+353 20 915 7545", EmploymentStartDate = new DateTime(2016, 05, 01), SecurityStamp = Guid.NewGuid().ToString() };
            ApplicationUser Ella = new ApplicationUser() { Manager = Colin, FullName = "Ella Jefferson", EmployeeCode = "2005", Email = "ellajefferson@acme.com", UserName = "ellajefferson@acme.com", Cell = "+36 55 979 367", EmploymentStartDate = new DateTime(2017, 01, 01), SecurityStamp = Guid.NewGuid().ToString() };
            ApplicationUser Earl = new ApplicationUser() { Manager = Colin, FullName = "Earl Craig", EmployeeCode = "2006", Email = "earlcraig@acme.com", UserName = "earlcraig@acme.com", Cell = "+353 20 916 5608", EmploymentStartDate = new DateTime(2017, 02, 01), SecurityStamp = Guid.NewGuid().ToString() };
            ApplicationUser Marsha = new ApplicationUser() { Manager = Colin, FullName = "Marsha Murphy", EmployeeCode = "2008", Email = "marshamurphy@acme.com", UserName = "marshamurphy@acme.com", Cell = "+36 55 949 891", EmploymentStartDate = new DateTime(2017, 02, 01), SecurityStamp = Guid.NewGuid().ToString() };
            ApplicationUser Luis = new ApplicationUser() { Manager = Colin, FullName = "Luis Ortega", EmployeeCode = "2009", Email = "luisortega@acme.com", UserName = "luisortega@acme.com", Cell = "+353 20 917 1339", EmploymentStartDate = new DateTime(2017, 03, 01), SecurityStamp = Guid.NewGuid().ToString() };
            ApplicationUser Faye = new ApplicationUser() { Manager = Colin, FullName = "Faye Dennis", EmployeeCode = "2010", Email = "fayedennis@acme.com", UserName = "fayedennis@acme.com", Cell = "", EmploymentStartDate = new DateTime(2017, 02, 01), SecurityStamp = Guid.NewGuid().ToString() };
            ApplicationUser Amy = new ApplicationUser() { Manager = Colin, FullName = "Amy Burns", EmployeeCode = "2012", Email = "amyburns@acme.com", UserName = "amyburns@acme.com", Cell = "+353 20 914 1775", EmploymentStartDate = new DateTime(2016, 01, 01), SecurityStamp = Guid.NewGuid().ToString() };
            ApplicationUser Darrel = new ApplicationUser() { Manager = Colin, FullName = "Darrel Weber", EmployeeCode = "2013", Email = "darrelweber@acme.com", UserName = "darrelweber@acme.com", Cell = "+36 55 615 463", EmploymentStartDate = new DateTime(2016, 01, 01), SecurityStamp = Guid.NewGuid().ToString() };
            ApplicationUser Charlotte = new ApplicationUser() { Manager = Milton, FullName = "Charlotte Osborne", EmployeeCode = "1005", Email = "charlotteosborne@acme.com", UserName = "charlotteosborne@acme.com", Cell = "+36 55 760 177", EmploymentStartDate = new DateTime(2017, 01, 01), SecurityStamp = Guid.NewGuid().ToString() };
            ApplicationUser Marie = new ApplicationUser() { Manager = Milton, FullName = "Marie Walters", EmployeeCode = "1006", Email = "mariewalters@acme.com", UserName = "mariewalters@acme.com", Cell = "+353 20 918 6908", EmploymentStartDate = new DateTime(2016, 02, 01), SecurityStamp = Guid.NewGuid().ToString() };
            ApplicationUser Leonard = new ApplicationUser() { Manager = Milton, FullName = "Leonard Gill", EmployeeCode = "1008", Email = "leonardgill@acme.com", UserName = "leonardgill@acme.com", Cell = "+36 55 525 585", EmploymentStartDate = new DateTime(2016, 03, 01), SecurityStamp = Guid.NewGuid().ToString() };
            ApplicationUser Enrique = new ApplicationUser() { Manager = Milton, FullName = "Enrique Thomas", EmployeeCode = "1009", Email = "enriquethomas@acme.com", UserName = "enriquethomas@acme.com", Cell = "+353 20 916 1335", EmploymentStartDate = new DateTime(2016, 01, 01), SecurityStamp = Guid.NewGuid().ToString() };
            ApplicationUser Omar = new ApplicationUser() { Manager = Milton, FullName = "Omar Dunn", EmployeeCode = "1010", Email = "omardunn@acme.com", UserName = "omardunn@acme.com", Cell = "", EmploymentStartDate = new DateTime(2017, 01, 01), SecurityStamp = Guid.NewGuid().ToString() };
            ApplicationUser Dewey = new ApplicationUser() { Manager = Milton, FullName = "Dewey George", EmployeeCode = "1012", Email = "deweygeorge@acme.com", UserName = "deweygeorge@acme.com", Cell = "+36 55 260 127", EmploymentStartDate = new DateTime(2016, 08, 01), SecurityStamp = Guid.NewGuid().ToString() };
            ApplicationUser Rudy = new ApplicationUser() { Manager = Milton, FullName = "Rudy Lewis", EmployeeCode = "1013", Email = "rudylewis@acme.com", UserName = "rudylewis@acme.com", Cell = "", EmploymentStartDate = new DateTime(2017, 01, 01), SecurityStamp = Guid.NewGuid().ToString() };
            ApplicationUser Neal = new ApplicationUser() { Manager = Milton, FullName = "Neal French", EmployeeCode = "1015", Email = "nealfrench@acme.com", UserName = "nealfrench@acme.com", Cell = "+353 20 919 4882", EmploymentStartDate = new DateTime(2016, 08, 14), SecurityStamp = Guid.NewGuid().ToString() };

            context.Users.AddOrUpdate(
                tbl => tbl.EmployeeCode,
                    Linda,
                 Milton,
                 Colin,
                 Ella,
                 Earl,
                 Marsha,
                 Luis,
                 Faye,
                 Amy,
                 Darrel,
                 Charlotte,
                 Marie,
                 Leonard,
                 Enrique,
                 Omar,
                 Dewey,
                 Rudy,
                 Neal
             );
        }

        private void SeedStates(ApplicationDbContext context)
        {
            LeaveState pending = new LeaveState()
            {
                Description = "Pending"
            };

            LeaveState denied = new LeaveState()
            {
                Description = "Denied"
            };

            LeaveState approved = new LeaveState()
            {
                Description = "Approved"
            };

            context.LeaveState.AddOrUpdate(
                tbl => tbl.Description,
                pending,
                approved,
                denied
            );
        }

        private void SeedLeaves(ApplicationDbContext context)
        {
            context.AnnualLeave.AddOrUpdate(
                tbl => tbl.StartDate,
                new AnnualLeave()
                {
                    StartDate = new DateTime(2017, 6, 3),
                    EndDate = new DateTime(2017, 6, 8),
                    LeaveStateID = context.LeaveState.Where(tbl => tbl.Description == "Pending").FirstOrDefault().LeaveStateID,
                    UserID = context.Users.Where(tbl => tbl.Email == "ellajefferson@acme.com").FirstOrDefault().Id,
                    RequestDate = new DateTime(2017, 5, 2)
                },
                new AnnualLeave()
                {
                    StartDate = new DateTime(2017, 5, 3),
                    EndDate = new DateTime(2017, 5, 10),
                    LeaveStateID = context.LeaveState.Where(tbl => tbl.Description == "Approved").FirstOrDefault().LeaveStateID,
                    UserID = context.Users.Where(tbl => tbl.Email == "miltoncoleman@amce.com").FirstOrDefault().Id,
                    RequestDate = new DateTime(2017, 5, 2)
                }
            );

            context.StudyLeave.AddOrUpdate(
                tbl => tbl.StartDate,
                new StudyLeave()
                {
                    StartDate = new DateTime(2017, 5, 3),
                    EndDate = new DateTime(2017, 5, 10),
                    LeaveStateID = context.LeaveState.Where(tbl => tbl.Description == "Approved").FirstOrDefault().LeaveStateID,
                    UserID = context.Users.Where(tbl => tbl.Email == "miltoncoleman@amce.com").FirstOrDefault().Id,
                    RequestDate = new DateTime(2017, 5, 2),
                    Institution = "Unisa",
                    StudentNumber = "x1"
                }
            );
        }

        private void SeedPublicHolidays(ApplicationDbContext context)
        {
            context.PublicHoliday.AddOrUpdate(
                tbl => tbl.HolidayDate,
                new PublicHoliday() { HolidayDate = new DateTime(2017, 1, 1), Name = "New Year's Day" },
                new PublicHoliday() { HolidayDate = new DateTime(2017, 1, 2), Name = "Public holiday" },
                new PublicHoliday() { HolidayDate = new DateTime(2017, 4, 13), Name = "Human Rights Day" },
                new PublicHoliday() { HolidayDate = new DateTime(2017, 4, 14), Name = "Good Friday" },
                new PublicHoliday() { HolidayDate = new DateTime(2017, 4, 17), Name = "Family Day" },
                new PublicHoliday() { HolidayDate = new DateTime(2017, 4, 27), Name = "Freedom Day" },
                new PublicHoliday() { HolidayDate = new DateTime(2017, 5, 1), Name = "Workers Day" },
                new PublicHoliday() { HolidayDate = new DateTime(2017, 6, 16), Name = "Youth Day" },
                new PublicHoliday() { HolidayDate = new DateTime(2017, 8, 9), Name = "National Women’s Day" },
                new PublicHoliday() { HolidayDate = new DateTime(2017, 9, 24), Name = "Heritage Day" },
                new PublicHoliday() { HolidayDate = new DateTime(2017, 9, 25), Name = "Public holiday" },
                new PublicHoliday() { HolidayDate = new DateTime(2017, 12, 16), Name = "Day of Reconciliation" },
                new PublicHoliday() { HolidayDate = new DateTime(2017, 12, 2), Name = "Christmas Day" },
                new PublicHoliday() { HolidayDate = new DateTime(2017, 12, 26), Name = "Day of Goodwill" }
                );
        }
    }
}



















