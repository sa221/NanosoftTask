using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NanoSoftExam.Data;
using NanoSoftExam.Models;

namespace NanoSoftExam.Controllers
{
    public class AttendencesController : Controller
    {
        private StudentContext db = new StudentContext();

        // GET: Attendences
        public ActionResult Index()
        {
            
            var attendences = db.Attendences.Include(a => a.Student);
            return View(attendences.ToList());
        }
        public ActionResult Create()
        {
            DateTime date;
            if (Request["AttendanceDate"] != null)
            {
                //date = Convert.ToDateTime(Request["AttendanceDate"]);
                date = DateTime.ParseExact(Request["AttendanceDate"], "yyyy-MM-dd", null);
            }
            else
            {
                date = DateTime.Now;
            }
            var st1 = db.Attendences.FirstOrDefault(x => DbFunctions.TruncateTime(x.Date) == DbFunctions.TruncateTime(date));
            List<StudentList> StudentInformation;
            if (st1 != null)
            {
                StudentInformation = (from x in db.Students
                    join y in db.Attendences on x.StudentId equals y.StudentId into ps
                    from y in ps.DefaultIfEmpty()
                                      where DbFunctions.TruncateTime(y.Date) == DbFunctions.TruncateTime(date)
                    select new StudentList
                    {
                        StudentId = x.StudentId,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        Status = y.Status
                    }).ToList();
            }

            else
            {
                StudentInformation = (from x in db.Students
                    select new StudentList
                    {
                        StudentId = x.StudentId,
                        FirstName = x.FirstName,
                        LastName = x.LastName
                        //Status = y.Status
                    }).ToList();
            }
            ViewBag.StudentList = StudentInformation;
            //var StudentInformation = db.Attendences.ToList();
            return View(StudentInformation);
        }

        
        [HttpPost]
        public ActionResult Attendance(List<Attendence> attendences)
        {
            foreach (Attendence att in attendences)
            {
                if (att.StudentId != 0)
                {
                    var check = db.Attendences.FirstOrDefault(v => v.Date == att.Date && v.StudentId == att.StudentId);
                    if (check != null)
                    {
                        check.Status = att.Status;
                        //var entry = db.Entry(check);
                        //if (entry.State == EntityState.Detached || entry.State == EntityState.Modified)
                        //{
                        //    entry.State = EntityState.Modified; //do it here

                        //    db.Set<Attendence>().Attach(check); //attach
                        //    db.SaveChanges();
                        //}
                        db.Entry(check).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    else
                    {
                        db.Attendences.Add(att);
                    }
                }
               
            }

            int insertedRecords = db.SaveChanges();
            return Json(insertedRecords);
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
