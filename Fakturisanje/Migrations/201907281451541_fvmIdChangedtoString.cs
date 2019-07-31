namespace Fakturisanje.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fvmIdChangedtoString : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Stavka", "FakturaViewModel_Id", "dbo.FakturaViewModels");
            DropIndex("dbo.Stavka", new[] { "FakturaViewModel_Id" });
            DropPrimaryKey("dbo.FakturaViewModels");
            AlterColumn("dbo.Stavka", "FakturaViewModel_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.FakturaViewModels", "Id", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.FakturaViewModels", "Id");
            CreateIndex("dbo.Stavka", "FakturaViewModel_Id");
            AddForeignKey("dbo.Stavka", "FakturaViewModel_Id", "dbo.FakturaViewModels", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Stavka", "FakturaViewModel_Id", "dbo.FakturaViewModels");
            DropIndex("dbo.Stavka", new[] { "FakturaViewModel_Id" });
            DropPrimaryKey("dbo.FakturaViewModels");
            AlterColumn("dbo.FakturaViewModels", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Stavka", "FakturaViewModel_Id", c => c.Int());
            AddPrimaryKey("dbo.FakturaViewModels", "Id");
            CreateIndex("dbo.Stavka", "FakturaViewModel_Id");
            AddForeignKey("dbo.Stavka", "FakturaViewModel_Id", "dbo.FakturaViewModels", "Id");
        }
    }
}
