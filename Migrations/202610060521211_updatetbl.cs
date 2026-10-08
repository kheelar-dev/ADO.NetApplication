namespace ADO.NetApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatetbl : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Staffs", "Salary", c => c.Int());
            AlterColumn("dbo.Staffs", "CreatedDate", c => c.DateTime());
            AlterColumn("dbo.Staffs", "UpdatedDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Staffs", "UpdatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Staffs", "CreatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Staffs", "Salary", c => c.Int(nullable: false));
        }
    }
}
