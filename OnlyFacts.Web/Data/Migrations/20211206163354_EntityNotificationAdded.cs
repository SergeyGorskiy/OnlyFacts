using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlyFacts.Web.Data.Migrations
{
    public partial class EntityNotificationAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(3000)", maxLength: 3000, nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    AddressFrom = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    AddressTo = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notification_AddressFrom",
                table: "Notification",
                column: "AddressFrom");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_AddressTo",
                table: "Notification",
                column: "AddressTo");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_Content",
                table: "Notification",
                column: "Content");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_Subject",
                table: "Notification",
                column: "Subject");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notification");
        }
    }
}
