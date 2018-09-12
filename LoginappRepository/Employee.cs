namespace LoginappRepository
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Employee")]
    public partial class Employee
    {
        public int EmployeeId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Description { get; set; }

        public bool IsEmployeeRetired { get; set; }

        public int? CountryId { get; set; }

        public int? StateId { get; set; }

        [StringLength(50)]
        public string Company { get; set; }

        public string EmpDate { get; set; }

        public bool IsActive { get; set; }
    }
}
