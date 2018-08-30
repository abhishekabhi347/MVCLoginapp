using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Loginapplication.Models
{
    public class Role
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Roleid { get; set; }

        [Required(ErrorMessage = "Please Enter Role Name")]
        [StringLength(50, ErrorMessage = "Your Name can contain only 50 characters")]
        [DataType(DataType.Text, ErrorMessage = "Please Enter Only Text")]
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }

        [StringLength(2)]
        [ScaffoldColumn(false)]
        public string Checkstatus { get; set; } = "Y";
    }
}