namespace RE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Intial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        ssss = c.String(),
                        CustomerName = c.String(),
                        PMName = c.String(),
                        TLName = c.String(),
                        JEName = c.String(),
                        Name = c.String(),
                        Description = c.String(nullable: false),
                        Price = c.Single(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Code = c.Int(nullable: false),
                        Comment = c.String(),
                        Comments = c.String(),
                        Approval = c.Boolean(nullable: false),
                        Submit = c.Boolean(nullable: false),
                        Report = c.Boolean(nullable: false),
                        PMAccept = c.Boolean(nullable: false),
                        TLAccept = c.Boolean(nullable: false),
                        JEAccept = c.Boolean(nullable: false),
                        Assign = c.Boolean(nullable: false),
                        Delivered = c.Boolean(nullable: false),
                        Feedback = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        UserName = c.String(nullable: false),
                        Password = c.String(nullable: false, maxLength: 50),
                        Mobile = c.Long(nullable: false),
                        Email = c.String(),
                        JobDescription = c.String(nullable: false),
                        JobRole = c.String(),
                        Photo = c.Binary(),
                        Project_ID = c.Long(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Projects", t => t.Project_ID)
                .Index(t => t.Project_ID);
            
            CreateTable(
                "dbo.ProjectUsers",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Projects_ID = c.Long(),
                        Usrs_ID = c.Long(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Projects", t => t.Projects_ID)
                .ForeignKey("dbo.Users", t => t.Usrs_ID)
                .Index(t => t.Projects_ID)
                .Index(t => t.Usrs_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProjectUsers", "Usrs_ID", "dbo.Users");
            DropForeignKey("dbo.ProjectUsers", "Projects_ID", "dbo.Projects");
            DropForeignKey("dbo.Users", "Project_ID", "dbo.Projects");
            DropIndex("dbo.ProjectUsers", new[] { "Usrs_ID" });
            DropIndex("dbo.ProjectUsers", new[] { "Projects_ID" });
            DropIndex("dbo.Users", new[] { "Project_ID" });
            DropTable("dbo.ProjectUsers");
            DropTable("dbo.Users");
            DropTable("dbo.Projects");
        }
    }
}
