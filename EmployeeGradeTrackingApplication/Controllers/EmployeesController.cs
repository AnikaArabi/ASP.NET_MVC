using EmployeeGradeTrackingApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using EmployeeGradeTrackingApplication.ViewModels;
using System.IO;
using System.Threading;

namespace EmployeeGradeTrackingApplication.Controllers
{
    [Authorize]
    public class EmployeesController : Controller
    {
        EmployeeDbContext db = new EmployeeDbContext();
        // GET: Employees
        public ActionResult Index()
        {
            return View(db.Employees.Include(x => x.Department).ToList());
        }
        public ActionResult Create()
        {
            ViewBag.Departments = db.Departments.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Create(EmployeeInputModel t)
        {
            if (ModelState.IsValid)
            {
                var employee = new Employee
                {
                    EmployeeName = t.EmployeeName,
                    JoiningDate = t.JoiningDate,
                    Email = t.Email,
                    Phone = t.Phone,
                    IsWorking = t.IsWorking,
                    DepartmentId = t.DepartmentId
                    
                };
                string ext = Path.GetExtension(t.Picture.FileName);
                string f = Guid.NewGuid() + ext;
                t.Picture.SaveAs(Server.MapPath("~/Uploads/") + f);
                employee.Picture = f;
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Departments = db.Departments.ToList();
            return View(t);
        }
        public ActionResult Edit(int id)
        {
            var t = db.Employees.First(x => x.EmployeeId == id);
            ViewBag.Departments = db.Departments.ToList();
            ViewBag.CurrentPic = t.Picture;
            return View(new EmployeeEditModel { EmployeeId = t.EmployeeId, EmployeeName = t.EmployeeName, JoiningDate = t.JoiningDate, Email = t.Email, Phone = t.Phone, IsWorking = t.IsWorking,  DepartmentId = t.DepartmentId });
        }
        [HttpPost]
        public ActionResult Edit(EmployeeEditModel t)
        {
            Thread.Sleep(2000);
            var employee = db.Employees.First(x => x.EmployeeId == t.EmployeeId);
            if (ModelState.IsValid)
            {

                employee.EmployeeName = t.EmployeeName;
                employee.Email = t.Email;
                employee.JoiningDate = t.JoiningDate;
                employee.Phone = t.Phone;
                employee.IsWorking = t.IsWorking;
                if (t.Picture != null)
                {
                    string ext = Path.GetExtension(t.Picture.FileName);
                    string f = Guid.NewGuid() + ext;
                    t.Picture.SaveAs(Server.MapPath("~/Uploads/") + f);
                    employee.Picture = f;
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CurrentPic = employee.Picture;
            ViewBag.Departments = db.Departments.ToList();
            return View(t);
        }
        public ActionResult Delete(int id)
        {
            return View(db.Employees.Include(x => x.Department).First(x => x.EmployeeId == id));
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            Employee t = new Employee { EmployeeId = id };
            db.Entry(t).State = EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}