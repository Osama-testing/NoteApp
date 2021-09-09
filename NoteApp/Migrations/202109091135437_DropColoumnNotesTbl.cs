namespace NoteApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropColoumnNotesTbl : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.NotesTbl", "List_Id", "dbo.ListTbl");
            DropIndex("dbo.NotesTbl", new[] { "List_Id" });
            DropColumn("dbo.NotesTbl", "List_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.NotesTbl", "List_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.NotesTbl", "List_Id");
            AddForeignKey("dbo.NotesTbl", "List_Id", "dbo.ListTbl", "List_Id", cascadeDelete: true);
        }
    }
}
