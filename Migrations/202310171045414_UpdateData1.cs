namespace WebBanHang.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateData1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_Adv", "ModifiedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.tb_Category", "ModifiedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.tb_New", "ModifiedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.tb_Post", "ModifiedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.tb_Contact", "ModifiedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.tb_Order", "ModifiedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.tb_Product", "ModifiedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.tb_ProductCategory", "ModifiedDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.tb_Adv", "CodifiedDate");
            DropColumn("dbo.tb_Category", "CodifiedDate");
            DropColumn("dbo.tb_New", "CodifiedDate");
            DropColumn("dbo.tb_Post", "CodifiedDate");
            DropColumn("dbo.tb_Contact", "CodifiedDate");
            DropColumn("dbo.tb_Order", "CodifiedDate");
            DropColumn("dbo.tb_Product", "CodifiedDate");
            DropColumn("dbo.tb_ProductCategory", "CodifiedDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tb_ProductCategory", "CodifiedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.tb_Product", "CodifiedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.tb_Order", "CodifiedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.tb_Contact", "CodifiedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.tb_Post", "CodifiedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.tb_New", "CodifiedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.tb_Category", "CodifiedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.tb_Adv", "CodifiedDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.tb_ProductCategory", "ModifiedDate");
            DropColumn("dbo.tb_Product", "ModifiedDate");
            DropColumn("dbo.tb_Order", "ModifiedDate");
            DropColumn("dbo.tb_Contact", "ModifiedDate");
            DropColumn("dbo.tb_Post", "ModifiedDate");
            DropColumn("dbo.tb_New", "ModifiedDate");
            DropColumn("dbo.tb_Category", "ModifiedDate");
            DropColumn("dbo.tb_Adv", "ModifiedDate");
        }
    }
}
