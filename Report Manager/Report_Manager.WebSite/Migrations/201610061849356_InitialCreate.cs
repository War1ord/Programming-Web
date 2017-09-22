namespace Report_Manager.WebSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DatabaseConnections",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                        Provider = c.String(nullable: false, maxLength: 200),
                        ConnectionString = c.String(nullable: false),
                        DateTimeCreated = c.DateTime(nullable: false),
                        DateTimeModified = c.DateTime(),
                        CreatedById = c.Int(nullable: false),
                        ModifiedById = c.Int(),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.ModifiedById)
                .Index(t => t.Name, unique: true)
                .Index(t => t.CreatedById)
                .Index(t => t.ModifiedById);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        IsUsernameEmail = c.Boolean(nullable: false),
                        DateTimeCreated = c.DateTime(nullable: false),
                        DateTimeModified = c.DateTime(),
                        CreatedById = c.Int(nullable: false),
                        ModifiedById = c.Int(),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        ModifiedBy_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Users", t => t.ModifiedBy_Id)
                .Index(t => t.Username, unique: true)
                .Index(t => t.CreatedById)
                .Index(t => t.ModifiedBy_Id);
            
            CreateTable(
                "dbo.Errors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InnerExceptionId = c.Int(),
                        HelpLink = c.String(),
                        HResult = c.Int(nullable: false),
                        Message = c.String(),
                        Source = c.String(),
                        StackTrace = c.String(),
                        DateTimeCreated = c.DateTime(nullable: false),
                        DateTimeModified = c.DateTime(),
                        CreatedById = c.Int(nullable: false),
                        ModifiedById = c.Int(),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById, cascadeDelete: true)
                .ForeignKey("dbo.Errors", t => t.InnerExceptionId)
                .ForeignKey("dbo.Users", t => t.ModifiedById)
                .Index(t => t.InnerExceptionId)
                .Index(t => t.CreatedById)
                .Index(t => t.ModifiedById);
            
            CreateTable(
                "dbo.ReportHistories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ReportId = c.Int(nullable: false),
                        Action = c.Int(nullable: false),
                        DateTimeCreated = c.DateTime(nullable: false),
                        DateTimeModified = c.DateTime(),
                        CreatedById = c.Int(nullable: false),
                        ModifiedById = c.Int(),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.ModifiedById)
                .ForeignKey("dbo.Reports", t => t.ReportId, cascadeDelete: true)
                .Index(t => t.ReportId)
                .Index(t => t.CreatedById)
                .Index(t => t.ModifiedById);
            
            CreateTable(
                "dbo.Reports",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DatabaseConnectionId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 200),
                        Query = c.String(nullable: false),
                        IsToBeEmailed = c.Boolean(nullable: false),
                        Email_Subject = c.String(nullable: false, maxLength: 500),
                        Email_Body = c.String(nullable: false),
                        Email_IsHtml = c.Boolean(nullable: false),
                        IsToBeSavedToFile = c.Boolean(nullable: false),
                        FullFilePath = c.String(nullable: false),
                        IsReportTemplate = c.Boolean(nullable: false),
                        Template = c.String(),
                        FileType = c.Int(nullable: false),
                        DateTimeCreated = c.DateTime(nullable: false),
                        DateTimeModified = c.DateTime(),
                        CreatedById = c.Int(nullable: false),
                        ModifiedById = c.Int(),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById, cascadeDelete: true)
                .ForeignKey("dbo.DatabaseConnections", t => t.DatabaseConnectionId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.ModifiedById)
                .Index(t => t.DatabaseConnectionId)
                .Index(t => t.Name, unique: true)
                .Index(t => t.CreatedById)
                .Index(t => t.ModifiedById);
            
            CreateTable(
                "dbo.ReportSchedules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ReportId = c.Int(nullable: false),
                        IntervalType = c.Int(nullable: false),
                        TimeSpanValue = c.Time(nullable: false, precision: 7),
                        DateTimeStart = c.DateTime(),
                        DateTimeEnd = c.DateTime(),
                        IsEnabled = c.Boolean(nullable: false),
                        DateTimeCreated = c.DateTime(nullable: false),
                        DateTimeModified = c.DateTime(),
                        CreatedById = c.Int(nullable: false),
                        ModifiedById = c.Int(),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.ModifiedById)
                .ForeignKey("dbo.Reports", t => t.ReportId, cascadeDelete: true)
                .Index(t => t.ReportId)
                .Index(t => t.CreatedById)
                .Index(t => t.ModifiedById);
            
            CreateTable(
                "dbo.ReportUserLinks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ReportId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        DateTimeCreated = c.DateTime(nullable: false),
                        DateTimeModified = c.DateTime(),
                        CreatedById = c.Int(nullable: false),
                        ModifiedById = c.Int(),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.ModifiedById)
                .ForeignKey("dbo.Reports", t => t.ReportId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.ReportId)
                .Index(t => t.UserId)
                .Index(t => t.CreatedById)
                .Index(t => t.ModifiedById);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                        DateTimeCreated = c.DateTime(nullable: false),
                        DateTimeModified = c.DateTime(),
                        CreatedById = c.Int(nullable: false),
                        ModifiedById = c.Int(),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.ModifiedById)
                .Index(t => t.CreatedById)
                .Index(t => t.ModifiedById);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                        DateTimeCreated = c.DateTime(nullable: false),
                        DateTimeModified = c.DateTime(),
                        CreatedById = c.Int(nullable: false),
                        ModifiedById = c.Int(),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.ModifiedById)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId)
                .Index(t => t.CreatedById)
                .Index(t => t.ModifiedById);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.UserRoles", "ModifiedById", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.Roles", "ModifiedById", "dbo.Users");
            DropForeignKey("dbo.Roles", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.ReportUserLinks", "UserId", "dbo.Users");
            DropForeignKey("dbo.ReportUserLinks", "ReportId", "dbo.Reports");
            DropForeignKey("dbo.ReportUserLinks", "ModifiedById", "dbo.Users");
            DropForeignKey("dbo.ReportUserLinks", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.ReportSchedules", "ReportId", "dbo.Reports");
            DropForeignKey("dbo.ReportSchedules", "ModifiedById", "dbo.Users");
            DropForeignKey("dbo.ReportSchedules", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.ReportHistories", "ReportId", "dbo.Reports");
            DropForeignKey("dbo.Reports", "ModifiedById", "dbo.Users");
            DropForeignKey("dbo.Reports", "DatabaseConnectionId", "dbo.DatabaseConnections");
            DropForeignKey("dbo.Reports", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.ReportHistories", "ModifiedById", "dbo.Users");
            DropForeignKey("dbo.ReportHistories", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.Errors", "ModifiedById", "dbo.Users");
            DropForeignKey("dbo.Errors", "InnerExceptionId", "dbo.Errors");
            DropForeignKey("dbo.Errors", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.DatabaseConnections", "ModifiedById", "dbo.Users");
            DropForeignKey("dbo.DatabaseConnections", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.Users", "ModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.Users", "CreatedById", "dbo.Users");
            DropIndex("dbo.UserRoles", new[] { "ModifiedById" });
            DropIndex("dbo.UserRoles", new[] { "CreatedById" });
            DropIndex("dbo.UserRoles", new[] { "RoleId" });
            DropIndex("dbo.UserRoles", new[] { "UserId" });
            DropIndex("dbo.Roles", new[] { "ModifiedById" });
            DropIndex("dbo.Roles", new[] { "CreatedById" });
            DropIndex("dbo.ReportUserLinks", new[] { "ModifiedById" });
            DropIndex("dbo.ReportUserLinks", new[] { "CreatedById" });
            DropIndex("dbo.ReportUserLinks", new[] { "UserId" });
            DropIndex("dbo.ReportUserLinks", new[] { "ReportId" });
            DropIndex("dbo.ReportSchedules", new[] { "ModifiedById" });
            DropIndex("dbo.ReportSchedules", new[] { "CreatedById" });
            DropIndex("dbo.ReportSchedules", new[] { "ReportId" });
            DropIndex("dbo.Reports", new[] { "ModifiedById" });
            DropIndex("dbo.Reports", new[] { "CreatedById" });
            DropIndex("dbo.Reports", new[] { "Name" });
            DropIndex("dbo.Reports", new[] { "DatabaseConnectionId" });
            DropIndex("dbo.ReportHistories", new[] { "ModifiedById" });
            DropIndex("dbo.ReportHistories", new[] { "CreatedById" });
            DropIndex("dbo.ReportHistories", new[] { "ReportId" });
            DropIndex("dbo.Errors", new[] { "ModifiedById" });
            DropIndex("dbo.Errors", new[] { "CreatedById" });
            DropIndex("dbo.Errors", new[] { "InnerExceptionId" });
            DropIndex("dbo.Users", new[] { "ModifiedBy_Id" });
            DropIndex("dbo.Users", new[] { "CreatedById" });
            DropIndex("dbo.Users", new[] { "Username" });
            DropIndex("dbo.DatabaseConnections", new[] { "ModifiedById" });
            DropIndex("dbo.DatabaseConnections", new[] { "CreatedById" });
            DropIndex("dbo.DatabaseConnections", new[] { "Name" });
            DropTable("dbo.UserRoles");
            DropTable("dbo.Roles");
            DropTable("dbo.ReportUserLinks");
            DropTable("dbo.ReportSchedules");
            DropTable("dbo.Reports");
            DropTable("dbo.ReportHistories");
            DropTable("dbo.Errors");
            DropTable("dbo.Users");
            DropTable("dbo.DatabaseConnections");
        }
    }
}
