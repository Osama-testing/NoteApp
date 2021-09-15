namespace NoteApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TableNameChange : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ListTbl", newName: "tblLists");
            RenameTable(name: "dbo.NotesTbl", newName: "tblNotes");
            RenameTable(name: "dbo.NoteTag", newName: "tblNoteTags");
            RenameTable(name: "dbo.TagTbl", newName: "tblTags");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.tblTags", newName: "TagTbl");
            RenameTable(name: "dbo.tblNoteTags", newName: "NoteTag");
            RenameTable(name: "dbo.tblNotes", newName: "NotesTbl");
            RenameTable(name: "dbo.tblLists", newName: "ListTbl");
        }
    }
}
