using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JCold_UVU_MVC_Inventory.Models;

namespace JCold_UVU_MVC_Inventory.Controllers
{
    public class CheckedOutsController : Controller
    {
        private JCold_UVU_MVC_InventoryDb db = new JCold_UVU_MVC_InventoryDb();

        // GET: CheckedOuts
        public ActionResult Index()
        {
            var checkedOuts = db.CheckedOuts.Include(c => c.Books).Include(c => c.Department).Include(c => c.Students).Include(c => c.Supplies);
            return View(checkedOuts.ToList());
        }

        // GET: CheckedOuts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CheckedOut checkedOut = db.CheckedOuts.Find(id);
            if (checkedOut == null)
            {
                return HttpNotFound();
            }
            return View(checkedOut);
        }

        // GET: CheckedOuts/Create
        public ActionResult Create()
        {
            ViewBag.BooksID = new SelectList(db.Books, "BooksID", "Title");
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "DepName");
            ViewBag.StudentsID = new SelectList(db.Students, "StudentsID", "StudentName");
            ViewBag.SuppliesID = new SelectList(db.Supplies, "SuppliesID", "Name");
            return View();
        }

        // POST: CheckedOuts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CheckedOutID,StudentsID,BooksID,SuppliesID,DepartmentID,Returned,ReturnedDate,CheckedOutDate")] CheckedOut checkedOut)
        {
            // Due Date Feature to try and flip a boolen in the Books table when an entry exists
            if (db.Books.Find(checkedOut.BooksID) != null)
            {
                bool AvailableStatus = false;
                int BookIDvar = 1;
                var Availability = new Books() { Available = AvailableStatus, BooksID = BookIDvar };
                db.Entry(Availability).Property(x => x.BooksID);
            }

            if (ModelState.IsValid)
            {
                db.CheckedOuts.Add(checkedOut);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BooksID = new SelectList(db.Books, "BooksID", "Title", checkedOut.BooksID);
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "DepName", checkedOut.DepartmentID);
            ViewBag.StudentsID = new SelectList(db.Students, "StudentsID", "StudentName", checkedOut.StudentsID);
            ViewBag.SuppliesID = new SelectList(db.Supplies, "SuppliesID", "Name", checkedOut.SuppliesID);
            return View(checkedOut);
        }

        // GET: CheckedOuts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CheckedOut checkedOut = db.CheckedOuts.Find(id);
            if (checkedOut == null)
            {
                return HttpNotFound();
            }
            ViewBag.BooksID = new SelectList(db.Books, "BooksID", "Title", checkedOut.BooksID);
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "DepName", checkedOut.DepartmentID);
            ViewBag.StudentsID = new SelectList(db.Students, "StudentsID", "StudentName", checkedOut.StudentsID);
            ViewBag.SuppliesID = new SelectList(db.Supplies, "SuppliesID", "Name", checkedOut.SuppliesID);
            return View(checkedOut);
        }

        // POST: CheckedOuts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CheckedOutID,StudentsID,BooksID,SuppliesID,DepartmentID,Returned,ReturnedDate,CheckedOutDate")] CheckedOut checkedOut)
        {
            if (checkedOut.Returned)
            {
                DateTime CurrentDate = DateTime.Now;
                checkedOut.ReturnedDate = CurrentDate;
                db.Entry(checkedOut).State = EntityState.Modified;
            }
            if (ModelState.IsValid)
            {
                db.Entry(checkedOut).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BooksID = new SelectList(db.Books, "BooksID", "Title", checkedOut.BooksID);
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "DepName", checkedOut.DepartmentID);
            ViewBag.StudentsID = new SelectList(db.Students, "StudentsID", "StudentName", checkedOut.StudentsID);
            ViewBag.SuppliesID = new SelectList(db.Supplies, "SuppliesID", "Name", checkedOut.SuppliesID);
            return View(checkedOut);
        }

        // GET: CheckedOuts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CheckedOut checkedOut = db.CheckedOuts.Find(id);
            if (checkedOut == null)
            {
                return HttpNotFound();
            }
            return View(checkedOut);
        }

        // POST: CheckedOuts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CheckedOut checkedOut = db.CheckedOuts.Find(id);
            db.CheckedOuts.Remove(checkedOut);
            db.SaveChanges();
            return RedirectToAction("Index");
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
