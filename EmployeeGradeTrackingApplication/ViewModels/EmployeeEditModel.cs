using EmployeeGradeTrackingApplication.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EmployeeGradeTrackingApplication.ViewModels
{
    public class EmployeeEditModel
    {
        [Display(Name = "Employee Id")]
        public int EmployeeId { get; set; }
        [Required, StringLength(50), Display(Name = "Employee Name")]
        public string EmployeeName { get; set; }
        [Required, Display(Name = "Joining Date"), DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime JoiningDate { get; set; }
        [Required, StringLength(50), DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required, StringLength(20)]
        public string Phone { get; set; }
        [Display(Name = "Working?")]
        public bool IsWorking { get; set; }
       
        public HttpPostedFileBase Picture { get; set; }
        [Required]
        public int DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        public Department Department { get; set; }
        public List<DesignationViewModel> Departments { get; set; } = new List<DesignationViewModel>();

    }
}