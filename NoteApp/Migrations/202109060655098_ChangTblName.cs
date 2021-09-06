namespace NoteApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangTblName : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.NotesModelNotesTbl", newName: "NotesTbl");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.NotesTbl", newName: "NotesModelNotesTbl");
        }
    }
}
