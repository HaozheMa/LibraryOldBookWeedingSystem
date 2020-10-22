using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LibrarySystem.Models;

namespace LibrarySystem.Controllers
{
    [Authorize]
    public class WeightsController : Controller
    {
        private LibraryDBContainer db = new LibraryDBContainer();

        // GET: Weights
        public ActionResult Index()
        {
            return View(db.Weights.ToList());
        }

        // GET: Weights/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Weights weights = db.Weights.Find(id);
            if (weights == null)
            {
                return HttpNotFound();
            }
            return View(weights);
        }

        [HttpPost, ActionName("Details")]
        [ValidateAntiForgeryToken]
        public ActionResult Confirm(Weights weights)
        {
            UpdateBook(weights.Id);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Weights/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Weights/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Wyear,WBaijia,Wduplication,WOffTime,WcirculationFequency")] Weights weights)
        {
            if (ModelState.IsValid)
            {
                db.Weights.Add(weights);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(weights);
        }

        // GET: Weights/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Weights weights = db.Weights.Find(id);
            if (weights == null)
            {
                return HttpNotFound();
            }
            return View(weights);
        }

        // POST: Weights/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Wyear,WBaijia,Wduplication,WOffTime,WcirculationFequency")] Weights weights)
        {
            if (ModelState.IsValid)
            {
                db.Entry(weights).State = EntityState.Modified;
                
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(weights);
        }

        // GET: Weights/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Weights weights = db.Weights.Find(id);
            if (weights == null)
            {
                return HttpNotFound();
            }
            return View(weights);
        }

        // POST: Weights/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Weights weights = db.Weights.Find(id);
            db.Weights.Remove(weights);
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

        public void UpdateBook(int? id)
        {
            Weights weights = db.Weights.Find(id);
            Books[] list = db.Books.ToArray();
           
            foreach (Books item in list)
            {
                if(item.YearCount != null)
                {
                    item.BWI = weights.Wyear * item.YearCount
                    + weights.WBaijia * Convert.ToInt32(item.PublisherBaiJia)
                    + weights.Wduplication * item.Duplicates
                    + weights.WOffTime * item.OffTimeCount
                    + weights.WcirculationFequency * item.CirculationFrequency;
                }
            }
            db.Books.AddOrUpdate(list);
        }

        [HttpPost]
        public JsonResult AjaxMethod(int id)
        {
            UpdateBook(id);
            db.SaveChanges();
            Weights weights = db.Weights.Find(id);
            return Json(weights);
        }
    }
}
