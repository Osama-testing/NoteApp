namespace NoteApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColoumnNotes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NotesTbl", "CreatedDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.NotesTbl", "CreatedDate");
        }
    }
}
