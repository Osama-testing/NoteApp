namespace NoteApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNotesTbl : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NotesModelNotesTbl",
                c => new
                    {
                        NoteId = c.Int(nullable: false, identity: true),
                        List_Id = c.Int(nullable: false),
                        NoteDesciption = c.String(unicode: false),
                        NoteFile = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.NoteId)
                .ForeignKey("dbo.ListTbl", t => t.List_Id, cascadeDelete: true)
                .Index(t => t.List_Id);
            
            DropColumn("dbo.ListTbl", "ListDescription");
            DropColumn("dbo.ListTbl", "FilePath");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ListTbl", "FilePath", c => c.String());
            AddColumn("dbo.ListTbl", "ListDescription", c => c.String(unicode: false));
            DropForeignKey("dbo.NotesModelNotesTbl", "List_Id", "dbo.ListTbl");
            DropIndex("dbo.NotesModelNotesTbl", new[] { "List_Id" });
            DropTable("dbo.NotesModelNotesTbl");
        }
    }
}
