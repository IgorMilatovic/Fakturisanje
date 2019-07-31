namespace Fakturisanje.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class skupKolicinazaUnos : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FakturaViewModels", "skupStavkizaUnos", c => c.String());
            AddColumn("dbo.FakturaViewModels", "skupKolicinazaUnos", c => c.String());
            DropColumn("dbo.Stavka", "skupStavkizaUnos");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Stavka", "skupStavkizaUnos", c => c.String());
            DropColumn("dbo.FakturaViewModels", "skupKolicinazaUnos");
            DropColumn("dbo.FakturaViewModels", "skupStavkizaUnos");
        }
    }
}
