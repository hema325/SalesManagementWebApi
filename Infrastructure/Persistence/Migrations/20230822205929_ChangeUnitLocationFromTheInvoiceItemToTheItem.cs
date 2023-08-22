using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeUnitLocationFromTheInvoiceItemToTheItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceItems_Units_UnitId",
                table: "InvoiceItems");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceItems_UnitId",
                table: "InvoiceItems");

            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "InvoiceItems");

            migrationBuilder.AddColumn<int>(
                name: "UnitId",
                table: "Items",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ItemUnitId",
                table: "InvoiceItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Items_UnitId",
                table: "Items",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItems_ItemUnitId",
                table: "InvoiceItems",
                column: "ItemUnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceItems_Units_ItemUnitId",
                table: "InvoiceItems",
                column: "ItemUnitId",
                principalTable: "Units",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Units_UnitId",
                table: "Items",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceItems_Units_ItemUnitId",
                table: "InvoiceItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Units_UnitId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_UnitId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceItems_ItemUnitId",
                table: "InvoiceItems");

            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ItemUnitId",
                table: "InvoiceItems");

            migrationBuilder.AddColumn<int>(
                name: "UnitId",
                table: "InvoiceItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItems_UnitId",
                table: "InvoiceItems",
                column: "UnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceItems_Units_UnitId",
                table: "InvoiceItems",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
