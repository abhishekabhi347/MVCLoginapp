namespace Loginapplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Employeedetails : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employee", "EmpDate", c => c.String());
            AlterStoredProcedure(
                "dbo.Employee_Insert",
                p => new
                    {
                        Name = p.String(maxLength: 50),
                        Description = p.String(maxLength: 100),
                        IsEmployeeRetired = p.Boolean(),
                        CountryId = p.Int(),
                        StateId = p.Int(),
                        Company = p.String(maxLength: 50),
                        EmpDate = p.String(),
                        IsActive = p.Boolean(),
                    },
                body:
                    @"INSERT [dbo].[Employee]([Name], [Description], [IsEmployeeRetired], [CountryId], [StateId], [Company], [EmpDate], [IsActive])
                      VALUES (@Name, @Description, @IsEmployeeRetired, @CountryId, @StateId, @Company, @EmpDate, @IsActive)
                      
                      DECLARE @EmployeeId int
                      SELECT @EmployeeId = [EmployeeId]
                      FROM [dbo].[Employee]
                      WHERE @@ROWCOUNT > 0 AND [EmployeeId] = scope_identity()
                      
                      SELECT t0.[EmployeeId]
                      FROM [dbo].[Employee] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[EmployeeId] = @EmployeeId"
            );
            
            AlterStoredProcedure(
                "dbo.Employee_Update",
                p => new
                    {
                        EmployeeId = p.Int(),
                        Name = p.String(maxLength: 50),
                        Description = p.String(maxLength: 100),
                        IsEmployeeRetired = p.Boolean(),
                        CountryId = p.Int(),
                        StateId = p.Int(),
                        Company = p.String(maxLength: 50),
                        EmpDate = p.String(),
                        IsActive = p.Boolean(),
                    },
                body:
                    @"UPDATE [dbo].[Employee]
                      SET [Name] = @Name, [Description] = @Description, [IsEmployeeRetired] = @IsEmployeeRetired, [CountryId] = @CountryId, [StateId] = @StateId, [Company] = @Company, [EmpDate] = @EmpDate, [IsActive] = @IsActive
                      WHERE ([EmployeeId] = @EmployeeId)"
            );
            
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employee", "EmpDate");
            throw new NotSupportedException("Scaffolding create or alter procedure operations is not supported in down methods.");
        }
    }
}
