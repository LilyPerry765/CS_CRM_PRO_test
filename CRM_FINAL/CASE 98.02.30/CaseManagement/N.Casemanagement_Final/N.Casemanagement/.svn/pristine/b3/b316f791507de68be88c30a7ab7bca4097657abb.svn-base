using System;
using FluentMigrator;

namespace CaseManagement.Migrations.DefaultDB
{
    [Migration(20160801150500)]
    public class DefaultDB_20160801_150500_BaseInfoRelation : Migration
    {
        public override void Up()
        {
            Alter.Table("Province").InSchema("dbo")
                .AddColumn("PMOLevelID").AsInt32().Nullable()
                    .ForeignKey("PMOLevelID", "dbo", "PMOLevel", "ID");

            Alter.Table("Activity").InSchema("dbo")
                .AddColumn("GroupID").AsInt32().Nullable()
                    .ForeignKey("GroupID", "dbo", "ActivityGroup", "ID");

            Alter.Table("Activity").InSchema("dbo")
                .AddColumn("RepeatTermID").AsInt32().Nullable()
                    .ForeignKey("RepeatTermID", "dbo", "RepeatTerm", "ID");

            Alter.Table("ProvinceProgram").InSchema("dbo")
                .AddColumn("ProvinceID").AsInt32().Nullable()
                    .ForeignKey("ProvinceID", "dbo", "Province", "ID");

            Alter.Table("ProvinceProgram").InSchema("dbo")
                .AddColumn("YearID").AsInt32().Nullable()
                    .ForeignKey("YearID", "dbo", "Year", "ID");
        }

        public override void Down()
        {
        }
    }
}