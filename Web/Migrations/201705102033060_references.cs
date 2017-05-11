namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class references : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Leaves", "State_LeaveStateID", "dbo.LeaveStates");
            DropIndex("dbo.Leaves", new[] { "State_LeaveStateID" });
            RenameColumn(table: "dbo.Leaves", name: "State_LeaveStateID", newName: "LeaveStateID");
            RenameColumn(table: "dbo.Leaves", name: "User_Id", newName: "UserID");
            RenameIndex(table: "dbo.Leaves", name: "IX_User_Id", newName: "IX_UserID");
            AlterColumn("dbo.Leaves", "LeaveStateID", c => c.Int(nullable: false));
            CreateIndex("dbo.Leaves", "LeaveStateID");
            AddForeignKey("dbo.Leaves", "LeaveStateID", "dbo.LeaveStates", "LeaveStateID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Leaves", "LeaveStateID", "dbo.LeaveStates");
            DropIndex("dbo.Leaves", new[] { "LeaveStateID" });
            AlterColumn("dbo.Leaves", "LeaveStateID", c => c.Int());
            RenameIndex(table: "dbo.Leaves", name: "IX_UserID", newName: "IX_User_Id");
            RenameColumn(table: "dbo.Leaves", name: "UserID", newName: "User_Id");
            RenameColumn(table: "dbo.Leaves", name: "LeaveStateID", newName: "State_LeaveStateID");
            CreateIndex("dbo.Leaves", "State_LeaveStateID");
            AddForeignKey("dbo.Leaves", "State_LeaveStateID", "dbo.LeaveStates", "LeaveStateID");
        }
    }
}
