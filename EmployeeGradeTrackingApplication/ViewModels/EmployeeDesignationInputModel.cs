using EmployeeGradeTrackingApplication.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EmployeeGradeTrackingApplication.ViewModels
{
    public class EmployeeDesignationInputModel
    {
        [Display(Name = "Employee Id")]
        public int EmployeeId { get; set; }
        [Required, StringLength(50), Display(Name = "Employee Name")]
        public string EmployeeName { get; set; }
        [Required, Display(Name = "Joining Date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime JoiningDate { get; set; }
        [Required, StringLength(50), DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required, StringLength(20)]
        public string Phone { get; set; }
        [Display(Name = "Working?")]
        public bool IsWorking { get; set; }
        [Required]
        public HttpPostedFileBase Picture { get; set; }
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }
        public List<DesingnationEmployeeViewModel> Designations { get; set; } = new List<DesingnationEmployeeViewModel>();
    }
}