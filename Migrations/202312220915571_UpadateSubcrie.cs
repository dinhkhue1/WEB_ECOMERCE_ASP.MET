namespace WebBanHang.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpadateSubcrie : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tb_Subcriber", "Email", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tb_Subcriber", "Email", c => c.String());
        }
    }
}
