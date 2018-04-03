using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
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

        public ActionResult Filter()
        {
            var checkOutSupplies = db.CheckOutSupplies.Include(c => c.Department).Include(c => c.Students).Include(c => c.Supplies);
            return View(checkOutSupplies.ToList());
        }

        //TODO fix the match so that we can have searching but the contains statment will be more stengent 
        [HttpPost]
        public JsonResult AutoCompleteSupplies(string Prefix)
        {
            var Supplies = (from c in db.Supplies
                         where c.Name.ToLower().Contains(Prefix.ToLower()) | c.ClassRoom.ToLower().Contains(Prefix.ToLower())
                         select new { c.SuppliesID, c.Name, c.Number, c.Value, c.ClassRoom });

            return Json(Supplies, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AutoCompleteStudents(string Prefix)
        {
            var Students = (from c in db.Students
                            where c.StudentName.ToLower().Contains(Prefix.ToLower()) | c.UVUID.ToLower().Contains(Prefix.ToLower())
                            select new { c.StudentsID, c.StudentName, c.UVUID });

            return Json(Students, JsonRequestBehavior.AllowGet);
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
            // Defining Values to pass into ViewBag below selectListStudents and Supplies
            var students = db.Students.Where(s => s.StudentsID == s.StudentsID).ToList();
            IEnumerable<SelectListItem>
                selectListStudents = from s in students
                                     select new SelectListItem
                                     {
                                         Value = s.StudentsID.ToString(),
                                         Text = s.StudentName + ", UVUID: " + s.UVUID + "  "
                                     };

            var supplies = db.Supplies.Where(s => s.SuppliesID == s.SuppliesID).ToList();
            IEnumerable<SelectListItem>
                selectListSupplies = from s in supplies
                                     select new SelectListItem
                                     {
                                         Value = s.SuppliesID.ToString(),
                                         Text = s.Name + ", Value: " + s.Value + ", Number: " + s.Number + ", Class Room: " + s.ClassRoom + " "
                                     };



            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "DepName");
            ViewBag.StudentsID = new SelectList(selectListStudents, "Value", "Text");
            ViewBag.SuppliesID = new SelectList(selectListSupplies, "Value", "Text");
            CheckOutSupplies model = new CheckOutSupplies();
            model.CheckedOutDate = DateTime.Now;
            model.DueDate = DateTime.Now.AddDays(7);
            return View(model);
        }

        // POST: CheckOutSupplies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CheckOutSuppliesID,StudentsID,SuppliesID,DepartmentID,DueDate,ReturnedSupply,ReturnedDate,CheckedOutDate")] CheckOutSupplies checkOutSupplies)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.CheckOutSupplies.Add(checkOutSupplies);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch(DbEntityValidationException ex)
                {

                    return Content("check out request was invalid go back and try again" + ex);
                }

            }

            ViewBag.SuppliesID = new SelectList(db.Supplies, "SuppliesID", "Name", checkOutSupplies.SuppliesID);
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "DepName", checkOutSupplies.DepartmentID);
            ViewBag.StudentsID = new SelectList(db.Students, "StudentsID", "StudentName", checkOutSupplies.StudentsID);
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
            // Defining Values to pass into ViewBag below selectListStudents and Supplies
            var students = db.Students.Where(s => s.StudentsID == s.StudentsID).ToList();
            IEnumerable<SelectListItem>
                selectListStudents = from s in students
                                     select new SelectListItem
                                     {
                                         Value = s.StudentsID.ToString(),
                                         Text = s.StudentName + ", UVUID: " + s.UVUID + "  "
                                     };

            var supplies = db.Supplies.Where(s => s.SuppliesID == s.SuppliesID).ToList();
            IEnumerable<SelectListItem>
                selectListSupplies = from s in supplies
                                     select new SelectListItem
                                     {
                                         Value = s.SuppliesID.ToString(),
                                         Text = s.Name + ", Value: " + s.Value + ", Number: " + s.Number + ", Class Room: " + s.ClassRoom + " "
                                     };

            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "DepName", checkOutSupplies.DepartmentID);
            ViewBag.StudentsID = new SelectList(selectListStudents, "Value", "Text", checkOutSupplies.StudentsID);
            ViewBag.SuppliesID = new SelectList(selectListSupplies, "Value", "Text", checkOutSupplies.SuppliesID);
            return View(checkOutSupplies);
        }

        // POST: CheckOutSupplies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CheckOutSuppliesID,StudentsID,SuppliesID,DepartmentID,DueDate,ReturnedSupply,ReturnedDate,CheckedOutDate")] CheckOutSupplies checkOutSupplies)
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
