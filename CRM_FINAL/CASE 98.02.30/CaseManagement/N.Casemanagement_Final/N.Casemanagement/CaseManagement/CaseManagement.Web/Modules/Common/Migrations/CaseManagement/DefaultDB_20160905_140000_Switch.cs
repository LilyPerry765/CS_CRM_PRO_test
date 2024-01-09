using System;
using FluentMigrator;

namespace CaseManagement.Migrations.DefaultDB
{
    [Migration(20160905140000)]
    public class DefaultDB_20160905_140000_Switch : Migration
    {
        public override void Up()
        {
            Create.Table("Switch").InSchema("dbo")
                        .WithColumn("ID").AsInt32().Identity().PrimaryKey().NotNullable()
                        .WithColumn("Name").AsString(1000).NotNullable()
                        .WithColumn("CreatedUserID").AsInt32().NotNullable()
                        .WithColumn("CreatedDate").AsDateTime().NotNullable()
                        .WithColumn("ModifiedUserID").AsInt32().NotNullable()
                        .WithColumn("ModifiedDate").AsDateTime().NotNullable()
                        .WithColumn("IsDeleted").AsBoolean().NotNullable()
                        .WithColumn("DeletedUserID").AsInt32().Nullable()
                        .WithColumn("DeletedDate").AsDateTime().Nullable();

            Create.Table("SwitchTransit").InSchema("dbo")
                        .WithColumn("ID").AsInt32().Identity().PrimaryKey().NotNullable()
                        .WithColumn("Name").AsString(1000).NotNullable()
                        .WithColumn("CreatedUserID").AsInt32().NotNullable()
                        .WithColumn("CreatedDate").AsDateTime().NotNullable()
                        .WithColumn("ModifiedUserID").AsInt32().NotNullable()
                        .WithColumn("ModifiedDate").AsDateTime().NotNullable()
                        .WithColumn("IsDeleted").AsBoolean().NotNullable()
                        .WithColumn("DeletedUserID").AsInt32().Nullable()
                        .WithColumn("DeletedDate").AsDateTime().Nullable();

            Create.Table("SwitchDSLAM").InSchema("dbo")
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