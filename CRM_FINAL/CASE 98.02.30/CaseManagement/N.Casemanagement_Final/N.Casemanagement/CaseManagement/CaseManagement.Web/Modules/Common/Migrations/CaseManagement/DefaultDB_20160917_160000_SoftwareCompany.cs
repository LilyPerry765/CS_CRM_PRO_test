using System;
using FluentMigrator;

namespace CaseManagement.Migrations.DefaultDB
{
    [Migration(20160917160000)]
    public class DefaultDB_20160917_160000_SoftwareCompany : Migration
    {
        public override void Up()
        {
            Create.Table("Company").InSchema("dbo")
                         .WithColumn("ID").AsInt32().Identity().PrimaryKey().NotNullable()
                         .WithColumn("Name").AsString(1000).NotNullable()
                         .WithColumn("CreatedUserID").AsInt32().NotNullable()
                         .WithColumn("CreatedDate").AsDateTime().NotNullable()
                         .WithColumn("ModifiedUserID").AsInt32().NotNullable()
                         .WithColumn("ModifiedDate").AsDateTime().NotNullable()
                         .WithColumn("IsDeleted").AsBoolean().NotNullable()
                         .WithColumn("DeletedUserID").AsInt32().Nullable()
                         .WithColumn("DeletedDate").AsDateTime().Nullable();

            Create.Table("Software").InSchema("dbo")
                        .WithColumn("ID").AsInt32().Identity().PrimaryKey().NotNullable()
                        .WithColumn("Name").AsString(1000).NotNullable()
                        .WithColumn("CreatedUserID").AsInt32().NotNullable()
                        .WithColumn("CreatedDate").AsDateTime().NotNullable()
                        .WithColumn("ModifiedUserID").AsInt32().NotNullable()
                        .WithColumn("ModifiedDate").AsDateTime().NotNullable()
                        .WithColumn("IsDeleted").AsBoolean().NotNullable()
                        .WithColumn("DeletedUserID").AsInt32().Nullable()
                        .WithColumn("DeletedDate").AsDateTime().Nullable();

            Create.Table("ProvinceCompanySoftware").InSchema("dbo")
                        .WithColumn("ID").AsInt32().Identity().PrimaryKey().NotNullable()
                        .WithColumn("ProvinveID").AsInt32().NotNullable().ForeignKey("Province_CompanySoftware", "dbo", "Province", "ID")
                        .WithColumn("CompanyID").AsInt32().NotNullable().ForeignKey("CompanyID", "dbo", "Company", "ID")
                        .WithColumn("SoftwareID").AsInt32().NotNullable().ForeignKey("SoftwareID", "dbo", "Software", "ID")
                        .WithColumn("StatusID").AsInt32().NotNullable()
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