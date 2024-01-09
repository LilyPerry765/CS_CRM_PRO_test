using System;
using FluentMigrator;

namespace CaseManagement.Migrations.DefaultDB
{
    [Migration(20160827120000)]
    public class DefaultDB_20160827_120000_WorkFlowInfo : Migration
    {
        public override void Up()
        {
            Create.Table("WorkFlowStep").InSchema("dbo")
                        .WithColumn("ID").AsInt32().Identity().PrimaryKey().NotNullable()
                        .WithColumn("Name").AsString(1000).NotNullable()
                        .WithColumn("CreatedUserID").AsInt32().NotNullable()
                        .WithColumn("CreatedDate").AsDateTime().NotNullable()
                        .WithColumn("ModifiedUserID").AsInt32().NotNullable()
                        .WithColumn("ModifiedDate").AsDateTime().NotNullable()
                        .WithColumn("IsDeleted").AsBoolean().NotNullable()
                        .WithColumn("DeletedUserID").AsInt32().Nullable()
                        .WithColumn("DeletedDate").AsDateTime().Nullable();

            Create.Table("WorkFlowStatus").InSchema("dbo")
                        .WithColumn("ID").AsInt32().Identity().PrimaryKey().NotNullable()
                        .WithColumn("Name").AsString(1000).NotNullable()
                        .WithColumn("CreatedUserID").AsInt32().NotNullable()
                        .WithColumn("CreatedDate").AsDateTime().NotNullable()
                        .WithColumn("ModifiedUserID").AsInt32().NotNullable()
                        .WithColumn("ModifiedDate").AsDateTime().NotNullable()
                        .WithColumn("IsDeleted").AsBoolean().NotNullable()
                        .WithColumn("DeletedUserID").AsInt32().Nullable()
                        .WithColumn("DeletedDate").AsDateTime().Nullable();

            Create.Table("WorkFlowStatusType").InSchema("dbo")
                        .WithColumn("ID").AsInt32().Identity().PrimaryKey().NotNullable()
                        .WithColumn("Name").AsString(1000).NotNullable()
                        .WithColumn("CreatedUserID").AsInt32().NotNullable()
                        .WithColumn("CreatedDate").AsDateTime().NotNullable()
                        .WithColumn("ModifiedUserID").AsInt32().NotNullable()
                        .WithColumn("ModifiedDate").AsDateTime().NotNullable()
                        .WithColumn("IsDeleted").AsBoolean().NotNullable()
                        .WithColumn("DeletedUserID").AsInt32().Nullable()
                        .WithColumn("DeletedDate").AsDateTime().Nullable();

            Create.Table("WorkFlowRule").InSchema("dbo")
                        .WithColumn("ID").AsInt32().Identity().PrimaryKey().NotNullable()
                        .WithColumn("CreatedUserID").AsInt32().NotNullable()
                        .WithColumn("CreatedDate").AsDateTime().NotNullable()
                        .WithColumn("ModifiedUserID").AsInt32().NotNullable()
                        .WithColumn("ModifiedDate").AsDateTime().NotNullable()
                        .WithColumn("IsDeleted").AsBoolean().NotNullable()
                        .WithColumn("DeletedUserID").AsInt32().Nullable()
                        .WithColumn("DeletedDate").AsDateTime().Nullable();

            Create.Table("WorkFlowAction").InSchema("dbo")
                        .WithColumn("ID").AsInt32().Identity().PrimaryKey().NotNullable()
                        .WithColumn("Name").AsString(1000).NotNullable()
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