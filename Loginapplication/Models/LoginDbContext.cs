using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Loginapplication.Models
{
   
    public  class LoginDbContext : DbContext
    {
        public LoginDbContext()
            : base("name=Login")
        {

        }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(e => e.EmpName)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Checkstatus)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.UserName)
                .IsUnicode(false);
        }
    }
}