using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Loginapplication.Models
{
    public class MenuManagement
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Menu_ID { get; set; }

        [Display(Name = "Menu Parent Id")]
        public int? Menu_Parent_ID { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(100)]
        public string Menu_NAME { get; set; }

        [DataType(DataType.Text)]
        [StringLength(100)]
        public string CONTROLLER_NAME { get; set; }

        [DataType(DataType.Text)]
        [StringLength(100)]
        public string ACTION_NAME { get; set; }

        public int Menu_order { get; set; }

        [StringLength(2)]
        [ScaffoldColumn(false)]
        public string Checkstatus { get; set; } = "Y";


    }
}