namespace NoteApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddListTbl : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ListTbl",
                c => new
                    {
                        List_Id = c.Int(nullable: false, identity: true),
                        Id = c.String(maxLength: 128),
                        ListName = c.String(),
                        ListDescription = c.String(unicode: false),
                        File = c.String(),
                    })
                .PrimaryKey(t => t.List_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ListTbl", "Id", "dbo.AspNetUsers");
            DropIndex("dbo.ListTbl", new[] { "Id" });
            DropTable("dbo.ListTbl");
        }
    }
}
