#pragma warning disable
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Business.Infrastructure.Migrations.Product
{
  /// <inheritdoc />
  public partial class InitialMigration : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.CreateTable(
          name: "ApplicationUsers",
          columns: table => new
          {
            UserId = table.Column<int>(type: "int", nullable: false)
                  .Annotation("SqlServer:Identity", "1, 1"),
            FirstName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
            Surname = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
            UserName = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
            Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
            CreatedBy = table.Column<int>(type: "int", nullable: false),
            UpdatedBy = table.Column<int>(type: "int", nullable: false),
            CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
            UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_ApplicationUsers", x => x.UserId);
          });

      migrationBuilder.InsertData(
          table: "ApplicationUsers",
          columns: new[] { "UserId", "CreatedBy", "CreatedOn", "Email", "FirstName", "Surname", "UpdatedBy", "UpdatedOn", "UserName" },
          values: new object[] { 1, 1, new DateTime(2025, 9, 19, 18, 37, 36, 728, DateTimeKind.Utc).AddTicks(1689), "SYSTEM@Application.com", "SYSTEM", "SYSTEM", 1, new DateTime(2025, 9, 19, 18, 37, 36, 728, DateTimeKind.Utc).AddTicks(1909), "SYSTEM" });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(
          name: "ApplicationUsers");
    }
  }
}
#pragma warning restore