namespace NoteApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColoumnInNotestbl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblNotes", "UpdatedDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tblNotes", "UpdatedDate");
        }
    }
}
