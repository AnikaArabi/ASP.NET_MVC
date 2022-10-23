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
    public class DesignationsController : Controller
    {
        EmployeeDbContext db = new EmployeeDbContext();
        // GET: Designations
        public ActionResult Index()
        {
            return View(db.Designations);
        }
        public ActionResult Create()
        {
            return View();
        }
        public PartialViewResult CreateDesignation()
        {
            return PartialView("_CreateDesignation");
        }
        [HttpPost]
        public PartialViewResult CreateDesignation(Designation b)
        {
            if (ModelState.IsValid)
            {
                db.Designations.Add(b);
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
        public PartialViewResult EditDesignation(int id)
        {
            var b = db.Designations.First(x => x.DesignationId == id);
            return PartialView("_EditDesignation", b);
        }
        [HttpPost]
        public PartialViewResult EditDesignation(Designation b)
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
            return View(db.Designations.First(x => x.DesignationId == id));
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DoDelete(int DesignationId)
        {
            var b = new Designation { DesignationId = DesignationId };
            db.Entry(b).State = EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}