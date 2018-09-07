using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Loginapplication.Models
{
    public class Country
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CountryId { get; set; }

        [Required,StringLength(50)]
        [DataType(DataType.Text, ErrorMessage = "Please Enter Only Text")]
        public string CountryName { get; set; }

        [ScaffoldColumn(false),StringLength(1)]
        public string CheckStatus { get; set; } = "Y";
    }
}