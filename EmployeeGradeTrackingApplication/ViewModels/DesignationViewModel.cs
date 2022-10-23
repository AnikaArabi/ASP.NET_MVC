using EmployeeGradeTrackingApplication.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmployeeGradeTrackingApplication.ViewModels
{
    public class DesignationViewModel
    {
        [Required]
        public int DesignationId { get; set; }
        
        public string DesignationName { get; set; }
    }
    
}