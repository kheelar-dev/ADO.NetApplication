namespace ADO.NetApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initital : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Staffs",
                c => new
                    {
                        StaffID = c.Int(nullable: false, identity: true),
                        StaffName = c.String(maxLength: 50),
                        JobTitle = c.String(maxLength: 50, unicode: false),
                        Salary = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedUser = c.String(maxLength: 50, unicode: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        UpdatedUser = c.String(maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.StaffID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Staffs");
        }
    }
}
