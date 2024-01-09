using System;
using FluentMigrator;

namespace CaseManagement.Migrations.DefaultDB
{
    [Migration(20161027080000)]
    public class DefaultDB_20161027_080000_ActivityRequestReason : Migration
    {
        public override void Up()
        {
            Create.Table("CommentReason").InSchema("dbo")
                        .WithColumn("ID").AsInt32().Identity().PrimaryKey().NotNullable()
                        .WithColumn("Comment").AsString(10000).NotNullable()
                        .WithColumn("CreatedUserID").AsInt32().NotNullable()
                        .WithColumn("CreatedDate").AsDateTime().NotNullable();

            Create.Table("ActivityRequestCommentReason").InSchema("dbo")
                      .WithColumn("ID").AsInt32().Identity().PrimaryKey().NotNullable()
                      .WithColumn("CommentReasonID").AsInt32().NotNullable().ForeignKey("CommentReasonID", "dbo", "CommentReason", "ID")
                      .WithColumn("ActivityRequestID").AsInt32().NotNullable().ForeignKey("ActivityRequest_CommentReason", "dbo", "ActivityRequest", "ID");
        }

        public override void Down()
        {
        }
    }
}