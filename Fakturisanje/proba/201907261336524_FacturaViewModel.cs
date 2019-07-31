namespace Fakturisanje.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FacturaViewModel : DbMigration
    {
        public override void Up()
        {
            
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.FakturaViewModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Faktura_IdFakture = c.String(maxLength: 10),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Stavka", "FakturaViewModel_Id", c => c.Int());
            AddColumn("dbo.Unosi", "FakturaViewModel_Id", c => c.Int());
            CreateIndex("dbo.FakturaViewModels", "Faktura_IdFakture");
            CreateIndex("dbo.Stavka", "FakturaViewModel_Id");
            CreateIndex("dbo.Unosi", "FakturaViewModel_Id");
            AddForeignKey("dbo.Unosi", "FakturaViewModel_Id", "dbo.FakturaViewModels", "Id");
            AddForeignKey("dbo.Stavka", "FakturaViewModel_Id", "dbo.FakturaViewModels", "Id");
            AddForeignKey("dbo.FakturaViewModels", "Faktura_IdFakture", "dbo.Faktura", "IdFakture");
        }
    }
}
