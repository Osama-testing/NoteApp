namespace NoteApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFK : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NoteTag", "NoteId", c => c.Int(nullable: false));
            CreateIndex("dbo.NoteTag", "NoteId");
            AddForeignKey("dbo.NoteTag", "NoteId", "dbo.NotesTbl", "NoteId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NoteTag", "NoteId", "dbo.NotesTbl");
            DropIndex("dbo.NoteTag", new[] { "NoteId" });
            DropColumn("dbo.NoteTag", "NoteId");
        }
    }
}
