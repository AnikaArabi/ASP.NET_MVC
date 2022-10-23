using EmployeeGradeTrackingApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeGradeTrackingApplication.Controllers
{
    public class HomeController : Controller
    {
        EmployeeDbContext db = new EmployeeDbContext();
        // GET: Home
        public ActionResult Index()
        {
            var data = db.Employees.ToList();
            return View();
        }
    }
}