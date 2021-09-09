namespace NoteApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColoum : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TagTbl", "NoteId", c => c.Int(nullable: false));
            CreateIndex("dbo.TagTbl", "NoteId");
            AddForeignKey("dbo.TagTbl", "NoteId", "dbo.NotesTbl", "NoteId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TagTbl", "NoteId", "dbo.NotesTbl");
            DropIndex("dbo.TagTbl", new[] { "NoteId" });
            DropColumn("dbo.TagTbl", "NoteId");
        }
    }
}
