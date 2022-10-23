using EmployeeGradeTrackingApplication.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace EmployeeGradeTrackingApplication.Controllers
{
    [Authorize]
    public class GradesController : Controller
    {
        
        // GET: Grades
        EmployeeDbContext db = new EmployeeDbContext();
        public ActionResult Index()
        {
            return View(db.Grades);
        }
        public ActionResult Create()
        {
            return View();
        }
        public PartialViewResult CreateGrade()
        {
            return PartialView("_CreateGrade");
        }
        [HttpPost]
        public PartialViewResult CreateGrade(Grade b)
        {
            Thread.Sleep(2000);
            if (ModelState.IsValid)
            {
                db.Grades.Add(b);
                db.SaveChanges();
                return PartialView("_Success");
            }
            return PartialView("_Fail");
        }
        public ActionResult Edit(int id)
        {
            ViewBag.Id = id;
            return View();
        }
        public PartialViewResult EditGrade(int id)
        {
            var b = db.Grades.First(x => x.GradeId == id);
            return PartialView("_EditGrade", b);
        }
        [HttpPost]
        public PartialViewResult EditGrade(Grade b)
        {
            Thread.Sleep(4000);
            if (ModelState.IsValid)
            {
                db.Entry(b).State = EntityState.Modified;
                db.SaveChanges();
                return PartialView("_Success");
            }
            return PartialView("_Fail");
        }
        public ActionResult Delete(int id)
        {
            return View(db.Grades.First(x => x.GradeId == id));
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DoDelete(int GradeId)
        {
            var b = new Grade { GradeId = GradeId };
            db.Entry(b).State = EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}