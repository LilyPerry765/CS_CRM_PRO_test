using System;
using FluentMigrator;

namespace CaseManagement.Migrations.DefaultDB
{
    [Migration(20160904120000)]
    public class DefaultDB_20160904_120000_RequestLog : Migration
    {
        public override void Up()
        {
            Create.Table("ActivityRequestLog").InSchema("dbo")
                            .WithColumn("ID").AsInt32().Identity().PrimaryKey().NotNullable()
                            .WithColumn("ActivityRequestID").AsInt32().NotNullable().ForeignKey("ActivityRequestID1", "dbo", "ActivityRequest", "ID")
                            .WithColumn("StatusID").AsInt32().NotNullable().ForeignKey("StatusID5", "dbo", "WorkFlowStatus", "ID")
                            .WithColumn("ActionID").AsInt32().NotNullable()
                            .WithColumn("UserID").AsInt32().NotNullable().ForeignKey("UserID11", "dbo", "Users", "UserId")
                            .WithColumn("InsertDate").AsDateTime().NotNullable();
        }

        public override void Down()
        {
        }
    }
}