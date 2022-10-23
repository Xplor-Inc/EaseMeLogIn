namespace EaseLogMeIn.DatabaseLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CustomException",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MethodName = c.String(),
                        Controller = c.String(),
                        Parameter = c.String(),
                        Error = c.String(),
                        StackTrace = c.String(),
                        UserId = c.Guid(),
                        Data = c.String(),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DesktopAppLoginHistory",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UniqueId = c.Guid(nullable: false),
                        UserId = c.String(),
                        Name = c.String(),
                        IPAddress = c.String(),
                        MACAddress = c.String(),
                        LoginTime = c.DateTime(nullable: false),
                        LogoutTime = c.DateTime(),
                        TimeSpend = c.String(),
                        VisitedWebCounts = c.Int(nullable: false),
                        OSName = c.String(),
                        OSVersion = c.String(),
                        OSArchitecture = c.String(),
                        SystemArchitecture = c.String(),
                        MachineName = c.String(),
                        IsSuccessed = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Employee",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        UserId = c.String(nullable: false),
                        IsNonADUser = c.Boolean(nullable: false),
                        MACId = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        DeleteDate = c.DateTime(),
                        UpdateDate = c.DateTime(),
                        CreatedBy = c.Guid(nullable: false),
                        DeletedBy = c.Guid(),
                        UpdatedBy = c.Guid(),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        UniqueId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RequestLoger",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MethodName = c.String(),
                        Controller = c.String(),
                        Parameter = c.String(),
                        UserId = c.Guid(),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserActionData",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Guid(nullable: false),
                        WebsiteId = c.Guid(nullable: false),
                        Data = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        ActionType = c.Int(nullable: false),
                        IsPasted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Website",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        URL = c.String(nullable: false),
                        UserId = c.String(nullable: false),
                        UserIdTextId = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        PasswordTextId = c.String(nullable: false),
                        ConfigurationType = c.Int(nullable: false),
                        IsBlocked = c.Boolean(nullable: false),
                        Salt = c.String(),
                        ButtonId = c.String(),
                        Script = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        DeleteDate = c.DateTime(),
                        UpdateDate = c.DateTime(),
                        CreatedBy = c.Guid(nullable: false),
                        DeletedBy = c.Guid(),
                        UpdatedBy = c.Guid(),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        UniqueId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WebsiteAccessLog",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Guid(nullable: false),
                        WebsiteId = c.Guid(nullable: false),
                        AccessTime = c.DateTime(nullable: false),
                        Status = c.Boolean(nullable: false),
                        IPAddress = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WebsiteUserMapping",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Guid(nullable: false),
                        WebsiteId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WebUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        UserId = c.String(),
                        Password = c.String(),
                        Salt = c.String(),
                        LastActivityDate = c.DateTime(),
                        CreateDate = c.DateTime(nullable: false),
                        DeleteDate = c.DateTime(),
                        UpdateDate = c.DateTime(),
                        CreatedBy = c.Guid(nullable: false),
                        DeletedBy = c.Guid(),
                        UpdatedBy = c.Guid(),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        UniqueId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.WebUsers");
            DropTable("dbo.WebsiteUserMapping");
            DropTable("dbo.WebsiteAccessLog");
            DropTable("dbo.Website");
            DropTable("dbo.UserActionData");
            DropTable("dbo.RequestLoger");
            DropTable("dbo.Employee");
            DropTable("dbo.DesktopAppLoginHistory");
            DropTable("dbo.CustomException");
        }
    }
}
