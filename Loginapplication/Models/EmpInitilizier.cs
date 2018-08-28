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
                new Employee { Name="Abhishek", Company="ISSI", Description="Dev", Country="India",IsEmployeeRetired=false, IsActive =true },
                new Employee { Name="Raju", Company="Google", Description="UI", Country="India",IsEmployeeRetired=false, IsActive =true },
                new Employee { Name="Gautham", Company="TCS", Description="Support", Country="US",IsEmployeeRetired=true, IsActive =true }
            };

            Emp.ForEach(x => context.Employees.Add(x));
            context.SaveChanges();

            base.Seed(context);
        }
    }
}