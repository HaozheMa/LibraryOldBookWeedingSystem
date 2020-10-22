using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LibrarySystem.Models;

namespace LibrarySystem.Controllers
{
    [Authorize]
    public class BooksController : Controller
    {
        private LibraryDBContainer db = new LibraryDBContainer();

        // GET: Books
        public ActionResult Index()
        {
            return View(db.Books.ToList());
        }

        // GET: Books/Details/5
        public ActionResult Details(long? id)
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
        public ActionResult Create([Bind(Include = "CallNum,Author,Name,ISBN,Duplicates,PublisherName,PublisherBaiJia,CirculationCount,PublishYear,OffTime")] Books books)
        {
            Calculatoion(books);

            if (ModelState.IsValid)
            {
                db.Books.Add(books);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(books);
        }

        // GET: Books/Edit/5
        public ActionResult Edit(long? id)
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

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CallNum,Author,Name,ISBN,Duplicates,PublisherName,PublisherBaiJia,CirculationCount,PublishYear,YearCount,OffTime,Id")] Books books)
        {
            Calculatoion(books);
            if (ModelState.IsValid)
            {
                db.Entry(books).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(books);
        }

        // GET: Books/Delete/5
        public ActionResult Delete(long? id)
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
        public ActionResult DeleteConfirmed(long id)
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

        public ActionResult GetData(JqueryDatatableParam param)
        {
            var books = db.Books.AsEnumerable();

            

            if (!string.IsNullOrEmpty(param.sSearch))
            {
                books = books.Where(x => x.Name.ToLower().Contains(param.sSearch.ToLower())
                                              || x.ISBN.ToLower().Contains(param.sSearch.ToLower())
                                              || x.CallNum.ToLower().Contains(param.sSearch.ToLower())
                                              || x.Author.ToLower().Contains(param.sSearch.ToLower())).ToList();
            }

            var sortColumnIndex = Convert.ToInt32(HttpContext.Request.QueryString["iSortCol_0"]);
            var sortDirection = HttpContext.Request.QueryString["sSortDir_0"];

            if (sortColumnIndex == 3)
            {
                books = sortDirection == "asc" ? books.OrderBy(c => c.ISBN) : books.OrderByDescending(c => c.ISBN);
            }
            else if (sortColumnIndex == 4)
            {
                books = sortDirection == "asc" ? books.OrderBy(c => c.Duplicates) : books.OrderByDescending(c => c.Duplicates);
            }
            else if (sortColumnIndex == 5)
            {
                books = sortDirection == "asc" ? books.OrderBy(c => c.CirculationCount) : books.OrderByDescending(c => c.CirculationCount);
            }
            else if (sortColumnIndex == 6)
            {
                books = sortDirection == "asc" ? books.OrderBy(c => c.PublisherName) : books.OrderByDescending(c => c.PublisherName);
            }
            else if (sortColumnIndex == 7)
            {
                books = sortDirection == "asc" ? books.OrderBy(c => c.PublishYear) : books.OrderByDescending(c => c.PublishYear);
            }
            else if (sortColumnIndex == 8)
            {
                books = sortDirection == "asc" ? books.OrderBy(c => c.OffTime) : books.OrderByDescending(c => c.OffTime);
            }
            else if (sortColumnIndex == 9)
            {
                books = sortDirection == "asc" ? books.OrderBy(c => c.BWI) : books.OrderByDescending(c => c.BWI);
            }
            else
            {
                Func<Books, string> orderingFunction = e => sortColumnIndex == 0 ? e.Name :
                                                               sortColumnIndex == 1 ? e.Author :
                                                               e.CallNum;

                books = sortDirection == "asc" ? books.OrderBy(orderingFunction) : books.OrderByDescending(orderingFunction);
            }

            var displayResult = books.Skip(param.iDisplayStart)
                .Take(param.iDisplayLength).ToList();
            var totalRecords = books.Count();

            return Json(new
            {
                param.sEcho,
                iTotalRecords = totalRecords,
                iTotalDisplayRecords = totalRecords,
                aaData = displayResult
            }, JsonRequestBehavior.AllowGet);

        }

        public void Calculatoion(Books books)
        {
            int year = books.PublishYear ?? 0;
            if (year != 0)
            {
                books.YearCount = 2018 - year;
            }


            double count = books.OffTime / 365;
            books.OffTimeCount = Convert.ToInt32(Math.Ceiling(count)) + 1;

            books.CirculationFrequency = Convert.ToDouble(Math.Round(((decimal)1 / books.CirculationCount), 2));

            Weights weights = db.Weights.Find(1);
            if (books.YearCount != null)
            {
                books.BWI = weights.Wyear * books.YearCount
                    + weights.WBaijia * Convert.ToInt32(books.PublisherBaiJia)
                    + weights.Wduplication * books.Duplicates
                    + weights.WOffTime * books.OffTimeCount
                    + weights.WcirculationFequency * books.CirculationFrequency;
            }
        }
    }
}
