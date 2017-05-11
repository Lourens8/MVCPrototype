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
    public class MedicalLeavesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MedicalLeaves
        public ActionResult Index()
        {
            var leave = db.MedicalLeave.Include(m => m.State).Include(m => m.User);
            return View(leave.ToList());
        }

        // GET: MedicalLeaves/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MedicalLeave medicalLeave = db.MedicalLeave.Find(id);
            if (medicalLeave == null)
            {
                return HttpNotFound();
            }
            return View(medicalLeave);
        }

        // GET: MedicalLeaves/Create
        public ActionResult Create()
        {
            ViewBag.LeaveStateID = new SelectList(db.LeaveState, "LeaveStateID", "Description");
            ViewBag.UserID = new SelectList(db.Users, "Id", "FullName");
            return View();
        }

        // POST: MedicalLeaves/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "LeaveID,LeaveStateID,UserID,StartDate,EndDate,RequestDate,Note,Attachement")] MedicalLeave medicalLeave)
        {
            if (ModelState.IsValid)
            {
                db.MedicalLeave.Add(medicalLeave);
                db.SaveChanges();
            }

            return Redirect("/");
        }

        // GET: MedicalLeaves/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MedicalLeave medicalLeave = db.MedicalLeave.Find(id);
            if (medicalLeave == null)
            {
                return HttpNotFound();
            }
            ViewBag.LeaveStateID = new SelectList(db.LeaveState, "LeaveStateID", "Description", medicalLeave.LeaveStateID);
            ViewBag.UserID = new SelectList(db.Users, "Id", "EmployeeCode", medicalLeave.UserID);
            return View(medicalLeave);
        }

        // POST: MedicalLeaves/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit([Bind(Include = "LeaveID,LeaveStateID,UserID,StartDate,EndDate,RequestDate,Note,Attachement")] MedicalLeave medicalLeave)
        {
            if (ModelState.IsValid)
            {
                db.Entry(medicalLeave).State = EntityState.Modified;
                db.SaveChanges();
            }

            return Redirect("/");
        }

        // GET: MedicalLeaves/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MedicalLeave medicalLeave = db.MedicalLeave.Find(id);
            if (medicalLeave == null)
            {
                return HttpNotFound();
            }
            return View(medicalLeave);
        }

        // POST: MedicalLeaves/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MedicalLeave medicalLeave = db.MedicalLeave.Find(id);
            db.MedicalLeave.Remove(medicalLeave);
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
