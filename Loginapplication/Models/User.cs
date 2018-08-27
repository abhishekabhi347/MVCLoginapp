namespace Loginapplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User
    {

        [Key]
        public int Empid { get; set; }

        [Display(Name = "Employee Name")]
        [StringLength(200)]
        //[Required(ErrorMessage = "Employee Name Is Mandatory")]
        public string EmpName { get; set; }

        [Required(ErrorMessage ="Password Is Mandatory")]
        [StringLength(500)]
        public string Password { get; set; }

        [StringLength(200)]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Please Provide Valid Email")]
        public string Email { get; set; }

        [StringLength(2)]
       // [Required(ErrorMessage = "Checkstatus Is Mandatory")]
        public string Checkstatus { get; set; }

        [Display(Name = "User Name")]
        [StringLength(100)]
        [Required(ErrorMessage ="User Name is Mandatory")]
        public string UserName { get; set; }

        public string Vcode { get; set; }
    }
}
