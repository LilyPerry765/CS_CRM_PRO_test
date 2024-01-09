using System;
using FluentMigrator;

namespace CaseManagement.Migrations.DefaultDB
{
    [Migration(20160809100000)]

    public class DefaultDB_20160809_100000_ActivityRelation : Migration
    {
        public override void Up()
        {
            

            Alter.Table("ActivityRecord").InSchema("dbo")
               .AddColumn("ActivityID").AsInt32().Nullable()
                   .ForeignKey("ActivityID", "dbo", "Activity", "ID");

            Alter.Table("ActivityRecord").InSchema("dbo")
               .AddColumn("ProvinceID").AsInt32().Nullable()
                   .ForeignKey("ProvinceID2", "dbo", "Province", "ID");

            Alter.Table("ActivityRecord").InSchema("dbo")
               .AddColumn("CycleID").AsInt32().Nullable()
                   .ForeignKey("CycleID", "dbo", "Cycle", "ID");



            Alter.Table("ActivityRecordDetail").InSchema("dbo")
               .AddColumn("ActivityRecordID").AsInt32().Nullable()
                   .ForeignKey("ActivityRecordID", "dbo", "ActivityRecord", "ID");

            Alter.Table("ActivityRecordDetail").InSchema("dbo")
               .AddColumn("CustomerEffectID").AsInt32().Nullable()
                   .ForeignKey("CustomerEffectID", "dbo", "CustomerEffect", "ID");

            Alter.Table("ActivityRecordDetail").InSchema("dbo")
               .AddColumn("OrganizationEffectID").AsInt32().Nullable()
                   .ForeignKey("OrganizationEffectID", "dbo", "OrganizationEffect", "ID");

            Alter.Table("ActivityRecordDetail").InSchema("dbo")
               .AddColumn("IncomeFlowID").AsInt32().Nullable()
                   .ForeignKey("IncomeFlowID", "dbo", "IncomeFlow", "ID");

            Alter.Table("ActivityRecordDetail").InSchema("dbo")
               .AddColumn("RiskLevelID").AsInt32().Nullable()
                   .ForeignKey("RiskLevelID", "dbo", "RiskLevel", "ID");



            Alter.Table("ActivityRecordComment").InSchema("dbo")
               .AddColumn("ActivityRecordID").AsInt32().Nullable()
                   .ForeignKey("ActivityRecordID", "dbo", "ActivityRecord", "ID");
        }

        public override void Down()
        {
        }
    }
}