namespace BudgetManager.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BankAccounts",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        BankId = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        Description = c.String(maxLength: 500),
                        UserId = c.Guid(nullable: false),
                        Created = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Banks", t => t.BankId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.BankId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Banks",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Description = c.String(maxLength: 500),
                        Created = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BankTransactions",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 500),
                        TransactionDate = c.DateTime(nullable: false),
                        Amount = c.Double(nullable: false),
                        Balance = c.Double(nullable: false),
                        TransactionSequence = c.Int(nullable: false),
                        BankTransactionType = c.Int(nullable: false),
                        GroupId = c.Guid(),
                        BankAccountId = c.Guid(),
                        UserId = c.Guid(nullable: false),
                        Created = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BankAccounts", t => t.BankAccountId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .ForeignKey("dbo.BankTransactionGroups", t => t.GroupId)
                .Index(t => t.GroupId)
                .Index(t => t.BankAccountId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.BankTransactionGroups",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Description = c.String(maxLength: 500),
                        UserId = c.Guid(nullable: false),
                        Created = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                        PasswordHash = c.String(nullable: false),
                        Person_Gender = c.Int(),
                        Person_Title = c.Int(),
                        Person_TitleOther = c.String(),
                        Person_FirstName = c.String(),
                        Person_MiddleNames = c.String(),
                        Person_NickName = c.String(nullable: false),
                        Person_Surname = c.String(nullable: false),
                        Person_ContactDetail_MobileTelephoneNumber = c.String(),
                        Person_ContactDetail_HomeTelephoneNumber = c.String(),
                        Person_ContactDetail_WorkTelephoneNumber = c.String(),
                        Person_ContactDetail_FaxNumber = c.String(),
                        Person_AddressInfoHome_AddressLine = c.String(),
                        Person_AddressInfoHome_Suburb = c.String(),
                        Person_AddressInfoHome_City = c.String(),
                        Person_AddressInfoHome_AreaCode = c.String(),
                        Person_AddressInfoWork_AddressLine = c.String(),
                        Person_AddressInfoWork_Suburb = c.String(),
                        Person_AddressInfoWork_City = c.String(),
                        Person_AddressInfoWork_AreaCode = c.String(),
                        OneTimePin = c.String(),
                        IsValidated = c.Boolean(nullable: false),
                        IsAuthenticated = c.Boolean(nullable: false),
                        IsLocked = c.Boolean(nullable: false),
                        IsAdmin = c.Boolean(nullable: false),
                        Created = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BankTransactionRules",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 500),
                        Text = c.String(nullable: false),
                        RuleType = c.Int(nullable: false),
                        IsCaseSensitive = c.Boolean(nullable: false),
                        BankAccountId = c.Guid(nullable: false),
                        BankTransactionGroupId = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        Created = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BankAccounts", t => t.BankAccountId)
                .ForeignKey("dbo.BankTransactionGroups", t => t.BankTransactionGroupId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.BankAccountId)
                .Index(t => t.BankTransactionGroupId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.BudgetRowItems",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        BudgetDate = c.DateTime(nullable: false),
                        AmountBudget = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AmountActual = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BudgetTemplateItemId = c.Guid(nullable: false),
                        BudgetTypeDateId = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        Created = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BudgetTemplateItems", t => t.BudgetTemplateItemId)
                .ForeignKey("dbo.BudgetTypeDates", t => t.BudgetTypeDateId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.BudgetTemplateItemId)
                .Index(t => t.BudgetTypeDateId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.BudgetTemplateItems",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        BudgetItemType = c.Int(nullable: false),
                        UserId = c.Guid(nullable: false),
                        Created = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.BudgetTypeDates",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        StartDate = c.DateTime(nullable: false),
                        BudgetType = c.Int(nullable: false),
                        UserId = c.Guid(nullable: false),
                        Created = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.BudgetTemplateItemRuleLinks",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        BudgetTemplateItemId = c.Guid(nullable: false),
                        BankTransactionRuleId = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        Created = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BankTransactionRules", t => t.BankTransactionRuleId)
                .ForeignKey("dbo.BudgetTemplateItems", t => t.BudgetTemplateItemId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.BudgetTemplateItemId)
                .Index(t => t.BankTransactionRuleId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Errors",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        HostName = c.String(maxLength: 128),
                        Version = c.String(maxLength: 128),
                        ErrorDate = c.DateTime(nullable: false),
                        ErrorMessage = c.String(),
                        ErrorDetail = c.String(),
                        ClassName = c.String(maxLength: 128),
                        FunctionName = c.String(maxLength: 128),
                        ErrorXml = c.String(),
                        Created = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BudgetTemplateItemRuleLinks", "UserId", "dbo.Users");
            DropForeignKey("dbo.BudgetTemplateItemRuleLinks", "BudgetTemplateItemId", "dbo.BudgetTemplateItems");
            DropForeignKey("dbo.BudgetTemplateItemRuleLinks", "BankTransactionRuleId", "dbo.BankTransactionRules");
            DropForeignKey("dbo.BudgetRowItems", "UserId", "dbo.Users");
            DropForeignKey("dbo.BudgetTypeDates", "UserId", "dbo.Users");
            DropForeignKey("dbo.BudgetRowItems", "BudgetTypeDateId", "dbo.BudgetTypeDates");
            DropForeignKey("dbo.BudgetTemplateItems", "UserId", "dbo.Users");
            DropForeignKey("dbo.BudgetRowItems", "BudgetTemplateItemId", "dbo.BudgetTemplateItems");
            DropForeignKey("dbo.BankTransactionRules", "UserId", "dbo.Users");
            DropForeignKey("dbo.BankTransactionRules", "BankTransactionGroupId", "dbo.BankTransactionGroups");
            DropForeignKey("dbo.BankTransactionRules", "BankAccountId", "dbo.BankAccounts");
            DropForeignKey("dbo.BankTransactions", "GroupId", "dbo.BankTransactionGroups");
            DropForeignKey("dbo.BankTransactionGroups", "UserId", "dbo.Users");
            DropForeignKey("dbo.BankTransactions", "UserId", "dbo.Users");
            DropForeignKey("dbo.BankAccounts", "UserId", "dbo.Users");
            DropForeignKey("dbo.BankTransactions", "BankAccountId", "dbo.BankAccounts");
            DropForeignKey("dbo.BankAccounts", "BankId", "dbo.Banks");
            DropIndex("dbo.BudgetTemplateItemRuleLinks", new[] { "UserId" });
            DropIndex("dbo.BudgetTemplateItemRuleLinks", new[] { "BankTransactionRuleId" });
            DropIndex("dbo.BudgetTemplateItemRuleLinks", new[] { "BudgetTemplateItemId" });
            DropIndex("dbo.BudgetTypeDates", new[] { "UserId" });
            DropIndex("dbo.BudgetTemplateItems", new[] { "UserId" });
            DropIndex("dbo.BudgetRowItems", new[] { "UserId" });
            DropIndex("dbo.BudgetRowItems", new[] { "BudgetTypeDateId" });
            DropIndex("dbo.BudgetRowItems", new[] { "BudgetTemplateItemId" });
            DropIndex("dbo.BankTransactionRules", new[] { "UserId" });
            DropIndex("dbo.BankTransactionRules", new[] { "BankTransactionGroupId" });
            DropIndex("dbo.BankTransactionRules", new[] { "BankAccountId" });
            DropIndex("dbo.BankTransactionGroups", new[] { "UserId" });
            DropIndex("dbo.BankTransactions", new[] { "UserId" });
            DropIndex("dbo.BankTransactions", new[] { "BankAccountId" });
            DropIndex("dbo.BankTransactions", new[] { "GroupId" });
            DropIndex("dbo.BankAccounts", new[] { "UserId" });
            DropIndex("dbo.BankAccounts", new[] { "BankId" });
            DropTable("dbo.Errors");
            DropTable("dbo.BudgetTemplateItemRuleLinks");
            DropTable("dbo.BudgetTypeDates");
            DropTable("dbo.BudgetTemplateItems");
            DropTable("dbo.BudgetRowItems");
            DropTable("dbo.BankTransactionRules");
            DropTable("dbo.Users");
            DropTable("dbo.BankTransactionGroups");
            DropTable("dbo.BankTransactions");
            DropTable("dbo.Banks");
            DropTable("dbo.BankAccounts");
        }
    }
}
