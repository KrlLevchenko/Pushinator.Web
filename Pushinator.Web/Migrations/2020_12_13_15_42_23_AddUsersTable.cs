using FluentMigrator;

namespace Pushinator.Web.Migrations
{
    [Migration(2020_12_13_15_42_23)]
    public class _2020_12_13_15_42_23_AddUsersTable: AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("Users")
                .WithColumn("Id").AsCustom("binary(16)").PrimaryKey().NotNullable()
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("Email").AsString().Nullable().Unique()
                .WithColumn("PasswordHash").AsString().Nullable();
        }
    }
}