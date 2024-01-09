using System;
using FluentMigrator;

namespace CaseManagement.Migrations.DefaultDB
{
    [Migration(20160906090000)]
    public class DefaultDB_20160906_090000_SwitchRelation : Migration
    {
        public override void Up()
        {
            Create.Table("SwitchProvince").InSchema("dbo")
                        .WithColumn("ID").AsInt32().Identity().PrimaryKey().NotNullable()
                        .WithColumn("ProvinceID").AsInt32().NotNullable().ForeignKey("Switch_Province", "dbo", "Province", "ID")
                        .WithColumn("SwitchID").AsInt32().NotNullable().ForeignKey("SwitchID", "dbo", "Switch", "ID")
                        .WithColumn("CreatedUserID").AsInt32().Nullable()
                        .WithColumn("CreatedDate").AsDateTime().Nullable()
                        .WithColumn("ModifiedUserID").AsInt32().Nullable()
                        .WithColumn("ModifiedDate").AsDateTime().Nullable()
                        .WithColumn("IsDeleted").AsBoolean().Nullable()
                        .WithColumn("DeletedUserID").AsInt32().Nullable()
                        .WithColumn("DeletedDate").AsDateTime().Nullable();

            Create.Table("SwitchTransitProvince").InSchema("dbo")
                        .WithColumn("ID").AsInt32().Identity().PrimaryKey().NotNullable()
                        .WithColumn("ProvinceID").AsInt32().NotNullable().ForeignKey("SwitchTransit_Province", "dbo", "Province", "ID")
                        .WithColumn("SwitchTransitID").AsInt32().NotNullable().ForeignKey("SwitchTransitID", "dbo", "SwitchTransit", "ID")
                        .WithColumn("CreatedUserID").AsInt32().Nullable()
                        .WithColumn("CreatedDate").AsDateTime().Nullable()
                        .WithColumn("ModifiedUserID").AsInt32().Nullable()
                        .WithColumn("ModifiedDate").AsDateTime().Nullable()
                        .WithColumn("IsDeleted").AsBoolean().Nullable()
                        .WithColumn("DeletedUserID").AsInt32().Nullable()
                        .WithColumn("DeletedDate").AsDateTime().Nullable();

            Create.Table("SwitchDSLAMProvince").InSchema("dbo")
                        .WithColumn("ID").AsInt32().Identity().PrimaryKey().NotNullable()
                        .WithColumn("ProvinceID").AsInt32().NotNullable().ForeignKey("SwitchDSLAM_Province", "dbo", "Province", "ID")
                        .WithColumn("SwitchDSLAMID").AsInt32().NotNullable().ForeignKey("SwitchDSLAMID", "dbo", "SwitchDSLAM", "ID")
                        .WithColumn("CreatedUserID").AsInt32().Nullable()
                        .WithColumn("CreatedDate").AsDateTime().Nullable()
                        .WithColumn("ModifiedUserID").AsInt32().Nullable()
                        .WithColumn("ModifiedDate").AsDateTime().Nullable()
                        .WithColumn("IsDeleted").AsBoolean().Nullable()
                        .WithColumn("DeletedUserID").AsInt32().Nullable()
                        .WithColumn("DeletedDate").AsDateTime().Nullable();
        }

        public override void Down()
        {
        }
    }
}