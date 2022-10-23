using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EmployeeGradeTrackingApplication.Models
{
    //public enum DepartmentName { FINANCE , AUDIT , MARKETING , PRODUCTION}
    //public enum GradeName { Assistant, SeniorAssistant, Officer, SeniorOfficer, Executive, GeneralOfficer}
    //public enum DesignationName { Accounting , Finance, HumanResources, Management }
    public class Department
    {
        [Display(Name = "Department Id")]
        public int DepartmentId { get; set; }
        [Required, StringLength(50), Display(Name = "Department Name")]
        public string DepartmentName { get; set; }
        public virtual ICollection<Employee> Employee { get; set; } = new List<Employee>();

    }
    public class Employee
    {
        [Display(Name ="Employee Id")]
        public int EmployeeId { get; set; }
        [Required, StringLength(50), Display(Name ="Employee Name")]
        public string EmployeeName { get; set; }
        [Required, Display(Name = "Joining Date"), Column(TypeName = "date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime JoiningDate { get; set; }
        [Required, StringLength(50), DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required, StringLength(20)]
        public string Phone { get; set; }
        [Display(Name = "Working?")]
        public bool IsWorking { get; set; }
        [Required, StringLength(200)]
        public string Picture { get; set; }
        [Required]
        public int DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        public Department Department { get; set; }
        public virtual ICollection<DesignationHistory> DesignationHistories { get; set; } = new List<DesignationHistory>();
        public virtual ICollection<GradeHistory> GradeHistories { get; set; } = new List<GradeHistory>();
    }
    public class Grade
    {
        [Display(Name = "Grade Id")]
        public int GradeId { get; set; }
        [Required, StringLength(50), Display(Name = "Grade")]
        public string GradeName { get; set; }
        public virtual ICollection<GradeHistory> GradeHistories { get; set; } = new List<GradeHistory>();


    }
    public class Designation
    {
        [Display(Name = "Designation Id")]
        public int DesignationId { get; set; }
        [Required, StringLength(50), Display(Name = "Designation")]
        public string DesignationName { get; set; }
        public virtual ICollection<DesignationHistory> DesignationHistories { get; set; } = new List<DesignationHistory>();

    }
    public class DesignationHistory
    {
        [Key, Column(Order = 0), ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        [Key, Column(Order = 1), ForeignKey("Designation")]
        public int DesignationId { get; set; }
        [Required, Display(Name = "Start Date"), Column(TypeName = "date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        [Required, Display(Name = "End Date"), Column(TypeName = "date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }
        public Employee Employee { get; set; }
        public Designation Designation { get; set; }
    }
    public class GradeHistory
    {
        [Key, Column(Order =0), ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        [Key, Column(Order = 1), ForeignKey("Grade")]
        public int GradeId { get; set; }
        [Required, Display(Name = "Start Date"), Column(TypeName = "date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        [Required, Display(Name = "End Date"), Column(TypeName = "date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }
        public Employee Employee { get; set; }
        public Grade Grade { get; set; }
    }
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext()
        {
            Database.SetInitializer(new EmployeeDbInitializer());
        }
        public DbSet<Department> Departments  { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Designation> Designations { get; set; }
        public DbSet<DesignationHistory> DesignationHistories { get; set; }
        public DbSet<GradeHistory> GradeHistories { get; set; }
    }
    public class EmployeeDbInitializer: DropCreateDatabaseIfModelChanges<EmployeeDbContext>
    {
        protected override void Seed(EmployeeDbContext db)
        {
            Department dp = new Department { DepartmentName = "FINANCE" };
            Employee em = new Employee { EmployeeName = "Anika Arabi", JoiningDate = DateTime.Now.AddDays(-6 * 30), Email = "anika@gmail.com", Phone = "01782326347", IsWorking = true, Picture = "1.jpg" };
            
            Grade gd = new Grade { GradeName = "General Officer" };
            gd.GradeHistories.Add(new GradeHistory { Grade = gd, Employee = em, StartDate = DateTime.Now.AddDays(-9 * 30), EndDate = DateTime.Now.AddDays(-2 * 30) });
            Designation ds = new Designation { DesignationName = "Human Resources" };
            ds.DesignationHistories.Add(new DesignationHistory { Employee = em, Designation = ds, StartDate = DateTime.Now.AddDays(-9 * 30), EndDate = DateTime.Now.AddDays(-2 * 30) });
            db.Departments.Add(dp);
            db.Employees.Add(em);
            db.Grades.Add(gd);
            db.Designations.Add(ds);
            db.SaveChanges();
        }
    }
}