using System;
using FluentMigrator;

namespace CaseManagement.Migrations.DefaultDB
{
    [Migration(20160807120000)]
    public class DefaultDB_20160807_120000_Activity : Migration
    {
        public override void Up()
        {
            Create.Table("ActivityRecord").InSchema("dbo")
                        .WithColumn("ID").AsInt32().Identity().PrimaryKey().NotNullable()                        
                        .WithColumn("CreatedUserID").AsInt32().NotNullable()
                        .WithColumn("CreatedDate").AsDateTime().NotNullable()                        
                        .WithColumn("IsDeleted").AsBoolean().NotNullable()
                        .WithColumn("DeletedUserID").AsInt32().Nullable()
                        .WithColumn("DeletedDate").AsDateTime().Nullable()
                        .WithColumn("EndDate").AsDateTime().Nullable();

            Create.Table("ActivityRecordDetail").InSchema("dbo")
                        .WithColumn("ID").AsInt32().Identity().PrimaryKey().NotNullable()
                        .WithColumn("Count").AsInt32().Nullable()
                        .WithColumn("CycleCost").AsInt32().Nullable()
                        .WithColumn("Factor").AsInt32().Nullable()
                        .WithColumn("DelayedCost").AsInt32().Nullable()
                        .WithColumn("AccessibleCost").AsInt32().Nullable()
                        .WithColumn("InaccessibleCost").AsInt32().Nullable()
                        .WithColumn("Financial").AsInt32().Nullable()
                        .WithColumn("EventDescription").AsInt32().Nullable()
                        .WithColumn("MainReason").AsInt32().Nullable()
                        .WithColumn("CorrectionOperation").AsInt32().Nullable()
                        .WithColumn("AvoidRepeatingOperation").AsInt32().Nullable()
                        .WithColumn("CreatedUserID").AsInt32().NotNullable()
                        .WithColumn("CreatedDate").AsDateTime().NotNullable()
                        .WithColumn("IsDeleted").AsBoolean().NotNullable()
                        .WithColumn("DeletedUserID").AsInt32().Nullable()
                        .WithColumn("DeletedDate").AsDateTime().Nullable();            

            Create.Table("ActivityRecordComment").InSchema("dbo")
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