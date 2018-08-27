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
        public EmpDbContext() : base("name=LoginDbCS") {
            //Database.SetInitializer<EmpDbContext>(new DropCreateDatabaseIfModelChanges<EmpDbContext>());
        }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            

        }
    }

}