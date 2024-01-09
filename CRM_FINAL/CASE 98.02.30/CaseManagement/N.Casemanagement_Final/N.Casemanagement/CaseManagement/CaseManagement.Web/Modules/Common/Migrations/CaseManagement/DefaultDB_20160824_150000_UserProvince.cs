using System;
using FluentMigrator;

namespace CaseManagement.Migrations.DefaultDB
{
    [Migration(20160824150000)]
    public class DefaultDB_20160824_150000_UserProvince : Migration
    {
        public override void Up()
        {
            Create.Table("UserProvince").InSchema("dbo")
                            .WithColumn("ID").AsInt32().Identity().PrimaryKey().NotNullable()
                            .WithColumn("UserID").AsInt32().NotNullable().ForeignKey("UserID4", "dbo", "Users", "UserId")
                            .WithColumn("ProvinceID").AsInt32().NotNullable().ForeignKey("ProvinceID4", "dbo", "Province", "ID")
                            .WithColumn("CreatedUserID").AsInt32().NotNullable()
                            .WithColumn("CreatedDate").AsDateTime().NotNullable()
                            .WithColumn("IsDeleted").AsBoolean().NotNullable()
                            .WithColumn("DeletedUserID").AsInt32().Nullable()
                            .WithColumn("DeletedDate").AsDateTime().Nullable()
                            .WithColumn("EndDate").AsDateTime().Nullable();
        }

        public override void Down()
        {
        }
    }
}