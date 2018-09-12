namespace LoginappRepository
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class State
    {
        public int StateId { get; set; }

        [Required]
        [StringLength(50)]
        public string StateName { get; set; }

        [StringLength(1)]
        public string CheckStatus { get; set; }

        public int CountryId { get; set; }

        public virtual Country Country { get; set; }
    }
}
