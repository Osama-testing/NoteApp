namespace NoteApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveValidationListtlb : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.tblLists", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.tblLists", new[] { "UserId" });
            AlterColumn("dbo.tblLists", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.tblLists", "UserId");
            AddForeignKey("dbo.tblLists", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblLists", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.tblLists", new[] { "UserId" });
            AlterColumn("dbo.tblLists", "UserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.tblLists", "UserId");
            AddForeignKey("dbo.tblLists", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
