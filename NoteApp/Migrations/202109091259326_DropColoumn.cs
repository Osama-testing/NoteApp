namespace NoteApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropColoumn : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.NoteTag", "NoteId", "dbo.NotesTbl");
            DropIndex("dbo.NoteTag", new[] { "NoteId" });
            DropColumn("dbo.NoteTag", "NoteId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.NoteTag", "NoteId", c => c.Int(nullable: false));
            CreateIndex("dbo.NoteTag", "NoteId");
            AddForeignKey("dbo.NoteTag", "NoteId", "dbo.NotesTbl", "NoteId", cascadeDelete: true);
        }
    }
}
