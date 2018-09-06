using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Loginapplication.Models
{
    public class FileUpload
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ImageId { get; set; }

        public string FileName { get; set; }

        public string ImagePath { get; set; }

        public byte[] Imagelength { get; set; }

        [ScaffoldColumn(false)]
        public string Checkstatus { get; set; } = "Y";

    }
}