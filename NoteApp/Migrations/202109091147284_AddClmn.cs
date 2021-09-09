namespace NoteApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddClmn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NotesTbl", "List_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.NotesTbl", "List_Id");
            AddForeignKey("dbo.NotesTbl", "List_Id", "dbo.ListTbl", "List_Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NotesTbl", "List_Id", "dbo.ListTbl");
            DropIndex("dbo.NotesTbl", new[] { "List_Id" });
            DropColumn("dbo.NotesTbl", "List_Id");
        }
    }
}
