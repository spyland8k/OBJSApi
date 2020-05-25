using Microsoft.EntityFrameworkCore.Migrations;

namespace OBJS.API.Migrations
{
    public partial class feedback_changed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Feedbacks_AdvertiseId",
                table: "Feedbacks");

            migrationBuilder.AlterColumn<int>(
                name: "Star",
                table: "Feedbacks",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 5);

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_AdvertiseId",
                table: "Feedbacks",
                column: "AdvertiseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Feedbacks_AdvertiseId",
                table: "Feedbacks");

            migrationBuilder.AlterColumn<int>(
                name: "Star",
                table: "Feedbacks",
                type: "int",
                nullable: false,
                defaultValue: 5,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_AdvertiseId",
                table: "Feedbacks",
                column: "AdvertiseId",
                unique: true);
        }
    }
}
