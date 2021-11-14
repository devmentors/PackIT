using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PackIT.Infrastructure.EF.Migrations
{
    public partial class Init_Read : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "packing");

            migrationBuilder.CreateTable(
                name: "PackingLists",
                schema: "packing",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Version = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Localization = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackingLists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PackingItems",
                schema: "packing",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Quantity = table.Column<long>(type: "bigint", nullable: false),
                    IsPacked = table.Column<bool>(type: "boolean", nullable: false),
                    PackingListId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackingItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PackingItems_PackingLists_PackingListId",
                        column: x => x.PackingListId,
                        principalSchema: "packing",
                        principalTable: "PackingLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PackingItems_PackingListId",
                schema: "packing",
                table: "PackingItems",
                column: "PackingListId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PackingItems",
                schema: "packing");

            migrationBuilder.DropTable(
                name: "PackingLists",
                schema: "packing");
        }
    }
}
