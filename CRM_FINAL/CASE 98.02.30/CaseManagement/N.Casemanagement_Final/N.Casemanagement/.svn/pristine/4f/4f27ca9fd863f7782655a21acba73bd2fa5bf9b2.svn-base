using System;
using FluentMigrator;

namespace CaseManagement.Migrations.DefaultDB
{
    [Migration(20160913160000)]
    public class DefaultDB_20160913_160000_ActivityReport : Migration
    {
        public override void Up()
        {
            Create.Table("ActivityReport").InSchema("dbo")
                        .WithColumn("ID").AsInt32().Identity().PrimaryKey().NotNullable()
                        .WithColumn("RequestID").AsInt32().Nullable().ForeignKey("Request1", "dbo", "ActivityRequest", "ID")
                        .WithColumn("ProvinceID").AsInt32().NotNullable().ForeignKey("RequestReport_province", "dbo", "Province", "ID")
                        .WithColumn("ActivityID").AsInt32().NotNullable().ForeignKey("RequestReport_Activity", "dbo", "Activity", "ID")
                        .WithColumn("LeakCost").AsInt64().Nullable()
                        .WithColumn("ConfirmCost").AsInt64().Nullable()
                        .WithColumn("ProgramCost").AsInt64().Nullable()
                        .WithColumn("Percent").AsString(50).Nullable()
                        .WithColumn("CreatedUserID").AsInt32().NotNullable()
                        .WithColumn("CreatedDate").AsDateTime().NotNullable()                        
                        .WithColumn("EndDate").AsDateTime().NotNullable();
        }

        public override void Down()
        {
        }
    }
}