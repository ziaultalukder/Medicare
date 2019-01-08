namespace Medicare.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DoctorTableTitlefielAdedd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Doctors", "Title", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Doctors", "Title");
        }
    }
}
