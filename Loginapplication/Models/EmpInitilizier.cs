using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Loginapplication.Models
{
    public class EmpInitilizier : DropCreateDatabaseIfModelChanges<EmpDbContext>
    {
        protected override void Seed(EmpDbContext context)
        {
            var Emp = new List<Employee>()
            {
                new Employee { Name="Abhishek", Company="ISSI", Description="Dev", IsEmployeeRetired=false, IsActive =true },
                new Employee { Name="Raju", Company="Google", Description="UI", IsEmployeeRetired=false, IsActive =true },
                new Employee { Name="Gautham", Company="TCS", Description="Support",IsEmployeeRetired=true, IsActive =true }
            };

            var roles = new List<Role>()
            {
                new Role { RoleName="Admin" },
                new Role { RoleName="Employee" },
                new Role { RoleName="User" }

            };

            var Countries = new List<Country>()
            {
                new Country {CountryName="India"},
                new Country {CountryName="United States"}

            };

            var State = new List<States>()
            {
                new States {StateName="Delhi", CountryId=2},
                new States {StateName="Punjab", CountryId=2},
                new States {StateName="Andhra Pradesh", CountryId=2},
                new States {StateName="Telangana", CountryId=2},
                new States {StateName="Haryana", CountryId=2},
                new States {StateName="California", CountryId=1},
                new States {StateName="New York", CountryId=1},
                new States {StateName="Virginia", CountryId=1},
                new States {StateName="New Jersey", CountryId=1},
                new States {StateName="Maryland", CountryId=1}

            };

            roles.ForEach(x => context.Roles.Add(x));

            Emp.ForEach(x => context.Employees.Add(x));

            Countries.ForEach(x => context.Countries.Add(x));

            State.ForEach(x => context.States.Add(x));

            context.SaveChanges();

            base.Seed(context);
        }
    }

   
}