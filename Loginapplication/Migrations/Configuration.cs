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

            context.Employees.AddOrUpdate(
                E => E.Name,
                new Employee { Name = "Abhishek", Company = "ISSI", Description = "Dev", IsEmployeeRetired = false, IsActive = true },
                new Employee { Name = "Raju", Company = "Google", Description = "UI", IsEmployeeRetired = false, IsActive = true },
                new Employee { Name = "Gautham", Company = "TCS", Description = "Support", IsEmployeeRetired = true, IsActive = true }
                );

            context.Roles.AddOrUpdate(
                R => R.RoleName,
                new Role { RoleName = "Admin", Menu_display = "1,2,3,4,5,6,8" },
                new Role { RoleName = "Employee", Menu_display = "1,2,3" },
                new Role { RoleName = "User", Menu_display = "2" }
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


            context.MenuManagement.AddOrUpdate(

                M => M.Menu_NAME,
                new MenuManagement { Menu_ID = 1, Menu_Parent_ID = 0, Menu_NAME = "About", CONTROLLER_NAME = "Home", ACTION_NAME = "About" },
                new MenuManagement { Menu_ID = 2, Menu_Parent_ID = 0, Menu_NAME = "Contact", CONTROLLER_NAME = "Home", ACTION_NAME = "Contact" },
                new MenuManagement { Menu_ID = 3, Menu_Parent_ID = 0, Menu_NAME = "Technologies", CONTROLLER_NAME = "Technology", ACTION_NAME = "Index" },
                new MenuManagement { Menu_ID = 4, Menu_Parent_ID = 0, Menu_NAME = "Utilities", CONTROLLER_NAME = "", ACTION_NAME = "" },
                new MenuManagement { Menu_ID = 5, Menu_Parent_ID = 4, Menu_NAME = "File Upload", CONTROLLER_NAME = "Utility", ACTION_NAME = "Create" },
                new MenuManagement { Menu_ID = 6, Menu_Parent_ID = 4, Menu_NAME = "Role Management", CONTROLLER_NAME = "Utility", ACTION_NAME = "Roles" },
                new MenuManagement { Menu_ID = 7, Menu_Parent_ID = 4, Menu_NAME = "Users", CONTROLLER_NAME = "Utility", ACTION_NAME = "Users", Checkstatus = "N" },
                new MenuManagement { Menu_ID = 8, Menu_Parent_ID = 4, Menu_NAME = "Site Settings", CONTROLLER_NAME = "Utility", ACTION_NAME = "SiteSetting" }
                );

            context.siteSettings.AddOrUpdate(
                M => M.SettingName,
                new SiteSettings { SettingName = "Default", ApplicationTitle = "Sample Application", ApplicationTitleColour = "FFFFFF", ApplicationTitleFont = "Arial Verdana", ApplicationTitleSize = "20", MenuColour = "0BA5FF", MenuTextColour = "FFFFFF", NavColour = "1DA4FF", NavTextColour = "FFFFFF",IsActive= true }

                );


            context.SaveChanges();



        }
    }
}
