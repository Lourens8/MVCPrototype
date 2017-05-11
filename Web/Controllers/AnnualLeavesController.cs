using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class AnnualLeavesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AnnualLeaves
        public ActionResult Index()
        {
            var leave = db.AnnualLeave.Include(a => a.State).Include(a => a.User);
            return View(leave.ToList());
        }

        // GET: AnnualLeaves/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnnualLeave annualLeave = db.AnnualLeave.Find(id);
            if (annualLeave == null)
            {
                return HttpNotFound();
            }
            return View(annualLeave);
        }

        // GET: AnnualLeaves/Create
        public ActionResult Create()
        {
            ViewBag.LeaveStateID = new SelectList(db.LeaveState, "LeaveStateID", "Description");
            ViewBag.UserID = new SelectList(db.Users, "Id", "EmployeeCode");
            return View();
        }

        // POST: AnnualLeaves/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "LeaveID,LeaveStateID,UserID,StartDate,EndDate,RequestDate,Note")] AnnualLeave annualLeave)
        {
            if (ModelState.IsValid)
            {
                db.AnnualLeave.Add(annualLeave);
                db.SaveChanges();
            }

            return Redirect("/");
        }

        // GET: AnnualLeaves/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnnualLeave annualLeave = db.AnnualLeave.Find(id);
            if (annualLeave == null)
            {
                return HttpNotFound();
            }
            ViewBag.LeaveStateID = new SelectList(db.LeaveState, "LeaveStateID", "Description", annualLeave.LeaveStateID);
            ViewBag.UserID = new SelectList(db.Users, "Id", "FullName", annualLeave.UserID);
            return View(annualLeave);
        }

        // POST: AnnualLeaves/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit([Bind(Include = "LeaveID,LeaveStateID,UserID,StartDate,EndDate,RequestDate,Note")] AnnualLeave annualLeave)
        {
            if (ModelState.IsValid)
            {
                db.Entry(annualLeave).State = EntityState.Modified;
                db.SaveChanges();
            }

            return Redirect("/");
        }

        // GET: AnnualLeaves/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnnualLeave annualLeave = db.AnnualLeave.Find(id);
            if (annualLeave == null)
            {
                return HttpNotFound();
            }
            return View(annualLeave);
        }

        // POST: AnnualLeaves/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AnnualLeave annualLeave = db.AnnualLeave.Find(id);
            db.AnnualLeave.Remove(annualLeave);
            db.SaveChanges();

            return Redirect("/");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
