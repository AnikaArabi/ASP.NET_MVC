using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmployeeGradeTrackingApplication.ViewModels
{
    public class DesingnationEmployeeViewModel
    {
        [Required]
        public int DesignationId { get; set; }
        [Required, StringLength(50), Display(Name = "Author Name")]
        public string DesignationName { get; set; }
    }
}