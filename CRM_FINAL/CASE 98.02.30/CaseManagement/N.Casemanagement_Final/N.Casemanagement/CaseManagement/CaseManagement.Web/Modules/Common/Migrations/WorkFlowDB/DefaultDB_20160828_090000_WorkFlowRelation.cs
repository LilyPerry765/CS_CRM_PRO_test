using System;
using FluentMigrator;

namespace CaseManagement.Migrations.DefaultDB
{
    [Migration(20160828090000)]
    public class DefaultDB_20160828_090000_WorkFlowRelation : Migration
    {
        public override void Up()
        {
            Alter.Table("WorkFlowStatus").InSchema("dbo")
                .AddColumn("StepID").AsInt32().Nullable()
                    .ForeignKey("StepID", "dbo", "WorkFlowStep", "ID");

            Alter.Table("WorkFlowStatus").InSchema("dbo")
                .AddColumn("StatusTypeID").AsInt32().Nullable()
                    .ForeignKey("StatusTypeID", "dbo", "WorkFlowStatusType", "ID");

            Alter.Table("WorkFlowRule").InSchema("dbo")
                .AddColumn("ActionID").AsInt32().Nullable()
                    .ForeignKey("ActionID", "dbo", "WorkFlowAction", "ID");

            Alter.Table("WorkFlowRule").InSchema("dbo")
                .AddColumn("CurrentStatusID").AsInt32().Nullable()
                    .ForeignKey("CurrentStatusID", "dbo", "WorkFlowStatus", "ID");

            Alter.Table("WorkFlowRule").InSchema("dbo")
                .AddColumn("NextStatusID").AsInt32().Nullable()
                    .ForeignKey("NextStatusID", "dbo", "WorkFlowStatus", "ID");

            Alter.Table("ActivityRequest").InSchema("dbo")
                .AddColumn("StatusID").AsInt32().Nullable()
                    .ForeignKey("StatusID", "dbo", "WorkFlowStatus", "ID");
        }

        public override void Down()
        {
        }
    }
}