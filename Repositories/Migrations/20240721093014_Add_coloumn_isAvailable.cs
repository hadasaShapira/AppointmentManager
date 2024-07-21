using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppointmentsApp.Migrations
{
    /// <inheritdoc />
    public partial class Add_coloumn_isAvailable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "Appointments",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "Appointments");
        }
    }
}
