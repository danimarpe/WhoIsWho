namespace WhoIsWho.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NamGame : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Games", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Games", "Name");
        }
    }
}
