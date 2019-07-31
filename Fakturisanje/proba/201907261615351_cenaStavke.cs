namespace Fakturisanje.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cenaStavke : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Stavka", "CenaStavke", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Stavka", "CenaStavke", c => c.String(nullable: false, maxLength: 10));
        }
    }
}
