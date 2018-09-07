using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Loginapplication.Models
{
    //public class EmpDetails : DbContext
    //{
    //    public EmpDetails(): base("LoginDbCS")
    //    {

    //    }
    //    public DbSet<Employee> Employees { get; set; }
    //}

    public class Employee
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        [DisplayName("Id")]
        public int EmployeeId { get; set; }

        [Required (ErrorMessage ="Please Enter Name")]
        [StringLength(50, ErrorMessage = "Your Name can contain only 50 characters")]
        [DataType(DataType.Text,ErrorMessage ="Please Enter Only Text")]
        public string Name { get; set; }

        [StringLength(100, ErrorMessage = "Description can contain only 100 characters")]
        [Required(ErrorMessage = "Please Enter Description")]
        public string Description { get; set; }

        public bool IsEmployeeRetired { get; set; }

        public int? CountryId { get; set; }

        public int? StateId { get; set; }

        [StringLength(50)]
        public string Company { get; set; }


        public bool IsActive { get; set; } = true;

        


    }
}