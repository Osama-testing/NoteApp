namespace NoteApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ClmnNameChnge : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblLists", "Name", c => c.String());
            DropColumn("dbo.tblLists", "ListName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tblLists", "ListName", c => c.String());
            DropColumn("dbo.tblLists", "Name");
        }
    }
}
