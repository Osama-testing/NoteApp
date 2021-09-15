namespace NoteApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChngeClmnName : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.tblNotes", name: "Id", newName: "UserId");
            RenameIndex(table: "dbo.tblNotes", name: "IX_Id", newName: "IX_UserId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.tblNotes", name: "IX_UserId", newName: "IX_Id");
            RenameColumn(table: "dbo.tblNotes", name: "UserId", newName: "Id");
        }
    }
}
