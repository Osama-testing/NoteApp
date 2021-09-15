namespace NoteApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChngListClmnName : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.tblLists", name: "Id", newName: "UserId");
            RenameIndex(table: "dbo.tblLists", name: "IX_Id", newName: "IX_UserId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.tblLists", name: "IX_UserId", newName: "IX_Id");
            RenameColumn(table: "dbo.tblLists", name: "UserId", newName: "Id");
        }
    }
}
