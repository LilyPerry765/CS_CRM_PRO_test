using System;
using FluentMigrator;

namespace CaseManagement.Migrations.DefaultDB
{
    [Migration(20160903100000)]
    public class DefaultDB_20160903_100000_Log : Migration
    {
        public override void Up()
        {
            Create.Table("Log").InSchema("dbo")
                            .WithColumn("ID").AsInt32().Identity().PrimaryKey().NotNullable()
                            .WithColumn("TableName").AsString(1000).NotNullable()
                            .WithColumn("PersianTableName").AsString(1000).NotNullable()
                            .WithColumn("RecordID").AsInt32().NotNullable()
                            .WithColumn("RecordName").AsString(1000).NotNullable()
                            .WithColumn("ActionID").AsInt32().NotNullable()
                            .WithColumn("UserID").AsInt32().NotNullable().ForeignKey("UserID10", "dbo", "Users", "UserId")
                            .WithColumn("InsertDate").AsDateTime().NotNullable();
        }

        public override void Down()
        {
        }
    }
}