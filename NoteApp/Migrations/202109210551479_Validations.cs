namespace NoteApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Validations : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tblTags", "TagItem", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tblTags", "TagItem", c => c.String());
        }
    }
}
