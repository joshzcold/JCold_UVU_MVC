namespace JCold_UVU_MVC_Inventory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        BooksID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        ISBN = c.String(),
                        Author = c.String(),
                        Publisher = c.String(),
                        Number = c.Int(nullable: false),
                        Available = c.Boolean(nullable: false),
                        ClassRoom = c.String(),
                    })
                .PrimaryKey(t => t.BooksID);
            
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        FileId = c.Int(nullable: false, identity: true),
                        FileName = c.String(maxLength: 255),
                        ContentType = c.String(maxLength: 100),
                        Content = c.Binary(),
                        FileType = c.Int(nullable: false),
                        BooksId = c.Int(),
                        SuppliesId = c.Int(),
                    })
                .PrimaryKey(t => t.FileId)
                .ForeignKey("dbo.Books", t => t.BooksId)
                .ForeignKey("dbo.Supplies", t => t.SuppliesId)
                .Index(t => t.BooksId)
                .Index(t => t.SuppliesId);
            
            CreateTable(
                "dbo.Supplies",
                c => new
                    {
                        SuppliesID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Value = c.String(),
                        Number = c.Int(nullable: false),
                        Available = c.Boolean(nullable: false),
                        ClassRoom = c.String(),
                    })
                .PrimaryKey(t => t.SuppliesID);
            
            CreateTable(
                "dbo.CheckOutBooks",
                c => new
                    {
                        CheckOutBookID = c.Int(nullable: false, identity: true),
                        StudentsID = c.Int(nullable: false),
                        BooksID = c.Int(nullable: false),
                        DepartmentID = c.Int(nullable: false),
                        DueDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ReturnedBook = c.Boolean(nullable: false),
                        ReturnedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        CheckedOutDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.CheckOutBookID)
                .ForeignKey("dbo.Books", t => t.BooksID, cascadeDelete: true)
                .ForeignKey("dbo.Departments", t => t.DepartmentID, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.StudentsID, cascadeDelete: true)
                .Index(t => t.StudentsID)
                .Index(t => t.BooksID)
                .Index(t => t.DepartmentID);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        DepartmentID = c.Int(nullable: false, identity: true),
                        DepName = c.String(nullable: false),
                        Location = c.String(),
                        Telephone = c.String(),
                        Email = c.String(),
                        WebAddress = c.String(),
                        DepChair = c.String(),
                        DepChairEmail = c.String(),
                    })
                .PrimaryKey(t => t.DepartmentID);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        StudentsID = c.Int(nullable: false, identity: true),
                        UVUID = c.String(nullable: false),
                        StudentName = c.String(nullable: false),
                        StudentEmail = c.String(),
                        StudentPhone = c.String(),
                        HasCheckedOutBooks = c.Boolean(nullable: false),
                        HasCheckedOutSupplies = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.StudentsID);
            
            CreateTable(
                "dbo.CheckOutSupplies",
                c => new
                    {
                        CheckOutSuppliesID = c.Int(nullable: false, identity: true),
                        StudentsID = c.Int(nullable: false),
                        SuppliesID = c.Int(nullable: false),
                        DepartmentID = c.Int(nullable: false),
                        DueDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ReturnedSupply = c.Boolean(nullable: false),
                        ReturnedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        CheckedOutDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.CheckOutSuppliesID)
                .ForeignKey("dbo.Departments", t => t.DepartmentID, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.StudentsID, cascadeDelete: true)
                .ForeignKey("dbo.Supplies", t => t.SuppliesID, cascadeDelete: true)
                .Index(t => t.StudentsID)
                .Index(t => t.SuppliesID)
                .Index(t => t.DepartmentID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        UserRolesId = c.Int(nullable: false, identity: true),
                        UserRoleName = c.String(),
                    })
                .PrimaryKey(t => t.UserRolesId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(),
                        BirthDate = c.DateTime(nullable: false),
                        UserRoleId = c.Int(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        UserRoles_UserRolesId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserRoles", t => t.UserRoles_UserRolesId)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.UserRoles_UserRolesId);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "UserRoles_UserRolesId", "dbo.UserRoles");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.CheckOutSupplies", "SuppliesID", "dbo.Supplies");
            DropForeignKey("dbo.CheckOutSupplies", "StudentsID", "dbo.Students");
            DropForeignKey("dbo.CheckOutSupplies", "DepartmentID", "dbo.Departments");
            DropForeignKey("dbo.CheckOutBooks", "StudentsID", "dbo.Students");
            DropForeignKey("dbo.CheckOutBooks", "DepartmentID", "dbo.Departments");
            DropForeignKey("dbo.CheckOutBooks", "BooksID", "dbo.Books");
            DropForeignKey("dbo.Files", "SuppliesId", "dbo.Supplies");
            DropForeignKey("dbo.Files", "BooksId", "dbo.Books");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "UserRoles_UserRolesId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.CheckOutSupplies", new[] { "DepartmentID" });
            DropIndex("dbo.CheckOutSupplies", new[] { "SuppliesID" });
            DropIndex("dbo.CheckOutSupplies", new[] { "StudentsID" });
            DropIndex("dbo.CheckOutBooks", new[] { "DepartmentID" });
            DropIndex("dbo.CheckOutBooks", new[] { "BooksID" });
            DropIndex("dbo.CheckOutBooks", new[] { "StudentsID" });
            DropIndex("dbo.Files", new[] { "SuppliesId" });
            DropIndex("dbo.Files", new[] { "BooksId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.UserRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.CheckOutSupplies");
            DropTable("dbo.Students");
            DropTable("dbo.Departments");
            DropTable("dbo.CheckOutBooks");
            DropTable("dbo.Supplies");
            DropTable("dbo.Files");
            DropTable("dbo.Books");
        }
    }
}
