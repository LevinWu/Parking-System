 using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FIT5032.Models;
using Microsoft.AspNet.Identity;

namespace FIT5032.Controllers
{
    public class BookingsController : Controller
    {
        private readonly ModelFinal db = new ModelFinal();

        // GET: Bookings
        public ActionResult Index()
        {
            var UserId1 = User.Identity.GetUserId();
            var bookings = db.Bookings.Where ( b => b.User_Id == UserId1);
            return View(bookings.ToList());
        }

        //Get:Space List
        public ActionResult Select()
        {
            return View(db.Spaces.ToList());
        }

        // GET: Bookings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // GET: Bookings/Create
        public ActionResult Create(int id) {

            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            var UserId = User.Identity.GetUserId();
            ViewBag.User_Id = UserId;
            ViewBag.Space_Id = id;
            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "User_Id,Space_Id,Entry_date,Exit_date,Description")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Bookings.Add(booking);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Space_Id = new SelectList(db.Spaces, "Space_Id", "Location", booking.Space_Id);
            return View(booking);
        }

        // GET: Bookings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            ViewBag.Space_Id = new SelectList(db.Spaces, "Space_Id", "Location", booking.Space_Id);
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Booking_Id,User_Id,Space_Id,Entry_date,Exit_date,Description")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(booking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Space_Id = new SelectList(db.Spaces, "Space_Id", "Location", booking.Space_Id);
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Booking booking = db.Bookings.Find(id);
            db.Bookings.Remove(booking);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Bookings/Rating/5
        public ActionResult Rating(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            Booking booking = db.Bookings.Find(id);
            if (booking.Rating == null)
            {
                ViewBag.Rating = 0;
            }
            else
            {
                ViewBag.Rating = booking.Rating;
            }

            return View(booking);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Rating(Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(booking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Space_Id = new SelectList(db.Spaces, "Space_Id", "Location", booking.Space_Id);
            return View(booking);
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
