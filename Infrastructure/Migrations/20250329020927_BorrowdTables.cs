using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class BorrowdTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("4caebd74-f83d-47ed-aee1-234a5622701b"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("832e6ef5-716e-47a1-ab62-5cfc2f0527ca"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("e4fda5d9-3f83-4aa8-81d9-d57f1f4160ef"));

            migrationBuilder.CreateTable(
                name: "BorrowedRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequestDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BorrowedRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BorrowedRequests_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BorrowedRequests_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BorrowedBooks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BorrowedRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BorrowedRequestId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BorrowedBooks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BorrowedBooks_BorrowedRequests_BorrowedRequestId",
                        column: x => x.BorrowedRequestId,
                        principalTable: "BorrowedRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BorrowedBooks_BorrowedRequests_BorrowedRequestId1",
                        column: x => x.BorrowedRequestId1,
                        principalTable: "BorrowedRequests",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "IsActived", "Name", "Password", "Role" },
                values: new object[,]
                {
                    { new Guid("049b7d0a-c69a-4be1-a0e3-c2654625ae26"), "user@example.com", true, "User", "AQAAAAIAAYagAAAAEL5F6aiqmP5seH5L4ty9Vbr6iKJReIG6M+N5egkxvVUMmbUht1Ux22xm+LPOWZd2ZA==", "User" },
                    { new Guid("0bc3f80c-68bd-4fc8-aa7c-f741c6febc49"), "admin@example.com", true, "Admin", "AQAAAAIAAYagAAAAEJU5Cd3lJck1GLM4aK7UFPUNRO/Wj6ADACSCMaf0fMRr8VBaEmvSIyyzdXrrssaVGw==", "Admin" },
                    { new Guid("7ab70683-3264-46d0-aec9-3b728d523ba7"), "officer@example.com", true, "Officer", "AQAAAAIAAYagAAAAEBwOyDMN3EiIar6qdfWKO0Q9nW0mB32q6XWpdwyxSoIUnxh1M19Dy0N5eQijHt4XLg==", "Officer" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BorrowedBooks_BorrowedRequestId",
                table: "BorrowedBooks",
                column: "BorrowedRequestId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BorrowedBooks_BorrowedRequestId1",
                table: "BorrowedBooks",
                column: "BorrowedRequestId1",
                unique: true,
                filter: "[BorrowedRequestId1] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BorrowedRequests_BookId",
                table: "BorrowedRequests",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BorrowedRequests_UserId",
                table: "BorrowedRequests",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BorrowedBooks");

            migrationBuilder.DropTable(
                name: "BorrowedRequests");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("049b7d0a-c69a-4be1-a0e3-c2654625ae26"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0bc3f80c-68bd-4fc8-aa7c-f741c6febc49"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("7ab70683-3264-46d0-aec9-3b728d523ba7"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "IsActived", "Name", "Password", "Role" },
                values: new object[,]
                {
                    { new Guid("4caebd74-f83d-47ed-aee1-234a5622701b"), "officer@example.com", true, "Officer", "AQAAAAIAAYagAAAAEPbCIr/K6DtlrNYHAEK/LuUraRpKLcHTmAFB08wT8UR178Huzq7zqt/ar9ufyRFd9Q==", "Officer" },
                    { new Guid("832e6ef5-716e-47a1-ab62-5cfc2f0527ca"), "admin@example.com", true, "Admin", "AQAAAAIAAYagAAAAENe8WV24ZVRQJNCj1kwCpQna6IuGH2HuGnKg9TxYJdz4dBWa5JMlc5wDO1YYDxmFWQ==", "Admin" },
                    { new Guid("e4fda5d9-3f83-4aa8-81d9-d57f1f4160ef"), "user@example.com", true, "User", "AQAAAAIAAYagAAAAEG1MstTnLHLZbEGZcD5q3FdakMLxdEr9Lq+brChh5N49/VmaiMfbWrdsKVHMwQAHJw==", "User" }
                });
        }
    }
}
