namespace NoteApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddValidationListTbl : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tblLists", "Name", c => c.String(nullable: false, maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tblLists", "Name", c => c.String(nullable: false));
        }
    }
}
