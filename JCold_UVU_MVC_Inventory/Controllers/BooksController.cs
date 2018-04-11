using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JCold_UVU_MVC_Inventory.Models;

namespace JCold_UVU_MVC_Inventory.Controllers
{
    public class BooksController : Controller
    {
        private JCold_UVU_MVC_InventoryDb db = new JCold_UVU_MVC_InventoryDb();

        // GET: Books
        //TODO make delete unavailable if a book is featured in checked out

        public ActionResult Index()
        {

            var UpdateQuery =
                from chkb in db.CheckOutBooks
                join bk in db.Books
                on chkb.BooksID equals bk.BooksID
                where chkb.BooksID == bk.BooksID && chkb.ReturnedBook == false
                select bk;
            foreach (Books chkb in UpdateQuery)
            {
                chkb.Available = false;
            }

            bool checkforCheckedBooks = db.CheckOutBooks.Any(p => p.ReturnedBook).Equals(false);

            var UpdateQueryOpposite =
            from chkb in db.CheckOutBooks
            join bk in db.Books
            on chkb.BooksID equals bk.BooksID
            where chkb.BooksID == bk.BooksID && checkforCheckedBooks == true
            select bk;
            foreach (Books chkb in UpdateQuery)
            {
                chkb.Available = true;
            }

            var ResetUpdateQueryBooksOnDelete =
                from st in db.Books
                where !(from ch in db.CheckOutBooks select ch.BooksID).Contains(st.BooksID)
                select st;
            foreach (Books chkb in ResetUpdateQueryBooksOnDelete)
            {
                chkb.Available = true;
            }
            db.SaveChanges();
            return View(db.Books.ToList());
        }

        public ActionResult Search(string bookTitle)
        {
            List<Books> bookList = db.Books.Where(x => x.Title.Contains(bookTitle) | x.ISBN.Contains(bookTitle) | x.ClassRoom.Contains(bookTitle)).ToList();
            return View(bookList);
        }

        public ActionResult Filter()
        {
            var Books = db.Books;
            return View(Books.ToList());
        }

        // GET: Books/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Books books = db.Books.Include(x => x.Files).SingleOrDefault(x => x.BooksID == id);
            if (books == null)
            {
                return HttpNotFound();
            }
            return View(books);
        }
        [Authorize(Roles = "canEdit")]
        // GET: Books/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BooksID,Title,ISBN,Author,Publisher,Number,Available,ClassRoom")] Books books, HttpPostedFileBase upload)
        {
            // function to add photo to book id
            try
            {
                if (ModelState.IsValid)
                {
                    if (upload != null && upload.ContentLength > 0)
                    {
                        var cover = new File
                        {
                            FileName = System.IO.Path.GetFileName(upload.FileName),
                            FileType = FileType.Photo,
                            ContentType = upload.ContentType
                        };

                        using (var reader = new System.IO.BinaryReader(upload.InputStream))
                        {
                            cover.Content = reader.ReadBytes(upload.ContentLength);
                        }
                        books.Files = new List<File> { cover };
                    }

                    db.Books.Add(books);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Unable to save changes. try again.");
            }

            return View(books);
        }
        [Authorize(Roles = "canEdit")]
        // GET: Books/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Books books = db.Books.Include(x => x.Files).SingleOrDefault(x => x.BooksID == id);
            if (books == null)
            {
                return HttpNotFound();
            }
            return View(books);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, [Bind(Include = "BooksID,Title,ISBN,Author,Publisher,Number,Available,ClassRoom")] Books books, HttpPostedFileBase upload)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // Very long function to detect if a photo exists on this book id. Delete it then replace it will the supplied photo.
            var updateBook = db.Books.Find(books.BooksID);

            if (TryUpdateModel(updateBook, "",
        new string[] { "BooksID", "Title", "ISBN", "Author", "Publisher", "Number", "Available", "ClassRoom" }))
            {

            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (upload != null && upload.ContentLength > 0)
                    {
                        if (updateBook.Files.Any(x => x.FileType == FileType.Photo))
                        {
                            db.Files.Remove(updateBook.Files.First(x => x.FileType == FileType.Photo));

                        }

                        var photo = new File
                        {
                            FileName = System.IO.Path.GetFileName(upload.FileName),
                            FileType = FileType.Photo,
                            ContentType = upload.ContentType
                        };
                        using (var reader = new System.IO.BinaryReader(upload.InputStream))
                        {
                            photo.Content = reader.ReadBytes(upload.ContentLength);
                        }

                        updateBook.Files = new List<File> { photo };


                        db.Entry(updateBook).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
                catch (RetryLimitExceededException)
                {
                    ModelState.AddModelError("", "Unable to add a profile image.");
                }

            }
            db.Entry(updateBook).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "canEdit")]
        // GET: Books/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Books books = db.Books.Find(id);
            if (books == null)
            {
                return HttpNotFound();
            }
            return View(books);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Books books = db.Books.Find(id);
            db.Books.Remove(books);
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
