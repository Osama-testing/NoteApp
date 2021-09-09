namespace NoteApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangCoomnName : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TagTbl", "List_Id", "dbo.ListTbl");
            DropIndex("dbo.TagTbl", new[] { "List_Id" });
            DropColumn("dbo.TagTbl", "List_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TagTbl", "List_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.TagTbl", "List_Id");
            AddForeignKey("dbo.TagTbl", "List_Id", "dbo.ListTbl", "List_Id", cascadeDelete: true);
        }
    }
}
