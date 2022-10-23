using EmployeeGradeTrackingApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using EmployeeGradeTrackingApplication.ViewModels;
using System.IO;

namespace EmployeeGradeTrackingApplication.Controllers
{
    [Authorize]
    public class MasterEmployeeController : Controller
    {
        private readonly EmployeeDbContext db = new EmployeeDbContext();
        // GET: MasterEmployee
        public ActionResult Index()
        {
            var data = db.Employees
                .Include(x => x.DesignationHistories.Select(y => y.Designation))
                .ToList();

            return View(data);
        }
        public ActionResult Create()
        {
            ViewBag.Departments = db.Departments.ToList();
            
            var data = new EmployeeDesignationInputModel();
            data.Designations.Add(new DesingnationEmployeeViewModel());
            return View(data);
        }
        [HttpPost]
        public ActionResult Create(EmployeeDesignationInputModel model, int[] DesignationId)
        {
            if (ModelState.IsValid)
            {
                var e = new Employee
                {
                    EmployeeName = model.EmployeeName,
                    JoiningDate = model.JoiningDate,
                    Email = model.Email,
                    Phone = model.Phone,
                    IsWorking = model.IsWorking,
                    DepartmentId = model.DepartmentId,

                };

                foreach (var i in DesignationId)
                {
                    e.DesignationHistories.Add(new DesignationHistory { DesignationId = i });
                }
                if (model.Picture.ContentLength > 0)
                {
                    string ext = Path.GetExtension(model.Picture.FileName);
                    string fileName = Guid.NewGuid() + ext;
                    model.Picture.SaveAs(Path.Combine(Server.MapPath("~/Uploads"), fileName));
                    e.Picture = fileName;
                }
                db.Employees.Add(e);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Departments = db.Departments.ToList();

            return View(model);
        }
       
        public ActionResult CreateNewField(EmployeeDesignationInputModel data)
        {
            ViewBag.Designations = db.Designations.ToList();

            return PartialView();
        }
    }
}