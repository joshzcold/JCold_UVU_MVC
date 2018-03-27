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
    public class StudentsController : Controller
    {
        private JCold_UVU_MVC_InventoryDb db = new JCold_UVU_MVC_InventoryDb();

        // GET: Students
        public ActionResult Index()
        {

            // TODO Add Query that will update HasCheckedBook to false when a book is returned or deleted. 
            var UpdateQueryBooks =
               from chks in db.CheckOutBooks
               join st in db.Students
               on chks.StudentsID equals st.StudentsID
               where chks.StudentsID == st.StudentsID && chks.ReturnedBook == false
               select st;

            var UpdateQuerySupplies =
               from chks in db.CheckOutSupplies
               join st in db.Students
               on chks.StudentsID equals st.StudentsID
               where chks.StudentsID == st.StudentsID && chks.ReturnedSupply == false
               select st;

            foreach (Students chks in UpdateQuerySupplies)
            {
                chks.HasCheckedOutSupplies = true;
            }

            foreach (Students chks in UpdateQueryBooks)
            {
                chks.HasCheckedOutBooks = true;
            }

            db.SaveChanges();
            return View(db.Students.ToList());
        }

        public ActionResult Search(string studentName)
        {
            List<Students> studentList = db.Students.Where(x => x.StudentName.Contains(studentName) | x.UVUID.Contains(studentName)).ToList();
            return View(studentList);
        }
        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Students students = db.Students.Find(id);
            if (students == null)
            {
                return HttpNotFound();
            }

            var CheckedBooksSelectQuery =
                from chks in db.CheckOutBooks
                 join st in db.Students
                 on chks.StudentsID equals st.StudentsID join bk in db.Books
                 on chks.BooksID equals bk.BooksID
                 where chks.StudentsID == st.StudentsID && chks.ReturnedBook == false
                 select new { bk.Title,bk.ISBN, bk.Number, bk.ClassRoom, chks.CheckedOutDate, chks.DueDate, chks.ReturnedDate };

            var CheckedSuppliesSelectQuery =
                from chks in db.CheckOutSupplies
                 join st in db.Students
                 on chks.StudentsID equals st.StudentsID join bk in db.Supplies
                 on chks.SuppliesID equals bk.SuppliesID
                 where chks.StudentsID == st.StudentsID && chks.ReturnedSupply == false
                 select new { bk.Name,bk.Number, bk.Value, bk.ClassRoom, chks.CheckedOutDate, chks.DueDate, chks.ReturnedDate };

            ViewBag.CheckSuppliesQuery = CheckedSuppliesSelectQuery;
            ViewBag.CheckBookQuery = CheckedBooksSelectQuery;
            return View(students);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentsID,UVUID,StudentName,StudentEmail,StudentPhone,HasCheckedOutBooks,HasCheckedOutSupplies")] Students students)
        {
            if (ModelState.IsValid)
            {
                db.Students.Add(students);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(students);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Students students = db.Students.Find(id);
            if (students == null)
            {
                return HttpNotFound();
            }
            return View(students);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentsID,UVUID,StudentName,StudentEmail,StudentPhone,HasCheckedOutBooks,HasCheckedOutSupplies")] Students students)
        {
            if (ModelState.IsValid)
            {
                db.Entry(students).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(students);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Students students = db.Students.Find(id);
            if (students == null)
            {
                return HttpNotFound();
            }
            return View(students);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Students students = db.Students.Find(id);
            db.Students.Remove(students);
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
