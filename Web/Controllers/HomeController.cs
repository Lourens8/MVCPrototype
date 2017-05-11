using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Controllers.BusinessRules;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            this._context = context;
        }

        [Authorize]
        public ActionResult Index()
        {
            ApplicationUser currentUser = (from u in _context.Users
                                           where u.UserName == User.Identity.Name
                                           select u).FirstOrDefault();

            LeaveList list = new LeaveList();

            list.Holidays = _context.PublicHoliday.ToList();

            list.UserLeaves = (from u in _context.Users
                               where u.Manager.Id == currentUser.Id
                               select new UserLeave()
                               {
                                   UserID = u.Id,
                                   Leaves = (from tbl in _context.Leave where tbl.User.Id == u.Id select tbl).ToList(),
                                   Name = u.FullName,
                                   EmploymentStartDate = u.EmploymentStartDate
                               }).ToList();

            List<UserLeave> subLeaves = (from u in _context.Users
                                         where u.Manager.Manager.Id == currentUser.Id
                                         select new UserLeave()
                                         {
                                             UserID = u.Id,
                                             Leaves = (from tbl in _context.Leave where tbl.User.Id == u.Id select tbl).ToList(),
                                             Name = u.FullName,
                                             EmploymentStartDate = u.EmploymentStartDate
                                         }).ToList();

            list.UserLeaves.AddRange(subLeaves);

            list.Leaves = _context.Leave.Where(tbl => tbl.User.Id == currentUser.Id).ToList();

            list.Leaves = Deferral.AddDeferral(list.Leaves, list.Holidays);
            list.Leaves = Accrual.AddAccrual(currentUser.EmploymentStartDate, currentUser.FullName, list.Leaves);

            foreach(UserLeave userLeave in list.UserLeaves)
            {
                userLeave.Leaves = Deferral.AddDeferral(userLeave.Leaves, list.Holidays);
                userLeave.Leaves = Accrual.AddAccrual(userLeave.EmploymentStartDate, userLeave.Name, userLeave.Leaves);
            }

            list.FormModel = new LeaveFormModel()
            {
                StartDate = DateTime.Now.Date,
                EndDate = DateTime.Now.Date,
                UserID = currentUser.Id,
                RequestDate = DateTime.Now,
                LeaveStateID = 1,
                Note = "",
                Attachement = "",
                Institution = "",
                StudentNumber = ""
            };

            ViewBag.LeaveStateID = new SelectList(_context.LeaveState, "LeaveStateID", "Description", 1);

            return View(list);
        }

        public ActionResult Login()
        {
            return View();
        }
    }
}