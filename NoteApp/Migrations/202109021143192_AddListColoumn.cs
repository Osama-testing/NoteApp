namespace NoteApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddListColoumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ListTbl", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.ListTbl", "UpdatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.ListTbl", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ListTbl", "IsActive");
            DropColumn("dbo.ListTbl", "UpdatedDate");
            DropColumn("dbo.ListTbl", "CreatedDate");
        }
    }
}
