using System;
using FluentMigrator;

namespace CaseManagement.Migrations.DefaultDB
{
    [Migration(20160810121000)]
    public class DefaultDB_20160810_120000_ActivityRequest : Migration
    {
        public override void Up()
        {
            Create.Table("ActivityRequest").InSchema("dbo")
                        .WithColumn("ID").AsInt32().Identity().PrimaryKey().NotNullable()
                        .WithColumn("Count").AsInt32().Nullable()
                        .WithColumn("CycleCost").AsInt32().Nullable()
                        .WithColumn("Factor").AsInt32().Nullable()
                        .WithColumn("DelayedCost").AsInt32().Nullable()
                        .WithColumn("YearCost").AsInt32().Nullable()
                        .WithColumn("AccessibleCost").AsInt32().Nullable()
                        .WithColumn("InaccessibleCost").AsInt32().Nullable()
                        .WithColumn("Financial").AsInt32().Nullable()
                        .WithColumn("EventDescription").AsString(1000).Nullable()
                        .WithColumn("MainReason").AsString(1000).Nullable()
                        .WithColumn("CorrectionOperation").AsString(1000).Nullable()
                        .WithColumn("AvoidRepeatingOperation").AsString(1000).Nullable()
                        .WithColumn("CreatedUserID").AsInt32().NotNullable()
                        .WithColumn("CreatedDate").AsDateTime().NotNullable()
                        .WithColumn("ModifiedUserID").AsInt32().NotNullable()
                        .WithColumn("ModifiedDate").AsDateTime().NotNullable()
                        .WithColumn("IsDeleted").AsBoolean().NotNullable()
                        .WithColumn("DeletedUserID").AsInt32().Nullable()
                        .WithColumn("DeletedDate").AsDateTime().Nullable()
                        .WithColumn("EndDate").AsDateTime().Nullable()
                        .WithColumn("LeakDate").AsDateTime().Nullable()
                        .WithColumn("ActionID").AsInt32().Nullable();


            Create.Table("ActivityRequestComment").InSchema("dbo")
                        .WithColumn("ID").AsInt32().Identity().PrimaryKey().NotNullable()
                        .WithColumn("Comment").AsString(10000).NotNullable()
                        .WithColumn("CreatedUserID").AsInt32().NotNullable()
                        .WithColumn("CreatedDate").AsDateTime().NotNullable();
        }

        public override void Down()
        {
        }
    }
}