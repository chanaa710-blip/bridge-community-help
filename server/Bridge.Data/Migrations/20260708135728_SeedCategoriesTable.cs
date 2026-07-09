using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Bridge.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedCategoriesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Categories");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "Icon", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("2265ea08-3732-4f73-b3b3-9945788923a7"), new DateTime(2026, 7, 8, 13, 57, 27, 72, DateTimeKind.Utc).AddTicks(2886), "wrench", "עזרה טכנית", null },
                    { new Guid("25a43237-411a-4d58-9dea-b5596cf72453"), new DateTime(2026, 7, 8, 13, 57, 27, 72, DateTimeKind.Utc).AddTicks(2949), "food-apple", "אוכל כשר ומוצרים", null },
                    { new Guid("52eb75b6-52e1-40f7-abd6-c85f4cb2d965"), new DateTime(2026, 7, 8, 13, 57, 27, 72, DateTimeKind.Utc).AddTicks(2956), "alert-octagon", "חירום / חילוץ", null },
                    { new Guid("58fb8a21-fcf0-411e-bc32-2059bf8719c7"), new DateTime(2026, 7, 8, 13, 57, 27, 72, DateTimeKind.Utc).AddTicks(2933), "book-open-variant", "עזרה דתית / מניין", null },
                    { new Guid("84bc977f-f570-4ba8-8058-427e5f84edb0"), new DateTime(2026, 7, 8, 13, 57, 27, 72, DateTimeKind.Utc).AddTicks(2931), "comment-text-outline", "תמיכה חברתית / שיחה", null },
                    { new Guid("884a97ff-bce9-487f-8779-838c03e1fbd5"), new DateTime(2026, 7, 8, 13, 57, 27, 72, DateTimeKind.Utc).AddTicks(2928), "heart-pulse", "עזרה רפואית / ליווי", null },
                    { new Guid("9accb771-5b17-4f3c-90a6-87f4f4ae0f64"), new DateTime(2026, 7, 8, 13, 57, 27, 72, DateTimeKind.Utc).AddTicks(2925), "car", "שינוע / קניות", null },
                    { new Guid("b23de822-12af-4164-88e9-dc4f9776a799"), new DateTime(2026, 7, 8, 13, 57, 27, 72, DateTimeKind.Utc).AddTicks(2954), "information-outline", "מידע והמלצות", null },
                    { new Guid("dceed682-34e8-4998-9562-63943d47f4d3"), new DateTime(2026, 7, 8, 13, 57, 27, 72, DateTimeKind.Utc).AddTicks(2952), "file-document-outline", "שפה / בירוקרטיה", null },
                    { new Guid("f56b1564-b8d3-41fa-8227-1e4a91020dbd"), new DateTime(2026, 7, 8, 13, 57, 27, 72, DateTimeKind.Utc).AddTicks(2947), "home-heart", "אירוח / סעודות שבת", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("2265ea08-3732-4f73-b3b3-9945788923a7"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("25a43237-411a-4d58-9dea-b5596cf72453"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("52eb75b6-52e1-40f7-abd6-c85f4cb2d965"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("58fb8a21-fcf0-411e-bc32-2059bf8719c7"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("84bc977f-f570-4ba8-8058-427e5f84edb0"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("884a97ff-bce9-487f-8779-838c03e1fbd5"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("9accb771-5b17-4f3c-90a6-87f4f4ae0f64"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b23de822-12af-4164-88e9-dc4f9776a799"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("dceed682-34e8-4998-9562-63943d47f4d3"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("f56b1564-b8d3-41fa-8227-1e4a91020dbd"));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
