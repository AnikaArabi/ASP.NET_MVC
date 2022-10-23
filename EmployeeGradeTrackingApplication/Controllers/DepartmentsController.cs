using EmployeeGradeTrackingApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Threading;

namespace EmployeeGradeTrackingApplication.Controllers
{
    [Authorize]
    public class DepartmentsController : Controller
    {
        EmployeeDbContext db = new EmployeeDbContext();
        // GET: Department
        public ActionResult Index()
        {
            return View(db.Departments.Include(b => b.Employee).ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        public PartialViewResult CreateDepartment()
        {
            return PartialView("_CreateDepartment");
        }
        [HttpPost]
        public PartialViewResult CreateDepartment(Department b)
        {
            if (ModelState.IsValid)
            {
                db.Departments.Add(b);
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
        public PartialViewResult EditDepartment(int id)
        {
            var b = db.Departments.First(x => x.DepartmentId == id);
            return PartialView("_EditDepartment", b);
        }
        [HttpPost]
        public PartialViewResult EditDepartment(Department b)
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
            return View(db.Departments.First(x => x.DepartmentId == id));
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DoDelete(int DepartmentId)
        {
            var b = new Department { DepartmentId = DepartmentId };
            db.Entry(b).State = EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}