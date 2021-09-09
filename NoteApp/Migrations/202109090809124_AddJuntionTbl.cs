namespace NoteApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddJuntionTbl : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NoteTag",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NoteId = c.Int(nullable: false),
                        TagId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.NotesTbl", t => t.NoteId, cascadeDelete: false)
                .ForeignKey("dbo.TagTbl", t => t.TagId, cascadeDelete: false)
                .Index(t => t.NoteId)
                .Index(t => t.TagId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NoteTag", "TagId", "dbo.TagTbl");
            DropForeignKey("dbo.NoteTag", "NoteId", "dbo.NotesTbl");
            DropIndex("dbo.NoteTag", new[] { "TagId" });
            DropIndex("dbo.NoteTag", new[] { "NoteId" });
            DropTable("dbo.NoteTag");
        }
    }
}
