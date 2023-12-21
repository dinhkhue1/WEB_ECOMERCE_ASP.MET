namespace WebBanHang.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_db_order : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tb_Order", "TotalAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.tb_Order", "Quantity", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tb_Order", "Quantity", c => c.String());
            AlterColumn("dbo.tb_Order", "TotalAmount", c => c.String());
        }
    }
}
