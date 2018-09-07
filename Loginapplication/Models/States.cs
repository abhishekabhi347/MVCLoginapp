using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Loginapplication.Models
{
    public class States
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StateId { get; set; }

        [Required, StringLength(50)]
        [DataType(DataType.Text, ErrorMessage = "Please Enter Only Text")]
        public string StateName { get; set; }

        [ScaffoldColumn(false), StringLength(1)]
        public string CheckStatus { get; set; } = "Y";

        // Foreign key 
        [Display(Name = "Country")]
        public int CountryId { get; set; }

        [ForeignKey("CountryId")]
        public virtual Country Country { get; set; }
    }
}