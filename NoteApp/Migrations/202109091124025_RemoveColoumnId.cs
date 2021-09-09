namespace NoteApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveColoumnId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.NotesTbl", "Id", "dbo.AspNetUsers");
            DropIndex("dbo.NotesTbl", new[] { "Id" });
            DropColumn("dbo.NotesTbl", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.NotesTbl", "Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.NotesTbl", "Id");
            AddForeignKey("dbo.NotesTbl", "Id", "dbo.AspNetUsers", "Id");
        }
    }
}
