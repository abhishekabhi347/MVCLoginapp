using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace Loginapplication.Models
{
    public class Technologies
    {
        [Key,Column(Order =0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("ID")]
        public int TechId { get; set; }

        [Required(ErrorMessage ="Technology is required")]
        [StringLength(50,ErrorMessage ="Technology Name must be under 50")]
        public string Technology { get; set; }

        [StringLength(100, ErrorMessage = "Description can contain only 100 characters")]
        [Required(ErrorMessage = "Please Enter Description")]
        public string Description { get; set; }

        [ScaffoldColumn(false)]
        public string Checkstatus { get; set; } = "Y";
    }
}