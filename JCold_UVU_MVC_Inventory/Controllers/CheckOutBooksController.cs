using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JCold_UVU_MVC_Inventory.Models;

namespace JCold_UVU_MVC_Inventory.Controllers
{
    public class CheckOutBooksController : Controller
    {
        private JCold_UVU_MVC_InventoryDb db = new JCold_UVU_MVC_InventoryDb();


        // GET: CheckOutBooks
        public ActionResult Index()
        {
            

            var checkOutBooks = db.CheckOutBooks.Include(c => c.Books).Include(c => c.Department).Include(c => c.Students);
            return View(checkOutBooks.ToList());
        }

        public ActionResult Search(string checkoutbookstring, bool checkBookResp = true)
        {
                List<CheckOutBook> checkedoutbooklist = db.CheckOutBooks.Where(x => x.ReturnedBook.Equals(checkBookResp)).ToList();
                return View(checkedoutbooklist);
        }

        public ActionResult Filter()
        {
            var checkOutBooks = db.CheckOutBooks.Include(c => c.Books).Include(c => c.Department).Include(c => c.Students);
            return View(checkOutBooks.ToList());
        }

        [HttpPost]
        public JsonResult AutoCompleteBooks(string Prefix)
        {
            var Books = (from c in db.Books
                         where c.Title.ToLower().Contains(Prefix.ToLower())
                         select new {c.BooksID, c.Title, c.ISBN, c.Number, c.ClassRoom });

            return Json(Books, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AutoCompleteStudents(string Prefix)
        {
            var Students = (from c in db.Students
                         where c.StudentName.ToLower().Contains(Prefix.ToLower())
                         select new { c.StudentsID, c.StudentName, c.UVUID});

            return Json(Students, JsonRequestBehavior.AllowGet);
        }

        // GET: CheckOutBooks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CheckOutBook checkOutBook = db.CheckOutBooks.Find(id);
            if (checkOutBook == null)
            {
                return HttpNotFound();
            }
            return View(checkOutBook);
        }

        // GET: CheckOutBooks/Create
        public ActionResult Create()
        {
            // Defining Values to pass into ViewBag below selectListStudents and Books
            var students = db.Students.Where(s => s.StudentsID == s.StudentsID).ToList();
            IEnumerable<SelectListItem>
                selectListStudents = from s in students
                                     select new SelectListItem
                                     {
                                         Value = s.StudentsID.ToString(),
                                         Text = s.StudentName + ", UVUID: " + s.UVUID + "  "
                                     };

            var books = db.Books.Where(s => s.BooksID == s.BooksID).ToList();
            IEnumerable<SelectListItem>
                selectListBooks = from s in books
                                     select new SelectListItem
                                     {
                                         Value = s.BooksID.ToString(),
                                         Text = s.Title + ", ISBN: " + s.ISBN + ", Number: " + s.Number + ", Class Room: " + s.ClassRoom + " "
                                     };

            ViewBag.BooksID = new SelectList(selectListBooks, "Value", "Text");
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "DepName");
            ViewBag.StudentsID = new SelectList(selectListStudents, "Value", "Text");

            CheckOutBook model = new CheckOutBook();
            model.CheckedOutDate = DateTime.Now;
            model.DueDate = DateTime.Now.AddDays(7);
            return View(model);
        }

        // POST: CheckOutBooks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CheckOutBookID,StudentsID,BooksID,DepartmentID,DueDate,ReturnedBook,ReturnedDate,CheckedOutDate")] CheckOutBook checkOutBook)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    db.CheckOutBooks.Add(checkOutBook);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    return Content("check out request was invalid go back and try again");
                }
                
            }

            ViewBag.BooksID = new SelectList(db.Books, "BooksID", "Title", checkOutBook.BooksID);
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "DepName", checkOutBook.DepartmentID);
            ViewBag.StudentsID = new SelectList(db.Students, "StudentsID", "StudentName", checkOutBook.StudentsID);
            return View(checkOutBook);

        }

        // GET: CheckOutBooks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CheckOutBook checkOutBook = db.CheckOutBooks.Find(id);
            if (checkOutBook == null)
            {
                return HttpNotFound();
            }
            // Defining Values to pass into ViewBag below selectListStudents and Books
            var students = db.Students.Where(s => s.StudentsID == s.StudentsID).ToList();
            IEnumerable<SelectListItem>
                selectListStudents = from s in students
                                     select new SelectListItem
                                     {
                                         Value = s.StudentsID.ToString(),
                                         Text = s.StudentName + ", UVUID: " + s.UVUID + "  "
                                     };

            var books = db.Books.Where(s => s.BooksID == s.BooksID).ToList();
            IEnumerable<SelectListItem>
                selectListBooks = from s in books
                                  select new SelectListItem
                                  {
                                      Value = s.BooksID.ToString(),
                                      Text = s.Title + ", ISBN: " + s.ISBN + ", Number: " + s.Number + ", Class Room: " + s.ClassRoom + " "
                                  };
            ViewBag.BooksID = new SelectList(selectListBooks, "Value", "Text", checkOutBook.BooksID);
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "DepName", checkOutBook.DepartmentID);
            ViewBag.StudentsID = new SelectList(selectListStudents, "Value", "Text", checkOutBook.StudentsID);
            return View(checkOutBook);
        }

        // POST: CheckOutBooks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CheckOutBookID,StudentsID,BooksID,DepartmentID,DueDate,ReturnedBook,ReturnedDate,CheckedOutDate")] CheckOutBook checkOutBook)
        {
            if (checkOutBook.ReturnedBook)
            {
                DateTime CurrentDate = DateTime.Now;
                checkOutBook.ReturnedDate = CurrentDate;
                db.Entry(checkOutBook).State = EntityState.Modified;
            }
            if (ModelState.IsValid)
            {
                db.Entry(checkOutBook).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BooksID = new SelectList(db.Books, "BooksID", "Title", checkOutBook.BooksID);
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "DepName", checkOutBook.DepartmentID);
            ViewBag.StudentsID = new SelectList(db.Students, "StudentsID", "StudentName", checkOutBook.StudentsID);
            return View(checkOutBook);
        }

        // GET: CheckOutBooks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CheckOutBook checkOutBook = db.CheckOutBooks.Find(id);
            if (checkOutBook == null)
            {
                return HttpNotFound();
            }
            return View(checkOutBook);
        }

        // POST: CheckOutBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CheckOutBook checkOutBook = db.CheckOutBooks.Find(id);
            db.CheckOutBooks.Remove(checkOutBook);
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
