using System;
using FluentMigrator;

namespace CaseManagement.Migrations.DefaultDB
{
    [Migration(20160829110000)]
    public class DefaultDB_20160829_110000_RoleStep : Migration
    {
        public override void Up()
        {
            Create.Table("RoleStep").InSchema("dbo")
                        .WithColumn("ID").AsInt32().Identity().PrimaryKey().NotNullable()
                        .WithColumn("RoleID").AsInt32().NotNullable().ForeignKey("RoleID", "dbo", "Roles", "RoleId")
                        .WithColumn("StepID").AsInt32().NotNullable().ForeignKey("StepID1", "dbo", "WorkFlowStep", "ID")
                        .WithColumn("CreatedUserID").AsInt32().NotNullable()
                        .WithColumn("CreatedDate").AsDateTime().NotNullable()
                        .WithColumn("ModifiedUserID").AsInt32().NotNullable()
                        .WithColumn("ModifiedDate").AsDateTime().NotNullable()
                        .WithColumn("IsDeleted").AsBoolean().NotNullable()
                        .WithColumn("DeletedUserID").AsInt32().Nullable()
                        .WithColumn("DeletedDate").AsDateTime().Nullable();
        }

        public override void Down()
        {
        }
    }
}