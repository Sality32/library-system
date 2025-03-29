using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Year = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Publisher = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    BorrowedQuantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActived = table.Column<bool>(type: "bit", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
