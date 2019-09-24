namespace MarketShow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Urun", newName: "Urunler");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Urunler", newName: "Urun");
        }
    }
}
