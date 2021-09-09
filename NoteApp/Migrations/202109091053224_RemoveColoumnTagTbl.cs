namespace NoteApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveColoumnTagTbl : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TagTbl", "NoteId", "dbo.NotesTbl");
            DropIndex("dbo.TagTbl", new[] { "NoteId" });
            DropColumn("dbo.TagTbl", "NoteId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TagTbl", "NoteId", c => c.Int(nullable: false));
            CreateIndex("dbo.TagTbl", "NoteId");
            AddForeignKey("dbo.TagTbl", "NoteId", "dbo.NotesTbl", "NoteId", cascadeDelete: true);
        }
    }
}
