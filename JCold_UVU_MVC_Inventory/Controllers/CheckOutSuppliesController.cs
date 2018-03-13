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
    public class CheckOutSuppliesController : Controller
    {
        private JCold_UVU_MVC_InventoryDb db = new JCold_UVU_MVC_InventoryDb();

        // GET: CheckOutSupplies
        public ActionResult Index()
        {
            var checkOutSupplies = db.CheckOutSupplies.Include(c => c.Department).Include(c => c.Students).Include(c => c.Supplies);
            return View(checkOutSupplies.ToList());
        }

        // GET: CheckOutSupplies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CheckOutSupplies checkOutSupplies = db.CheckOutSupplies.Find(id);
            if (checkOutSupplies == null)
            {
                return HttpNotFound();
            }
            return View(checkOutSupplies);
        }

        // GET: CheckOutSupplies/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "DepName");
            ViewBag.StudentsID = new SelectList(db.Students, "StudentsID", "StudentName");
            ViewBag.SuppliesID = new SelectList(db.Supplies, "SuppliesID", "Name");
            CheckOutSupplies model = new CheckOutSupplies();
            model.CheckedOutDate = DateTime.Now;
            return View(model);
        }

        // POST: CheckOutSupplies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CheckOutSuppliesID,StudentsID,SuppliesID,DepartmentID,ReturnedSupply,ReturnedDate,CheckedOutDate")] CheckOutSupplies checkOutSupplies)
        {
            if (ModelState.IsValid)
            {
                db.CheckOutSupplies.Add(checkOutSupplies);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "DepName", checkOutSupplies.DepartmentID);
            ViewBag.StudentsID = new SelectList(db.Students, "StudentsID", "StudentName", checkOutSupplies.StudentsID);
            ViewBag.SuppliesID = new SelectList(db.Supplies, "SuppliesID", "Name", checkOutSupplies.SuppliesID);
            return View(checkOutSupplies);
        }

        // GET: CheckOutSupplies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CheckOutSupplies checkOutSupplies = db.CheckOutSupplies.Find(id);
            if (checkOutSupplies == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "DepName", checkOutSupplies.DepartmentID);
            ViewBag.StudentsID = new SelectList(db.Students, "StudentsID", "StudentName", checkOutSupplies.StudentsID);
            ViewBag.SuppliesID = new SelectList(db.Supplies, "SuppliesID", "Name", checkOutSupplies.SuppliesID);
            return View(checkOutSupplies);
        }

        // POST: CheckOutSupplies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CheckOutSuppliesID,StudentsID,SuppliesID,DepartmentID,ReturnedSupply,ReturnedDate,CheckedOutDate")] CheckOutSupplies checkOutSupplies)
        {
            if (checkOutSupplies.ReturnedSupply)
            {
                DateTime CurrentDate = DateTime.Now;
                checkOutSupplies.ReturnedDate = CurrentDate;
                db.Entry(checkOutSupplies).State = EntityState.Modified;
            }
            if (ModelState.IsValid)
            {
                db.Entry(checkOutSupplies).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "DepName", checkOutSupplies.DepartmentID);
            ViewBag.StudentsID = new SelectList(db.Students, "StudentsID", "StudentName", checkOutSupplies.StudentsID);
            ViewBag.SuppliesID = new SelectList(db.Supplies, "SuppliesID", "Name", checkOutSupplies.SuppliesID);
            return View(checkOutSupplies);
        }

        // GET: CheckOutSupplies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CheckOutSupplies checkOutSupplies = db.CheckOutSupplies.Find(id);
            if (checkOutSupplies == null)
            {
                return HttpNotFound();
            }
            return View(checkOutSupplies);
        }

        // POST: CheckOutSupplies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CheckOutSupplies checkOutSupplies = db.CheckOutSupplies.Find(id);
            db.CheckOutSupplies.Remove(checkOutSupplies);
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
