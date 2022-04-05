namespace AspNetIdentity.WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Int : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MetaAreas", "AreaAddress", "dbo.Addresses");
            DropIndex("dbo.MetaAreas", new[] { "AreaAddress" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.MetaAreas", "AreaAddress");
            AddForeignKey("dbo.MetaAreas", "AreaAddress", "dbo.Addresses", "AddressId", cascadeDelete: true);
        }
    }
}
