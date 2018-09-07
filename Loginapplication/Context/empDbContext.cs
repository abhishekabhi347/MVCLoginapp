using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Loginapplication.Models
{
    public class EmpDbContext : DbContext
    {
        public EmpDbContext() : base("name=DefaultCS")
        {
            //Database.SetInitializer(new EmpInitilizier());
        }

        public DbSet<Employee> Employees { get; set; }


        public DbSet<Technologies> Technologies { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<FileUpload> FileUploads { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<States> States { get; set; }

        public virtual DbSet<User> Users { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Types().Configure(t => t.MapToStoredProcedures());

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