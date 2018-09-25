namespace Loginapplication.Migrations
{
    using Loginapplication.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<EmpDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(EmpDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            
            context.MenuManagement.AddOrUpdate(

                M => M.Menu_NAME,
                new MenuManagement { Menu_ID =1, Menu_Parent_ID = 0, Menu_NAME = "About", CONTROLLER_NAME = "Home", ACTION_NAME = "About" , Menu_order= 1},
                new MenuManagement { Menu_ID =2, Menu_Parent_ID = 0, Menu_NAME = "Contact", CONTROLLER_NAME = "Home", ACTION_NAME = "Contact" ,Menu_order=2},
                new MenuManagement { Menu_ID =3, Menu_Parent_ID = 0, Menu_NAME = "Technologies", CONTROLLER_NAME = "Technology", ACTION_NAME = "Index",Menu_order=3 },
                new MenuManagement { Menu_ID =4, Menu_Parent_ID = 0, Menu_NAME = "Utilities", CONTROLLER_NAME = "", ACTION_NAME = "" ,Menu_order=4},
                new MenuManagement { Menu_ID =5, Menu_Parent_ID = 4, Menu_NAME = "File Upload", CONTROLLER_NAME = "Utility", ACTION_NAME = "Create",Menu_order=401 },
                new MenuManagement { Menu_ID =6, Menu_Parent_ID = 4, Menu_NAME = "Role Management", CONTROLLER_NAME = "Utility", ACTION_NAME = "Roles" ,Menu_order=402},
                new MenuManagement { Menu_ID =7, Menu_Parent_ID = 4, Menu_NAME = "Users", CONTROLLER_NAME = "Utility", ACTION_NAME = "Users", Checkstatus = "N" ,Menu_order=403},
                new MenuManagement { Menu_ID =8, Menu_Parent_ID = 4, Menu_NAME = "Site Settings", CONTROLLER_NAME = "Utility", ACTION_NAME = "SiteSetting" ,Menu_order=404}
                );

            context.siteSettings.AddOrUpdate(
                M => M.SettingName,
                new SiteSettings {SettingsID=1, SettingName = "Default", ApplicationTitle = "Sample Application", ApplicationTitleColour = "FFFFFF", ApplicationTitleFont = "Arial Verdana", ApplicationTitleSize = "20",
                    MenuColour = "0BA5FF", MenuTextColour = "FFFFFF", NavColour = "1DA4FF", NavTextColour = "FFFFFF", IsActive = true }

                );

            context.Users.AddOrUpdate(
               U=>U.EmpName,
               new User { EmpName="Administrator",UserName="admin",Password= "2A-67-C0-9B-5E-50-79-63-AE-08-92-C1-23-54-96-52", Email="admin@gmail.com",Roleid=2,Vcode= "zzcdRo9XRX" }
                
                );

            context.Employees.AddOrUpdate(
                E => E.Name,
                new Employee {EmployeeId=1,  Name = "Abhishek", Company = "ISSI", Description = "Dev", IsEmployeeRetired = false, IsActive = true },
                new Employee {EmployeeId=2, Name = "Raju", Company = "Google", Description = "UI", IsEmployeeRetired = false, IsActive = true },
                new Employee {EmployeeId=3, Name = "Gautham", Company = "TCS", Description = "Support", IsEmployeeRetired = true, IsActive = true }
                );

            context.Roles.AddOrUpdate(
                R => R.RoleName,
                new Role { Roleid=1, RoleName = "Admin", Menu_display = "1,2,3,4,5,6,8" },
                new Role { Roleid=2, RoleName = "Employee", Menu_display = "1,2,3" },
                new Role { Roleid=3, RoleName = "User", Menu_display = "2" }
                );

            context.Countries.AddOrUpdate(
                C => C.CountryName,
                new Country { CountryName = "India" },
                new Country { CountryName = "United States" }
                );

            context.States.AddOrUpdate(
                S => S.StateName,
                new States { StateName = "Delhi", CountryId = 2 },
                new States { StateName = "Punjab", CountryId = 2 },
                new States { StateName = "Andhra Pradesh", CountryId = 2 },
                new States { StateName = "Telangana", CountryId = 2 },
                new States { StateName = "Haryana", CountryId = 2 },
                new States { StateName = "California", CountryId = 1 },
                new States { StateName = "New York", CountryId = 1 },
                new States { StateName = "Virginia", CountryId = 1 },
                new States { StateName = "New Jersey", CountryId = 1 },
                new States { StateName = "Maryland", CountryId = 1 }
                );



            context.SaveChanges();



        }
    }
}
