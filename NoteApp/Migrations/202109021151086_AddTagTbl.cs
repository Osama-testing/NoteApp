namespace NoteApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTagTbl : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TagTbl",
                c => new
                    {
                        TagId = c.Int(nullable: false, identity: true),
                        List_Id = c.Int(nullable: false),
                        TagItem = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.TagId)
                .ForeignKey("dbo.ListTbl", t => t.List_Id, cascadeDelete: true)
                .Index(t => t.List_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TagTbl", "List_Id", "dbo.ListTbl");
            DropIndex("dbo.TagTbl", new[] { "List_Id" });
            DropTable("dbo.TagTbl");
        }
    }
}
