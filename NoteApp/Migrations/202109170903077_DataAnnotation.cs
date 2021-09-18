namespace NoteApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataAnnotation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tblLists", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tblLists", "Name", c => c.String());
        }
    }
}
