using System;
using FluentMigrator;

namespace CaseManagement.Migrations.DefaultDB
{
    [Migration(20160810121100)]

    public class DefaultDB_20160810_121100_ActivityRequestRelation : Migration
    {
        public override void Up()
        {
            Alter.Table("ActivityRequest").InSchema("dbo")
               .AddColumn("ActivityID").AsInt32().Nullable()
                   .ForeignKey("ActivityID", "dbo", "Activity", "ID");

            Alter.Table("ActivityRequest").InSchema("dbo")
               .AddColumn("ProvinceID").AsInt32().Nullable()
                   .ForeignKey("ProvinceID2", "dbo", "Province", "ID");

            Alter.Table("ActivityRequest").InSchema("dbo")
               .AddColumn("CycleID").AsInt32().Nullable()
                   .ForeignKey("CycleID", "dbo", "Cycle", "ID");

            Alter.Table("ActivityRequest").InSchema("dbo")
               .AddColumn("CustomerEffectID").AsInt32().Nullable()
                   .ForeignKey("CustomerEffectID", "dbo", "CustomerEffect", "ID");

            Alter.Table("ActivityRequest").InSchema("dbo")
               .AddColumn("OrganizationEffectID").AsInt32().Nullable()
                   .ForeignKey("OrganizationEffectID", "dbo", "OrganizationEffect", "ID");

            Alter.Table("ActivityRequest").InSchema("dbo")
               .AddColumn("IncomeFlowID").AsInt32().Nullable()
                   .ForeignKey("IncomeFlowID", "dbo", "IncomeFlow", "ID");

            Alter.Table("ActivityRequest").InSchema("dbo")
               .AddColumn("RiskLevelID").AsInt32().Nullable()
                   .ForeignKey("RiskLevelID", "dbo", "RiskLevel", "ID");

            Alter.Table("ActivityRequestComment").InSchema("dbo")
               .AddColumn("ActivityRequestID").AsInt32().Nullable()
                   .ForeignKey("ActivityRequestID", "dbo", "ActivityRequest", "ID");
        }

        public override void Down()
        {
        }
    }
}