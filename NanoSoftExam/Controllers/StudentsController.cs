using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CaptchaMvc.HtmlHelpers;
using NanoSoftExam.Data;
using NanoSoftExam.Models;

namespace NanoSoftExam.Controllers
{
    public class StudentsController : Controller
    {
        private StudentContext db = new StudentContext();

        // GET: Students
        public ActionResult Index()
        {
            var students = db.Students.Include(s => s.Class);
            return View(students.ToList());
        }

        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            ViewBag.ClassName = new SelectList(db.Classes.OrderBy(x=>x.Id), "ClassName", "ClassName");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentId,LastName,FirstName,FatherName,Address,ClassName")] Student student, HttpPostedFileBase image1)
        {
            if (ModelState.IsValid)
            {
                if (!this.IsCaptchaValid("Captcha is not valid"))
                {
                    ViewBag.ErrorMessage = "Captcha is not valid";
                    ViewBag.ClassName = new SelectList(db.Classes.OrderBy(x => x.Id), "ClassName", "ClassName");
                    return View("Create");
                }
                student.Image = new byte[image1.ContentLength];
                image1.InputStream.Read(student.Image, 0, image1.ContentLength);
                db.Students.Add(student);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
           
            ViewBag.ClassName = new SelectList(db.Classes, "ClassName", "ClassName", student.ClassName);
            return View(student);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClassName = new SelectList(db.Classes, "ClassName", "ClassName", student.ClassName);
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentId,LastName,FirstName,FatherName,Address,ClassName")] Student student, HttpPostedFileBase image1)
        {
            if (ModelState.IsValid)
            {
                student.Image = new byte[image1.ContentLength];
                image1.InputStream.Read(student.Image, 0, image1.ContentLength);
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClassName = new SelectList(db.Classes, "ClassName", "ClassName", student.ClassName);
            return View(student);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
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
