namespace Fakturisanje.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class skupStavkizaUnos : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Stavka", "skupStavkizaUnos", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Stavka", "skupStavkizaUnos");
        }
    }
}
