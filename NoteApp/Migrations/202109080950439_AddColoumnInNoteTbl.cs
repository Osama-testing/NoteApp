namespace NoteApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColoumnInNoteTbl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NotesTbl", "Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.NotesTbl", "Id");
            AddForeignKey("dbo.NotesTbl", "Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NotesTbl", "Id", "dbo.AspNetUsers");
            DropIndex("dbo.NotesTbl", new[] { "Id" });
            DropColumn("dbo.NotesTbl", "Id");
        }
    }
}
