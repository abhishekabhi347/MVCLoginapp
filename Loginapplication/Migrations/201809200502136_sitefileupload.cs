namespace Loginapplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sitefileupload : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SiteSettings", "FileName", c => c.String());
            AddColumn("dbo.SiteSettings", "ImagePath", c => c.String());
            AddColumn("dbo.SiteSettings", "Imagelength", c => c.Binary());
            AlterStoredProcedure(
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
            
            AlterStoredProcedure(
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
            
        }
        
        public override void Down()
        {
            DropColumn("dbo.SiteSettings", "Imagelength");
            DropColumn("dbo.SiteSettings", "ImagePath");
            DropColumn("dbo.SiteSettings", "FileName");
            throw new NotSupportedException("Scaffolding create or alter procedure operations is not supported in down methods.");
        }
    }
}
