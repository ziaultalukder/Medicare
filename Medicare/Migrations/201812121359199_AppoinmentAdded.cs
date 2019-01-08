namespace Medicare.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AppoinmentAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Appoinments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Contact = c.String(),
                        Date = c.DateTime(nullable: false),
                        DoctorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Doctors", t => t.DoctorId, cascadeDelete: true)
                .Index(t => t.DoctorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Appoinments", "DoctorId", "dbo.Doctors");
            DropIndex("dbo.Appoinments", new[] { "DoctorId" });
            DropTable("dbo.Appoinments");
        }
    }
}
