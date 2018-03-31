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
    public class SuppliesController : Controller
    {
        private JCold_UVU_MVC_InventoryDb db = new JCold_UVU_MVC_InventoryDb();

        // GET: Supplies
        public ActionResult Index()
        {
            var UpdateQuery =
                from chks in db.CheckOutSupplies
                join sp in db.Supplies
                on chks.SuppliesID equals sp.SuppliesID
                where chks.SuppliesID == sp.SuppliesID && chks.ReturnedSupply == false
                select sp;

            foreach (Supplies chks in UpdateQuery)
            {
                chks.Available = false;
            }
            return View(db.Supplies.ToList());
        }

        public ActionResult Search(string supplyName)
        {
            List<Supplies> supplyList = db.Supplies.Where(x => x.Name.Contains(supplyName) | x.ClassRoom.Contains(supplyName)).ToList();
            return View(supplyList);
        }
        public ActionResult Filter()
        {
            var Supplies = db.Supplies;
            return View(Supplies.ToList());
        }

        // GET: Supplies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplies supplies = db.Supplies.Find(id);
            if (supplies == null)
            {
                return HttpNotFound();
            }
            return View(supplies);
        }

        // GET: Supplies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Supplies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SuppliesID,Name,Value,Number,Available,ClassRoom")] Supplies supplies)
        {
            if (ModelState.IsValid)
            {
                db.Supplies.Add(supplies);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(supplies);
        }

        // GET: Supplies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplies supplies = db.Supplies.Find(id);
            if (supplies == null)
            {
                return HttpNotFound();
            }
            return View(supplies);
        }

        // POST: Supplies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SuppliesID,Name,Value,Number,Available,ClassRoom")] Supplies supplies)
        {
            if (ModelState.IsValid)
            {
                db.Entry(supplies).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(supplies);
        }

        // GET: Supplies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplies supplies = db.Supplies.Find(id);
            if (supplies == null)
            {
                return HttpNotFound();
            }
            return View(supplies);
        }

        // POST: Supplies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Supplies supplies = db.Supplies.Find(id);
            db.Supplies.Remove(supplies);
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
