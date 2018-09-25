namespace Loginapplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigrations : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Country",
                c => new
                    {
                        CountryId = c.Int(nullable: false, identity: true),
                        CountryName = c.String(nullable: false, maxLength: 50),
                        CheckStatus = c.String(maxLength: 1),
                    })
                .PrimaryKey(t => t.CountryId);
            
            CreateTable(
                "dbo.Employee",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Description = c.String(nullable: false, maxLength: 100),
                        IsEmployeeRetired = c.Boolean(nullable: false),
                        CountryId = c.Int(),
                        StateId = c.Int(),
                        Company = c.String(maxLength: 50),
                        EmpDate = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeeId);
            
            CreateTable(
                "dbo.FileUpload",
                c => new
                    {
                        ImageId = c.Int(nullable: false, identity: true),
                        FileName = c.String(),
                        ImagePath = c.String(),
                        Imagelength = c.Binary(),
                        Checkstatus = c.String(),
                    })
                .PrimaryKey(t => t.ImageId);
            
            CreateTable(
                "dbo.MenuManagement",
                c => new
                    {
                        Menu_ID = c.Int(nullable: false, identity: true),
                        Menu_Parent_ID = c.Int(),
                        Menu_NAME = c.String(nullable: false, maxLength: 100),
                        CONTROLLER_NAME = c.String(maxLength: 100),
                        ACTION_NAME = c.String(maxLength: 100),
                        Menu_order = c.Int(nullable: false),
                        Checkstatus = c.String(maxLength: 2),
                    })
                .PrimaryKey(t => t.Menu_ID);
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        Roleid = c.Int(nullable: false, identity: true),
                        RoleName = c.String(nullable: false, maxLength: 50),
                        Menu_display = c.String(),
                        Checkstatus = c.String(maxLength: 2),
                        User_Empid = c.Int(),
                    })
                .PrimaryKey(t => t.Roleid)
                .ForeignKey("dbo.User", t => t.User_Empid)
                .Index(t => t.User_Empid);
            
            CreateTable(
                "dbo.SiteSettings",
                c => new
                    {
                        SettingsID = c.Int(nullable: false, identity: true),
                        SettingName = c.String(nullable: false, maxLength: 20),
                        ApplicationTitle = c.String(nullable: false, maxLength: 50),
                        ApplicationTitleColour = c.String(maxLength: 100),
                        ApplicationTitleSize = c.String(nullable: false, maxLength: 50),
                        ApplicationTitleFont = c.String(nullable: false, maxLength: 50),
                        NavColour = c.String(nullable: false, maxLength: 50),
                        NavTextColour = c.String(nullable: false, maxLength: 50),
                        MenuColour = c.String(nullable: false, maxLength: 50),
                        MenuTextColour = c.String(nullable: false, maxLength: 50),
                        FileName = c.String(),
                        ImagePath = c.String(),
                        Imagelength = c.Binary(),
                        IsActive = c.Boolean(nullable: false),
                        Checkstatus = c.String(maxLength: 1),
                    })
                .PrimaryKey(t => t.SettingsID);
            
            CreateTable(
                "dbo.States",
                c => new
                    {
                        StateId = c.Int(nullable: false, identity: true),
                        StateName = c.String(nullable: false, maxLength: 50),
                        CheckStatus = c.String(maxLength: 1),
                        CountryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StateId)
                .ForeignKey("dbo.Country", t => t.CountryId, cascadeDelete: true)
                .Index(t => t.CountryId);
            
            CreateTable(
                "dbo.Technologies",
                c => new
                    {
                        TechId = c.Int(nullable: false, identity: true),
                        Technology = c.String(nullable: false, maxLength: 50),
                        Description = c.String(nullable: false, maxLength: 100),
                        Checkstatus = c.String(),
                    })
                .PrimaryKey(t => t.TechId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Empid = c.Int(nullable: false, identity: true),
                        EmpName = c.String(maxLength: 200, unicode: false),
                        Password = c.String(nullable: false, maxLength: 500),
                        Email = c.String(maxLength: 200),
                        UserName = c.String(nullable: false, maxLength: 100, unicode: false),
                        PhoneNo = c.String(maxLength: 15),
                        Roleid = c.Int(nullable: false),
                        Vcode = c.String(),
                        Checkstatus = c.String(maxLength: 2, fixedLength: true, unicode: false),
                        Roledetails_Roleid = c.Int(),
                    })
                .PrimaryKey(t => t.Empid)
                .ForeignKey("dbo.Role", t => t.Roledetails_Roleid)
                .Index(t => t.Roledetails_Roleid);
            
            CreateStoredProcedure(
                "dbo.Country_Insert",
                p => new
                    {
                        CountryName = p.String(maxLength: 50),
                        CheckStatus = p.String(maxLength: 1),
                    },
                body:
                    @"INSERT [dbo].[Country]([CountryName], [CheckStatus])
                      VALUES (@CountryName, @CheckStatus)
                      
                      DECLARE @CountryId int
                      SELECT @CountryId = [CountryId]
                      FROM [dbo].[Country]
                      WHERE @@ROWCOUNT > 0 AND [CountryId] = scope_identity()
                      
                      SELECT t0.[CountryId]
                      FROM [dbo].[Country] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[CountryId] = @CountryId"
            );
            
            CreateStoredProcedure(
                "dbo.Country_Update",
                p => new
                    {
                        CountryId = p.Int(),
                        CountryName = p.String(maxLength: 50),
                        CheckStatus = p.String(maxLength: 1),
                    },
                body:
                    @"UPDATE [dbo].[Country]
                      SET [CountryName] = @CountryName, [CheckStatus] = @CheckStatus
                      WHERE ([CountryId] = @CountryId)"
            );
            
            CreateStoredProcedure(
                "dbo.Country_Delete",
                p => new
                    {
                        CountryId = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[Country]
                      WHERE ([CountryId] = @CountryId)"
            );
            
            CreateStoredProcedure(
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
            
            CreateStoredProcedure(
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
            
            CreateStoredProcedure(
                "dbo.Employee_Delete",
                p => new
                    {
                        EmployeeId = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[Employee]
                      WHERE ([EmployeeId] = @EmployeeId)"
            );
            
            CreateStoredProcedure(
                "dbo.FileUpload_Insert",
                p => new
                    {
                        FileName = p.String(),
                        ImagePath = p.String(),
                        Imagelength = p.Binary(),
                        Checkstatus = p.String(),
                    },
                body:
                    @"INSERT [dbo].[FileUpload]([FileName], [ImagePath], [Imagelength], [Checkstatus])
                      VALUES (@FileName, @ImagePath, @Imagelength, @Checkstatus)
                      
                      DECLARE @ImageId int
                      SELECT @ImageId = [ImageId]
                      FROM [dbo].[FileUpload]
                      WHERE @@ROWCOUNT > 0 AND [ImageId] = scope_identity()
                      
                      SELECT t0.[ImageId]
                      FROM [dbo].[FileUpload] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[ImageId] = @ImageId"
            );
            
            CreateStoredProcedure(
                "dbo.FileUpload_Update",
                p => new
                    {
                        ImageId = p.Int(),
                        FileName = p.String(),
                        ImagePath = p.String(),
                        Imagelength = p.Binary(),
                        Checkstatus = p.String(),
                    },
                body:
                    @"UPDATE [dbo].[FileUpload]
                      SET [FileName] = @FileName, [ImagePath] = @ImagePath, [Imagelength] = @Imagelength, [Checkstatus] = @Checkstatus
                      WHERE ([ImageId] = @ImageId)"
            );
            
            CreateStoredProcedure(
                "dbo.FileUpload_Delete",
                p => new
                    {
                        ImageId = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[FileUpload]
                      WHERE ([ImageId] = @ImageId)"
            );
            
            CreateStoredProcedure(
                "dbo.MenuManagement_Insert",
                p => new
                    {
                        Menu_Parent_ID = p.Int(),
                        Menu_NAME = p.String(maxLength: 100),
                        CONTROLLER_NAME = p.String(maxLength: 100),
                        ACTION_NAME = p.String(maxLength: 100),
                        Menu_order = p.Int(),
                        Checkstatus = p.String(maxLength: 2),
                    },
                body:
                    @"INSERT [dbo].[MenuManagement]([Menu_Parent_ID], [Menu_NAME], [CONTROLLER_NAME], [ACTION_NAME], [Menu_order], [Checkstatus])
                      VALUES (@Menu_Parent_ID, @Menu_NAME, @CONTROLLER_NAME, @ACTION_NAME, @Menu_order, @Checkstatus)
                      
                      DECLARE @Menu_ID int
                      SELECT @Menu_ID = [Menu_ID]
                      FROM [dbo].[MenuManagement]
                      WHERE @@ROWCOUNT > 0 AND [Menu_ID] = scope_identity()
                      
                      SELECT t0.[Menu_ID]
                      FROM [dbo].[MenuManagement] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Menu_ID] = @Menu_ID"
            );
            
            CreateStoredProcedure(
                "dbo.MenuManagement_Update",
                p => new
                    {
                        Menu_ID = p.Int(),
                        Menu_Parent_ID = p.Int(),
                        Menu_NAME = p.String(maxLength: 100),
                        CONTROLLER_NAME = p.String(maxLength: 100),
                        ACTION_NAME = p.String(maxLength: 100),
                        Menu_order = p.Int(),
                        Checkstatus = p.String(maxLength: 2),
                    },
                body:
                    @"UPDATE [dbo].[MenuManagement]
                      SET [Menu_Parent_ID] = @Menu_Parent_ID, [Menu_NAME] = @Menu_NAME, [CONTROLLER_NAME] = @CONTROLLER_NAME, [ACTION_NAME] = @ACTION_NAME, [Menu_order] = @Menu_order, [Checkstatus] = @Checkstatus
                      WHERE ([Menu_ID] = @Menu_ID)"
            );
            
            CreateStoredProcedure(
                "dbo.MenuManagement_Delete",
                p => new
                    {
                        Menu_ID = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[MenuManagement]
                      WHERE ([Menu_ID] = @Menu_ID)"
            );
            
            CreateStoredProcedure(
                "dbo.Role_Insert",
                p => new
                    {
                        RoleName = p.String(maxLength: 50),
                        Menu_display = p.String(),
                        Checkstatus = p.String(maxLength: 2),
                        User_Empid = p.Int(),
                    },
                body:
                    @"INSERT [dbo].[Role]([RoleName], [Menu_display], [Checkstatus], [User_Empid])
                      VALUES (@RoleName, @Menu_display, @Checkstatus, @User_Empid)
                      
                      DECLARE @Roleid int
                      SELECT @Roleid = [Roleid]
                      FROM [dbo].[Role]
                      WHERE @@ROWCOUNT > 0 AND [Roleid] = scope_identity()
                      
                      SELECT t0.[Roleid]
                      FROM [dbo].[Role] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Roleid] = @Roleid"
            );
            
            CreateStoredProcedure(
                "dbo.Role_Update",
                p => new
                    {
                        Roleid = p.Int(),
                        RoleName = p.String(maxLength: 50),
                        Menu_display = p.String(),
                        Checkstatus = p.String(maxLength: 2),
                        User_Empid = p.Int(),
                    },
                body:
                    @"UPDATE [dbo].[Role]
                      SET [RoleName] = @RoleName, [Menu_display] = @Menu_display, [Checkstatus] = @Checkstatus, [User_Empid] = @User_Empid
                      WHERE ([Roleid] = @Roleid)"
            );
            
            CreateStoredProcedure(
                "dbo.Role_Delete",
                p => new
                    {
                        Roleid = p.Int(),
                        User_Empid = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[Role]
                      WHERE (([Roleid] = @Roleid) AND (([User_Empid] = @User_Empid) OR ([User_Empid] IS NULL AND @User_Empid IS NULL)))"
            );
            
            CreateStoredProcedure(
                "dbo.SiteSettings_Insert",
                p => new
                    {
                        SettingName = p.String(maxLength: 20),
                        ApplicationTitle = p.String(maxLength: 50),
                        ApplicationTitleColour = p.String(maxLength: 100),
                        ApplicationTitleSize = p.String(maxLength: 50),
                        ApplicationTitleFont = p.String(maxLength: 50),
                        NavColour = p.String(maxLength: 50),
                        NavTextColour = p.String(maxLength: 50),
                        MenuColour = p.String(maxLength: 50),
                        MenuTextColour = p.String(maxLength: 50),
                        FileName = p.String(),
                        ImagePath = p.String(),
                        Imagelength = p.Binary(),
                        IsActive = p.Boolean(),
                        Checkstatus = p.String(maxLength: 1),
                    },
                body:
                    @"INSERT [dbo].[SiteSettings]([SettingName], [ApplicationTitle], [ApplicationTitleColour], [ApplicationTitleSize], [ApplicationTitleFont], [NavColour], [NavTextColour], [MenuColour], [MenuTextColour], [FileName], [ImagePath], [Imagelength], [IsActive], [Checkstatus])
                      VALUES (@SettingName, @ApplicationTitle, @ApplicationTitleColour, @ApplicationTitleSize, @ApplicationTitleFont, @NavColour, @NavTextColour, @MenuColour, @MenuTextColour, @FileName, @ImagePath, @Imagelength, @IsActive, @Checkstatus)
                      
                      DECLARE @SettingsID int
                      SELECT @SettingsID = [SettingsID]
                      FROM [dbo].[SiteSettings]
                      WHERE @@ROWCOUNT > 0 AND [SettingsID] = scope_identity()
                      
                      SELECT t0.[SettingsID]
                      FROM [dbo].[SiteSettings] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[SettingsID] = @SettingsID"
            );
            
            CreateStoredProcedure(
                "dbo.SiteSettings_Update",
                p => new
                    {
                        SettingsID = p.Int(),
                        SettingName = p.String(maxLength: 20),
                        ApplicationTitle = p.String(maxLength: 50),
                        ApplicationTitleColour = p.String(maxLength: 100),
                        ApplicationTitleSize = p.String(maxLength: 50),
                        ApplicationTitleFont = p.String(maxLength: 50),
                        NavColour = p.String(maxLength: 50),
                        NavTextColour = p.String(maxLength: 50),
                        MenuColour = p.String(maxLength: 50),
                        MenuTextColour = p.String(maxLength: 50),
                        FileName = p.String(),
                        ImagePath = p.String(),
                        Imagelength = p.Binary(),
                        IsActive = p.Boolean(),
                        Checkstatus = p.String(maxLength: 1),
                    },
                body:
                    @"UPDATE [dbo].[SiteSettings]
                      SET [SettingName] = @SettingName, [ApplicationTitle] = @ApplicationTitle, [ApplicationTitleColour] = @ApplicationTitleColour, [ApplicationTitleSize] = @ApplicationTitleSize, [ApplicationTitleFont] = @ApplicationTitleFont, [NavColour] = @NavColour, [NavTextColour] = @NavTextColour, [MenuColour] = @MenuColour, [MenuTextColour] = @MenuTextColour, [FileName] = @FileName, [ImagePath] = @ImagePath, [Imagelength] = @Imagelength, [IsActive] = @IsActive, [Checkstatus] = @Checkstatus
                      WHERE ([SettingsID] = @SettingsID)"
            );
            
            CreateStoredProcedure(
                "dbo.SiteSettings_Delete",
                p => new
                    {
                        SettingsID = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[SiteSettings]
                      WHERE ([SettingsID] = @SettingsID)"
            );
            
            CreateStoredProcedure(
                "dbo.States_Insert",
                p => new
                    {
                        StateName = p.String(maxLength: 50),
                        CheckStatus = p.String(maxLength: 1),
                        CountryId = p.Int(),
                    },
                body:
                    @"INSERT [dbo].[States]([StateName], [CheckStatus], [CountryId])
                      VALUES (@StateName, @CheckStatus, @CountryId)
                      
                      DECLARE @StateId int
                      SELECT @StateId = [StateId]
                      FROM [dbo].[States]
                      WHERE @@ROWCOUNT > 0 AND [StateId] = scope_identity()
                      
                      SELECT t0.[StateId]
                      FROM [dbo].[States] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[StateId] = @StateId"
            );
            
            CreateStoredProcedure(
                "dbo.States_Update",
                p => new
                    {
                        StateId = p.Int(),
                        StateName = p.String(maxLength: 50),
                        CheckStatus = p.String(maxLength: 1),
                        CountryId = p.Int(),
                    },
                body:
                    @"UPDATE [dbo].[States]
                      SET [StateName] = @StateName, [CheckStatus] = @CheckStatus, [CountryId] = @CountryId
                      WHERE ([StateId] = @StateId)"
            );
            
            CreateStoredProcedure(
                "dbo.States_Delete",
                p => new
                    {
                        StateId = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[States]
                      WHERE ([StateId] = @StateId)"
            );
            
            CreateStoredProcedure(
                "dbo.Technologies_Insert",
                p => new
                    {
                        Technology = p.String(maxLength: 50),
                        Description = p.String(maxLength: 100),
                        Checkstatus = p.String(),
                    },
                body:
                    @"INSERT [dbo].[Technologies]([Technology], [Description], [Checkstatus])
                      VALUES (@Technology, @Description, @Checkstatus)
                      
                      DECLARE @TechId int
                      SELECT @TechId = [TechId]
                      FROM [dbo].[Technologies]
                      WHERE @@ROWCOUNT > 0 AND [TechId] = scope_identity()
                      
                      SELECT t0.[TechId]
                      FROM [dbo].[Technologies] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[TechId] = @TechId"
            );
            
            CreateStoredProcedure(
                "dbo.Technologies_Update",
                p => new
                    {
                        TechId = p.Int(),
                        Technology = p.String(maxLength: 50),
                        Description = p.String(maxLength: 100),
                        Checkstatus = p.String(),
                    },
                body:
                    @"UPDATE [dbo].[Technologies]
                      SET [Technology] = @Technology, [Description] = @Description, [Checkstatus] = @Checkstatus
                      WHERE ([TechId] = @TechId)"
            );
            
            CreateStoredProcedure(
                "dbo.Technologies_Delete",
                p => new
                    {
                        TechId = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[Technologies]
                      WHERE ([TechId] = @TechId)"
            );
            
            CreateStoredProcedure(
                "dbo.User_Insert",
                p => new
                    {
                        EmpName = p.String(maxLength: 200, unicode: false),
                        Password = p.String(maxLength: 500),
                        Email = p.String(maxLength: 200),
                        UserName = p.String(maxLength: 100, unicode: false),
                        PhoneNo = p.String(maxLength: 15),
                        Roleid = p.Int(),
                        Vcode = p.String(),
                        Checkstatus = p.String(maxLength: 2, fixedLength: true, unicode: false),
                        Roledetails_Roleid = p.Int(),
                    },
                body:
                    @"INSERT [dbo].[User]([EmpName], [Password], [Email], [UserName], [PhoneNo], [Roleid], [Vcode], [Checkstatus], [Roledetails_Roleid])
                      VALUES (@EmpName, @Password, @Email, @UserName, @PhoneNo, @Roleid, @Vcode, @Checkstatus, @Roledetails_Roleid)
                      
                      DECLARE @Empid int
                      SELECT @Empid = [Empid]
                      FROM [dbo].[User]
                      WHERE @@ROWCOUNT > 0 AND [Empid] = scope_identity()
                      
                      SELECT t0.[Empid]
                      FROM [dbo].[User] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Empid] = @Empid"
            );
            
            CreateStoredProcedure(
                "dbo.User_Update",
                p => new
                    {
                        Empid = p.Int(),
                        EmpName = p.String(maxLength: 200, unicode: false),
                        Password = p.String(maxLength: 500),
                        Email = p.String(maxLength: 200),
                        UserName = p.String(maxLength: 100, unicode: false),
                        PhoneNo = p.String(maxLength: 15),
                        Roleid = p.Int(),
                        Vcode = p.String(),
                        Checkstatus = p.String(maxLength: 2, fixedLength: true, unicode: false),
                        Roledetails_Roleid = p.Int(),
                    },
                body:
                    @"UPDATE [dbo].[User]
                      SET [EmpName] = @EmpName, [Password] = @Password, [Email] = @Email, [UserName] = @UserName, [PhoneNo] = @PhoneNo, [Roleid] = @Roleid, [Vcode] = @Vcode, [Checkstatus] = @Checkstatus, [Roledetails_Roleid] = @Roledetails_Roleid
                      WHERE ([Empid] = @Empid)"
            );
            
            CreateStoredProcedure(
                "dbo.User_Delete",
                p => new
                    {
                        Empid = p.Int(),
                        Roledetails_Roleid = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[User]
                      WHERE (([Empid] = @Empid) AND (([Roledetails_Roleid] = @Roledetails_Roleid) OR ([Roledetails_Roleid] IS NULL AND @Roledetails_Roleid IS NULL)))"
            );
            
        }
        
        public override void Down()
        {
            DropStoredProcedure("dbo.User_Delete");
            DropStoredProcedure("dbo.User_Update");
            DropStoredProcedure("dbo.User_Insert");
            DropStoredProcedure("dbo.Technologies_Delete");
            DropStoredProcedure("dbo.Technologies_Update");
            DropStoredProcedure("dbo.Technologies_Insert");
            DropStoredProcedure("dbo.States_Delete");
            DropStoredProcedure("dbo.States_Update");
            DropStoredProcedure("dbo.States_Insert");
            DropStoredProcedure("dbo.SiteSettings_Delete");
            DropStoredProcedure("dbo.SiteSettings_Update");
            DropStoredProcedure("dbo.SiteSettings_Insert");
            DropStoredProcedure("dbo.Role_Delete");
            DropStoredProcedure("dbo.Role_Update");
            DropStoredProcedure("dbo.Role_Insert");
            DropStoredProcedure("dbo.MenuManagement_Delete");
            DropStoredProcedure("dbo.MenuManagement_Update");
            DropStoredProcedure("dbo.MenuManagement_Insert");
            DropStoredProcedure("dbo.FileUpload_Delete");
            DropStoredProcedure("dbo.FileUpload_Update");
            DropStoredProcedure("dbo.FileUpload_Insert");
            DropStoredProcedure("dbo.Employee_Delete");
            DropStoredProcedure("dbo.Employee_Update");
            DropStoredProcedure("dbo.Employee_Insert");
            DropStoredProcedure("dbo.Country_Delete");
            DropStoredProcedure("dbo.Country_Update");
            DropStoredProcedure("dbo.Country_Insert");
            DropForeignKey("dbo.Role", "User_Empid", "dbo.User");
            DropForeignKey("dbo.User", "Roledetails_Roleid", "dbo.Role");
            DropForeignKey("dbo.States", "CountryId", "dbo.Country");
            DropIndex("dbo.User", new[] { "Roledetails_Roleid" });
            DropIndex("dbo.States", new[] { "CountryId" });
            DropIndex("dbo.Role", new[] { "User_Empid" });
            DropTable("dbo.User");
            DropTable("dbo.Technologies");
            DropTable("dbo.States");
            DropTable("dbo.SiteSettings");
            DropTable("dbo.Role");
            DropTable("dbo.MenuManagement");
            DropTable("dbo.FileUpload");
            DropTable("dbo.Employee");
            DropTable("dbo.Country");
        }
    }
}
