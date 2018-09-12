namespace Loginapplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User
    {

        [Key,Column(Order =0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]       
        public int Empid { get; set; }

        [Display(Name = "Employee Name")]
        [StringLength(200)]
        [DataType(DataType.Text)]
        //[Required(ErrorMessage = "Employee Name Is Mandatory")]
        public string EmpName { get; set; }

        [Required(ErrorMessage ="Password Is Mandatory")]
        [DataType(DataType.Password)]
        [StringLength(500)]
        public string Password { get; set; }

        [StringLength(200)]
        [DataType(DataType.EmailAddress)]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Please Provide Valid Email")]
        public string Email { get; set; }    

        [Display(Name = "User Name")]
        [StringLength(100)]
        [Required(ErrorMessage ="User Name is Mandatory")]
        public string UserName { get; set; }

        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [StringLength(15)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string PhoneNo { get; set; }


        [Display(Name ="Choose Role")]
        [Required(ErrorMessage ="Please Choose role")]
        public int Roleid { get; set; }

        public string Vcode { get; set; }

        [ScaffoldColumn(false)]
        public List<Role> RoleList { get; set; }

        [ScaffoldColumn(false)]
        public Role Roledetails { get; set; }


        [StringLength(2)]
        [ScaffoldColumn(false)]
        // [Required(ErrorMessage = "Checkstatus Is Mandatory")]
        public string Checkstatus { get; set; } = "Y";

    }
}
