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
    public class StudyLeavesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: StudyLeaves
        public ActionResult Index()
        {
            var leave = db.StudyLeave.Include(s => s.State).Include(s => s.User);
            return View(leave.ToList());
        }

        // GET: StudyLeaves/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudyLeave studyLeave = db.StudyLeave.Find(id);
            if (studyLeave == null)
            {
                return HttpNotFound();
            }
            return View(studyLeave);
        }

        // GET: StudyLeaves/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StudyLeaves/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "LeaveID,LeaveStateID,UserID,StartDate,EndDate,RequestDate,Note,StudentNumber,Institution")] StudyLeave studyLeave)
        {
            if (ModelState.IsValid)
            {
                db.StudyLeave.Add(studyLeave);
                db.SaveChanges();
            }
            
            return Redirect("/");
        }

        // GET: StudyLeaves/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudyLeave studyLeave = db.StudyLeave.Find(id);
            if (studyLeave == null)
            {
                return HttpNotFound();
            }
            ViewBag.LeaveStateID = new SelectList(db.LeaveState, "LeaveStateID", "Description", studyLeave.LeaveStateID);
            ViewBag.UserID = new SelectList(db.Users, "Id", "EmployeeCode", studyLeave.UserID);
            return View(studyLeave);
        }

        // POST: StudyLeaves/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit([Bind(Include = "LeaveID,LeaveStateID,UserID,StartDate,EndDate,RequestDate,Note,StudentNumber,Institution")] StudyLeave studyLeave)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studyLeave).State = EntityState.Modified;
                db.SaveChanges();
            }

            return Redirect("/");
        }

        // GET: StudyLeaves/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudyLeave studyLeave = db.StudyLeave.Find(id);
            if (studyLeave == null)
            {
                return HttpNotFound();
            }
            return View(studyLeave);
        }

        // POST: StudyLeaves/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            StudyLeave studyLeave = db.StudyLeave.Find(id);
            db.StudyLeave.Remove(studyLeave);
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
