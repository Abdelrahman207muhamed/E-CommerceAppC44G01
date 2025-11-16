using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOnTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SubTotal",
                table: "Orders",
                newName: "subTotal");

            migrationBuilder.RenameColumn(
                name: "OrderDate",
                table: "Orders",
                newName: "orderDate");

            migrationBuilder.RenameColumn(
                name: "Address_Street",
                table: "Orders",
                newName: "shipToAddress_Street");

            migrationBuilder.RenameColumn(
                name: "Address_LastName",
                table: "Orders",
                newName: "shipToAddress_LastName");

            migrationBuilder.RenameColumn(
                name: "Address_FirstName",
                table: "Orders",
                newName: "shipToAddress_FirstName");

            migrationBuilder.RenameColumn(
                name: "Address_Country",
                table: "Orders",
                newName: "shipToAddress_Country");

            migrationBuilder.RenameColumn(
                name: "Address_City",
                table: "Orders",
                newName: "shipToAddress_City");

            migrationBuilder.RenameColumn(
                name: "UserEmail",
                table: "Orders",
                newName: "buyerEmail");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "DeliveryMethods",
                newName: "Cost");

            migrationBuilder.AddColumn<int>(
                name: "status",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "subTotal",
                table: "Orders",
                newName: "SubTotal");

            migrationBuilder.RenameColumn(
                name: "orderDate",
                table: "Orders",
                newName: "OrderDate");

            migrationBuilder.RenameColumn(
                name: "shipToAddress_Street",
                table: "Orders",
                newName: "Address_Street");

            migrationBuilder.RenameColumn(
                name: "shipToAddress_LastName",
                table: "Orders",
                newName: "Address_LastName");

            migrationBuilder.RenameColumn(
                name: "shipToAddress_FirstName",
                table: "Orders",
                newName: "Address_FirstName");

            migrationBuilder.RenameColumn(
                name: "shipToAddress_Country",
                table: "Orders",
                newName: "Address_Country");

            migrationBuilder.RenameColumn(
                name: "shipToAddress_City",
                table: "Orders",
                newName: "Address_City");

            migrationBuilder.RenameColumn(
                name: "buyerEmail",
                table: "Orders",
                newName: "UserEmail");

            migrationBuilder.RenameColumn(
                name: "Cost",
                table: "DeliveryMethods",
                newName: "Price");
        }
    }
}
