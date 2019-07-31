namespace Fakturisanje.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pomocnaKolicinaUStavkaModelu : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Stavka", "kolicina", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Stavka", "kolicina");
        }
    }
}
