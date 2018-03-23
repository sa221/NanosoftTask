namespace NanoSoftExam.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Attendences",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StudentId = c.Int(nullable: false),
                        Status = c.String(),
                        Date = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: true)
                .Index(t => t.StudentId);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        StudentId = c.Int(nullable: false, identity: true),
                        LastName = c.String(),
                        FirstName = c.String(),
                        FatherName = c.String(),
                        Address = c.String(),
                        Image = c.Binary(),
                        ClassName = c.String(),
                        Class_Id = c.Int(),
                    })
                .PrimaryKey(t => t.StudentId)
                .ForeignKey("dbo.Classes", t => t.Class_Id)
                .Index(t => t.Class_Id);
            
            CreateTable(
                "dbo.Classes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClassName = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Attendences", "StudentId", "dbo.Students");
            DropForeignKey("dbo.Students", "Class_Id", "dbo.Classes");
            DropIndex("dbo.Students", new[] { "Class_Id" });
            DropIndex("dbo.Attendences", new[] { "StudentId" });
            DropTable("dbo.Classes");
            DropTable("dbo.Students");
            DropTable("dbo.Attendences");
        }
    }
}
