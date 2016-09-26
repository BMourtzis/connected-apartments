namespace ConnApsDomain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Level = c.String(nullable: false),
                        Number = c.String(nullable: false),
                        BuildingId = c.Int(nullable: false),
                        TenantsAllowed = c.Int(),
                        FacingDirection = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Buildings", t => t.BuildingId, cascadeDelete: true)
                .Index(t => t.BuildingId);
            
            CreateTable(
                "dbo.Buildings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BuildingName = c.String(nullable: false),
                        Address = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.People",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        DoB = c.DateTime(nullable: false),
                        Phone = c.String(),
                        UserId = c.String(nullable: false),
                        BuildingId = c.Int(),
                        ApartmentId = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Buildings", t => t.BuildingId, cascadeDelete: true)
                .ForeignKey("dbo.Locations", t => t.ApartmentId)
                .Index(t => t.BuildingId)
                .Index(t => t.ApartmentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.People", "ApartmentId", "dbo.Locations");
            DropForeignKey("dbo.People", "BuildingId", "dbo.Buildings");
            DropForeignKey("dbo.Locations", "BuildingId", "dbo.Buildings");
            DropIndex("dbo.People", new[] { "ApartmentId" });
            DropIndex("dbo.People", new[] { "BuildingId" });
            DropIndex("dbo.Locations", new[] { "BuildingId" });
            DropTable("dbo.People");
            DropTable("dbo.Buildings");
            DropTable("dbo.Locations");
        }
    }
}
