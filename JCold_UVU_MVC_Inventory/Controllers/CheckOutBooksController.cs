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
            ViewBag.BooksID = new SelectList(db.Books, "BooksID", "Title");
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "DepName");
            ViewBag.StudentsID = new SelectList(db.Students, "StudentsID", "StudentName");
            CheckOutBook model = new CheckOutBook();
            model.CheckedOutDate = DateTime.Now;
            return View(model);
        }

        // POST: CheckOutBooks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CheckOutBookID,StudentsID,BooksID,DepartmentID,ReturnedBook,ReturnedDate,CheckedOutDate")] CheckOutBook checkOutBook)
        {
            //var UpdateQuery =
            //    from chkb in db.CheckOutBooks
            //    join bk in db.Books
            //    on chkb.BooksID equals bk.BooksID
            //    where chkb.BooksID == bk.BooksID
            //    select bk;

            //foreach(Books chkb in UpdateQuery)
            //{
            //    chkb.Available = false;
            //}


            if (ModelState.IsValid)
            {
                db.CheckOutBooks.Add(checkOutBook);
                db.SaveChanges();
                return RedirectToAction("Index");
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
            ViewBag.BooksID = new SelectList(db.Books, "BooksID", "Title", checkOutBook.BooksID);
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "DepName", checkOutBook.DepartmentID);
            ViewBag.StudentsID = new SelectList(db.Students, "StudentsID", "StudentName", checkOutBook.StudentsID);
            return View(checkOutBook);
        }

        // POST: CheckOutBooks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CheckOutBookID,StudentsID,BooksID,DepartmentID,ReturnedBook,ReturnedDate,CheckedOutDate")] CheckOutBook checkOutBook)
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
