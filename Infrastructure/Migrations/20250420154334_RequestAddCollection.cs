using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RequestAddCollection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "RequestAddCollections",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DetailBooksKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestAddCollections", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "IsActived", "Name", "Password", "Role" },
                values: new object[,]
                {
                    { new Guid("1d37ecca-0b1f-4e4c-a12d-31216dddcf4d"), "admin@example.com", true, "Admin", "AQAAAAIAAYagAAAAEPSV3tmivxRu4Rr+gkolaW2QahnGzAuwmg6rSpG9R8hFpQQSRHd9MQd5YZ0cRUsRVA==", "Admin" },
                    { new Guid("4cb7dc20-56b8-44fa-bdda-35555d6bfa76"), "user@example.com", true, "User", "AQAAAAIAAYagAAAAEJGhvdh543Ljp952C3wt9juJY3J2X7DG6e8iRnfPG0Pj8RG3OtSB8CB9n2LqVx3B3A==", "User" },
                    { new Guid("ecc77f17-5aa9-463f-b516-03327907c2a5"), "officer@example.com", true, "Officer", "AQAAAAIAAYagAAAAEHzAZPv9BhIucZjYnlBYhHuClFP1/jAg20D+1Q0T2KvFolZwt52Sjhl7JAAIa8aPdA==", "Officer" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RequestAddCollections");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("1d37ecca-0b1f-4e4c-a12d-31216dddcf4d"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("4cb7dc20-56b8-44fa-bdda-35555d6bfa76"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("ecc77f17-5aa9-463f-b516-03327907c2a5"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "IsActived", "Name", "Password", "Role" },
                values: new object[,]
                {
                    { new Guid("049b7d0a-c69a-4be1-a0e3-c2654625ae26"), "user@example.com", true, "User", "AQAAAAIAAYagAAAAEL5F6aiqmP5seH5L4ty9Vbr6iKJReIG6M+N5egkxvVUMmbUht1Ux22xm+LPOWZd2ZA==", "User" },
                    { new Guid("0bc3f80c-68bd-4fc8-aa7c-f741c6febc49"), "admin@example.com", true, "Admin", "AQAAAAIAAYagAAAAEJU5Cd3lJck1GLM4aK7UFPUNRO/Wj6ADACSCMaf0fMRr8VBaEmvSIyyzdXrrssaVGw==", "Admin" },
                    { new Guid("7ab70683-3264-46d0-aec9-3b728d523ba7"), "officer@example.com", true, "Officer", "AQAAAAIAAYagAAAAEBwOyDMN3EiIar6qdfWKO0Q9nW0mB32q6XWpdwyxSoIUnxh1M19Dy0N5eQijHt4XLg==", "Officer" }
                });
        }
    }
}
