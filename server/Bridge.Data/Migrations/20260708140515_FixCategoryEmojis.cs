using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Bridge.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixCategoryEmojis : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "Icon", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("3f841366-f62d-47ae-9431-e782bea00048"), new DateTime(2026, 7, 8, 14, 5, 14, 210, DateTimeKind.Utc).AddTicks(9349), "ℹ️", "מידע והמלצות", null },
                    { new Guid("5dacfdcf-7743-47fa-971c-e0b21c345f7d"), new DateTime(2026, 7, 8, 14, 5, 14, 210, DateTimeKind.Utc).AddTicks(9347), "📄", "שפה / בירוקרטיה", null },
                    { new Guid("6d1f47fa-3bcd-4996-99a4-8d1a5452acdc"), new DateTime(2026, 7, 8, 14, 5, 14, 210, DateTimeKind.Utc).AddTicks(9335), "🏠", "אירוח / סעודות שבת", null },
                    { new Guid("93b43ca5-4da2-4898-a0d2-e3b89daeeb35"), new DateTime(2026, 7, 8, 14, 5, 14, 210, DateTimeKind.Utc).AddTicks(9333), "📖", "עזרה דתית / מניין", null },
                    { new Guid("988083e4-a6bb-460a-9b90-b0dc2a7390d4"), new DateTime(2026, 7, 8, 14, 5, 14, 210, DateTimeKind.Utc).AddTicks(9327), "🚗", "שינוע / קניות", null },
                    { new Guid("c2e1e08d-c069-44b2-ac2c-fb5653558e26"), new DateTime(2026, 7, 8, 14, 5, 14, 210, DateTimeKind.Utc).AddTicks(9336), "🍎", "אוכל כשר ומוצרים", null },
                    { new Guid("c85f0bff-8785-436e-9124-0c4944ff829a"), new DateTime(2026, 7, 8, 14, 5, 14, 210, DateTimeKind.Utc).AddTicks(9331), "💬", "תמיכה חברתית / שיחה", null },
                    { new Guid("d6cc541a-5b98-4240-9362-5a4dfd60e47d"), new DateTime(2026, 7, 8, 14, 5, 14, 210, DateTimeKind.Utc).AddTicks(9256), "🛠️", "עזרה טכנית", null },
                    { new Guid("e4047c16-8f18-4dd0-9578-7203a4b6a234"), new DateTime(2026, 7, 8, 14, 5, 14, 210, DateTimeKind.Utc).AddTicks(9350), "🆘", "חירום / חילוץ", null },
                    { new Guid("f1325d87-6c41-4b7e-b715-a3384509283d"), new DateTime(2026, 7, 8, 14, 5, 14, 210, DateTimeKind.Utc).AddTicks(9330), "❤️", "עזרה רפואית / ליווי", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("3f841366-f62d-47ae-9431-e782bea00048"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("5dacfdcf-7743-47fa-971c-e0b21c345f7d"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("6d1f47fa-3bcd-4996-99a4-8d1a5452acdc"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("93b43ca5-4da2-4898-a0d2-e3b89daeeb35"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("988083e4-a6bb-460a-9b90-b0dc2a7390d4"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("c2e1e08d-c069-44b2-ac2c-fb5653558e26"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("c85f0bff-8785-436e-9124-0c4944ff829a"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("d6cc541a-5b98-4240-9362-5a4dfd60e47d"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("e4047c16-8f18-4dd0-9578-7203a4b6a234"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("f1325d87-6c41-4b7e-b715-a3384509283d"));

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
    }
}
