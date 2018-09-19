using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Loginapplication.Models
{
    public class SiteSettings
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SettingsID { get; set; }

        [StringLength(20,ErrorMessage ="Settings Name Should be less than 20")]
        [DisplayName("Settings Name")]
        [Required(ErrorMessage ="Settings Name is required")]
        [DataType(DataType.Text)]
        public string SettingName { get; set; }

        [StringLength(50, ErrorMessage = "Application Title Should be less than 50")]
        [DisplayName("Application Title")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Application Title is required")]
        public string ApplicationTitle { get; set; }

        [DisplayName("Application Title Colour")]
        [StringLength(100)]
        public string ApplicationTitleColour { get; set; }

        [StringLength(50)]
        [DisplayName("Application Title Font Size")]
        [Required(ErrorMessage = "Application Title Font Size is required")]
        public string ApplicationTitleSize { get; set; }

        [StringLength(50)]
        [DisplayName("Application Title Font")]
        [Required(ErrorMessage = "Application Title Font is required")]
        public string ApplicationTitleFont { get; set; }

        [StringLength(50)]
        [DisplayName("Navbar Background Colour")]
        [Required(ErrorMessage = "Navbar Background Colour is required")]
        public string NavColour { get; set; }


        [StringLength(50)]
        [DisplayName("Navbar Text Colour")]
        [Required(ErrorMessage = "Navbar Text Colour is required")]
        public string NavTextColour { get; set; }

        [StringLength(50)]
        [DisplayName("Menu Background Colour")]
        [Required(ErrorMessage = "Menu Background Colour is required")]
        public string MenuColour { get; set; }


        [StringLength(50)]
        [DisplayName("Menu Text Colour")]
        [Required(ErrorMessage = "Menu Text Colour is required")]
        public string MenuTextColour { get; set; }

        [ScaffoldColumn(false)]
        public bool IsActive { get; set; } = false;

        [StringLength(1)]
        [ScaffoldColumn(false)]
        public string Checkstatus { get; set; } = "Y";
    }
}