namespace NoteApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameListColoumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ListTbl", "FilePath", c => c.String());
            DropColumn("dbo.ListTbl", "File");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ListTbl", "File", c => c.String());
            DropColumn("dbo.ListTbl", "FilePath");
        }
    }
}
