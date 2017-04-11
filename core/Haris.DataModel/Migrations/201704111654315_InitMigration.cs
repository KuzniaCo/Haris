namespace Haris.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cubes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CubeAddress = c.String(),
                        CubeType = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Logs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CubeId = c.Int(nullable: false),
                        Value = c.String(),
                        OriginMessage = c.String(),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cubes", t => t.CubeId, cascadeDelete: true)
                .Index(t => t.CubeId);
            
            CreateTable(
                "dbo.WebHooks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CubeId = c.Int(nullable: false),
                        Name = c.String(),
                        Url = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cubes", t => t.CubeId, cascadeDelete: true)
                .Index(t => t.CubeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WebHooks", "CubeId", "dbo.Cubes");
            DropForeignKey("dbo.Logs", "CubeId", "dbo.Cubes");
            DropIndex("dbo.WebHooks", new[] { "CubeId" });
            DropIndex("dbo.Logs", new[] { "CubeId" });
            DropTable("dbo.WebHooks");
            DropTable("dbo.Logs");
            DropTable("dbo.Cubes");
        }
    }
}
