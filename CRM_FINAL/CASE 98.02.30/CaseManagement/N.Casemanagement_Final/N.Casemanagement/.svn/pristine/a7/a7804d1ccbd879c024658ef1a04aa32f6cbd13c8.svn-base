using System;
using FluentMigrator;

namespace CaseManagement.Migrations.DefaultDB
{
    [Migration(20160801150000)]
    public class DefaultDB_20160801_150000_BaseInfo : Migration
    {
        public override void Up()
        {
            Create.Table("CustomerEffect").InSchema("dbo")
                        .WithColumn("ID").AsInt32().Identity().PrimaryKey().NotNullable()
                        .WithColumn("Name").AsString(1000).NotNullable()
                        .WithColumn("CreatedUserID").AsInt32().NotNullable()
                        .WithColumn("CreatedDate").AsDateTime().NotNullable()
                        .WithColumn("ModifiedUserID").AsInt32().NotNullable()
                        .WithColumn("ModifiedDate").AsDateTime().NotNullable()
                        .WithColumn("IsDeleted").AsBoolean().NotNullable()
                        .WithColumn("DeletedUserID").AsInt32().Nullable()
                        .WithColumn("DeletedDate").AsDateTime().Nullable();

            Create.Table("OrganizationEffect").InSchema("dbo")
                        .WithColumn("ID").AsInt32().Identity().PrimaryKey().NotNullable()
                        .WithColumn("Name").AsString(1000).NotNullable()
                        .WithColumn("CreatedUserID").AsInt32().NotNullable()
                        .WithColumn("CreatedDate").AsDateTime().NotNullable()
                        .WithColumn("ModifiedUserID").AsInt32().NotNullable()
                        .WithColumn("ModifiedDate").AsDateTime().NotNullable()
                        .WithColumn("IsDeleted").AsBoolean().NotNullable()
                        .WithColumn("DeletedUserID").AsInt32().Nullable()
                        .WithColumn("DeletedDate").AsDateTime().Nullable();

            Create.Table("RiskLevel").InSchema("dbo")
                        .WithColumn("ID").AsInt32().Identity().PrimaryKey().NotNullable()
                        .WithColumn("Name").AsString(1000).NotNullable()
                        .WithColumn("CreatedUserID").AsInt32().NotNullable()
                        .WithColumn("CreatedDate").AsDateTime().NotNullable()
                        .WithColumn("ModifiedUserID").AsInt32().NotNullable()
                        .WithColumn("ModifiedDate").AsDateTime().NotNullable()
                        .WithColumn("IsDeleted").AsBoolean().NotNullable()
                        .WithColumn("DeletedUserID").AsInt32().Nullable()
                        .WithColumn("DeletedDate").AsDateTime().Nullable();

            Create.Table("IncomeFlow").InSchema("dbo")
                        .WithColumn("ID").AsInt32().Identity().PrimaryKey().NotNullable()
                        .WithColumn("Name").AsString(1000).NotNullable()
                        .WithColumn("CreatedUserID").AsInt32().NotNullable()
                        .WithColumn("CreatedDate").AsDateTime().NotNullable()
                        .WithColumn("ModifiedUserID").AsInt32().NotNullable()
                        .WithColumn("ModifiedDate").AsDateTime().NotNullable()
                        .WithColumn("IsDeleted").AsBoolean().NotNullable()
                        .WithColumn("DeletedUserID").AsInt32().Nullable()
                        .WithColumn("DeletedDate").AsDateTime().Nullable();

            Create.Table("ActivityGroup").InSchema("dbo")
                        .WithColumn("ID").AsInt32().Identity().PrimaryKey().NotNullable()
                        .WithColumn("Name").AsString(1000).NotNullable()
                        .WithColumn("EnglishName").AsString(1000).Nullable()
                        .WithColumn("Code").AsInt32().NotNullable()
                        .WithColumn("CreatedUserID").AsInt32().NotNullable()
                        .WithColumn("CreatedDate").AsDateTime().NotNullable()
                        .WithColumn("ModifiedUserID").AsInt32().NotNullable()
                        .WithColumn("ModifiedDate").AsDateTime().NotNullable()
                        .WithColumn("IsDeleted").AsBoolean().NotNullable()
                        .WithColumn("DeletedUserID").AsInt32().Nullable()
                        .WithColumn("DeletedDate").AsDateTime().Nullable();

            Create.Table("Activity").InSchema("dbo")
                        .WithColumn("ID").AsInt32().Identity().PrimaryKey().NotNullable()
                        .WithColumn("Code").AsInt32().NotNullable()
                        .WithColumn("Name").AsString(1000).NotNullable()
                        .WithColumn("EnglishName").AsString(1000).Nullable()
                        .WithColumn("Objective").AsString(1000).NotNullable()
                        .WithColumn("EnglishObjective").AsString(1000).Nullable()
                        .WithColumn("CreatedUserID").AsInt32().NotNullable()
                        .WithColumn("CreatedDate").AsDateTime().NotNullable()
                        .WithColumn("ModifiedUserID").AsInt32().NotNullable()
                        .WithColumn("ModifiedDate").AsDateTime().NotNullable()
                        .WithColumn("IsDeleted").AsBoolean().NotNullable()
                        .WithColumn("DeletedUserID").AsInt32().Nullable()
                        .WithColumn("DeletedDate").AsDateTime().Nullable()
                        .WithColumn("KeyCheckArea").AsString(1000).Nullable()
                        .WithColumn("DataSource").AsString(1000).Nullable()
                        .WithColumn("Methodology").AsString(1000).Nullable()
                        .WithColumn("KeyFocus").AsString(1000).Nullable()
                        .WithColumn("Action").AsString(1000).Nullable()
                        .WithColumn("KPI").AsString(1000).Nullable();

            Create.Table("PMOLevel").InSchema("dbo")
                        .WithColumn("ID").AsInt32().Identity().PrimaryKey().NotNullable()
                        .WithColumn("Name").AsString(1000).NotNullable()
                        .WithColumn("CreatedUserID").AsInt32().NotNullable()
                        .WithColumn("CreatedDate").AsDateTime().NotNullable()
                        .WithColumn("ModifiedUserID").AsInt32().NotNullable()
                        .WithColumn("ModifiedDate").AsDateTime().NotNullable()
                        .WithColumn("IsDeleted").AsBoolean().NotNullable()
                        .WithColumn("DeletedUserID").AsInt32().Nullable()
                        .WithColumn("DeletedDate").AsDateTime().Nullable();

            Create.Table("Year").InSchema("dbo")
                        .WithColumn("ID").AsInt32().Identity().PrimaryKey().NotNullable()
                        .WithColumn("Year").AsInt32().NotNullable()
                        .WithColumn("CreatedUserID").AsInt32().NotNullable()
                        .WithColumn("CreatedDate").AsDateTime().NotNullable()
                        .WithColumn("ModifiedUserID").AsInt32().NotNullable()
                        .WithColumn("ModifiedDate").AsDateTime().NotNullable()
                        .WithColumn("IsDeleted").AsBoolean().NotNullable()
                        .WithColumn("DeletedUserID").AsInt32().Nullable()
                        .WithColumn("DeletedDate").AsDateTime().Nullable();

            Create.Table("Cycle").InSchema("dbo")
                        .WithColumn("ID").AsInt32().Identity().PrimaryKey().NotNullable()
                        .WithColumn("Year").AsInt32().NotNullable()
                        .WithColumn("Cycle").AsInt32().NotNullable()
                        .WithColumn("CycleName").AsString().NotNullable()
                        .WithColumn("IsEnabled").AsBoolean().NotNullable()
                        .WithColumn("CreatedUserID").AsInt32().NotNullable()
                        .WithColumn("CreatedDate").AsDateTime().NotNullable()
                        .WithColumn("ModifiedUserID").AsInt32().NotNullable()
                        .WithColumn("ModifiedDate").AsDateTime().NotNullable()
                        .WithColumn("IsDeleted").AsBoolean().NotNullable()
                        .WithColumn("DeletedUserID").AsInt32().Nullable()
                        .WithColumn("DeletedDate").AsDateTime().Nullable();

            Create.Table("RepeatTerm").InSchema("dbo")
                        .WithColumn("ID").AsInt32().Identity().PrimaryKey().NotNullable()
                        .WithColumn("Name").AsString(1000).NotNullable()
                        .WithColumn("CreatedUserID").AsInt32().NotNullable()
                        .WithColumn("CreatedDate").AsDateTime().NotNullable()
                        .WithColumn("ModifiedUserID").AsInt32().NotNullable()
                        .WithColumn("ModifiedDate").AsDateTime().NotNullable()
                        .WithColumn("IsDeleted").AsBoolean().NotNullable()
                        .WithColumn("DeletedUserID").AsInt32().Nullable()
                        .WithColumn("DeletedDate").AsDateTime().Nullable();

            Create.Table("Province").InSchema("dbo")
                        .WithColumn("ID").AsInt32().Identity().PrimaryKey().NotNullable()
                        .WithColumn("Name").AsString(1000).NotNullable()
                        .WithColumn("EnglishName").AsString(1000).Nullable()
                        .WithColumn("ManagerName").AsString(1000).NotNullable()
                        .WithColumn("LetterNo").AsString(1000).NotNullable()
                        .WithColumn("InstallDate").AsDateTime().NotNullable()
                        .WithColumn("CreatedUserID").AsInt32().NotNullable()
                        .WithColumn("CreatedDate").AsDateTime().NotNullable()
                        .WithColumn("ModifiedUserID").AsInt32().NotNullable()
                        .WithColumn("ModifiedDate").AsDateTime().NotNullable()
                        .WithColumn("IsDeleted").AsBoolean().NotNullable()
                        .WithColumn("DeletedUserID").AsInt32().Nullable()
                        .WithColumn("DeletedDate").AsDateTime().Nullable();

            Create.Table("ProvinceProgram").InSchema("dbo")
                        .WithColumn("ID").AsInt32().Identity().PrimaryKey().NotNullable()
                        .WithColumn("Program").AsInt64().NotNullable()
                        .WithColumn("ActivityCount").AsInt32().NotNullable()
                        .WithColumn("ActivityNonRepeatCount").AsInt32().NotNullable()
                        .WithColumn("LeakCost").AsInt64().Nullable()
                        .WithColumn("ConfirmCost").AsInt64().Nullable()
                        .WithColumn("Percent").AsString(50).Nullable()
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