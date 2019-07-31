namespace Fakturisanje.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class proba1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FakturaViewModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Faktura_IdFakture = c.String(maxLength: 10),
                        Unosi_IdFakture = c.String(maxLength: 10),
                        Unosi_IdStavke = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Faktura", t => t.Faktura_IdFakture)
                .ForeignKey("dbo.Unosi", t => new { t.Unosi_IdFakture, t.Unosi_IdStavke })
                .Index(t => t.Faktura_IdFakture)
                .Index(t => new { t.Unosi_IdFakture, t.Unosi_IdStavke });
            
            //AddColumn("dbo.Stavka", "FakturaViewModel_Id", c => c.Int());
            //CreateIndex("dbo.Stavka", "FakturaViewModel_Id");
            //AddForeignKey("dbo.Stavka", "FakturaViewModel_Id", "dbo.FakturaViewModels", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FakturaViewModels", new[] { "Unosi_IdFakture", "Unosi_IdStavke" }, "dbo.Unosi");
            DropForeignKey("dbo.Stavka", "FakturaViewModel_Id", "dbo.FakturaViewModels");
            DropForeignKey("dbo.FakturaViewModels", "Faktura_IdFakture", "dbo.Faktura");
            DropIndex("dbo.FakturaViewModels", new[] { "Unosi_IdFakture", "Unosi_IdStavke" });
            DropIndex("dbo.FakturaViewModels", new[] { "Faktura_IdFakture" });
            DropIndex("dbo.Stavka", new[] { "FakturaViewModel_Id" });
            DropColumn("dbo.Stavka", "FakturaViewModel_Id");
            DropTable("dbo.FakturaViewModels");
        }
    }
}
