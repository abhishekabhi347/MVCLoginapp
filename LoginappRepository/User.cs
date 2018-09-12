namespace LoginappRepository
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            Roles = new HashSet<Role>();
        }

        [Key]
        public int Empid { get; set; }

        [StringLength(200)]
        public string EmpName { get; set; }

        [Required]
        [StringLength(500)]
        public string Password { get; set; }

        [StringLength(200)]
        public string Email { get; set; }

        [Required]
        [StringLength(100)]
        public string UserName { get; set; }

        [StringLength(15)]
        public string PhoneNo { get; set; }

        public int? Roleid { get; set; }

        public string Vcode { get; set; }

        [StringLength(2)]
        public string Checkstatus { get; set; }

        public int? Roledetails_Roleid { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Role> Roles { get; set; }

        public virtual Role Role { get; set; }
    }
}
