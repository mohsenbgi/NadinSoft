using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NadinSoft.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ProduceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ManufacturePhone = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    ManufactureEmail = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_ManufactureEmail",
                table: "Products",
                column: "ManufactureEmail",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProduceDate",
                table: "Products",
                column: "ProduceDate",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
