using Microsoft.EntityFrameworkCore.Migrations;

namespace OBJS.API.Migrations
{
    public partial class onModelCreatingChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdvertiseStates_Advertises_AdvertiseId",
                table: "AdvertiseStates");

            migrationBuilder.DropIndex(
                name: "IX_AdvertiseStates_AdvertiseId",
                table: "AdvertiseStates");

            migrationBuilder.DropColumn(
                name: "AdvertiseId",
                table: "AdvertiseStates");

            migrationBuilder.CreateIndex(
                name: "IX_Advertises_AdvertiseStateId",
                table: "Advertises",
                column: "AdvertiseStateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Advertises_AdvertiseStates_AdvertiseStateId",
                table: "Advertises",
                column: "AdvertiseStateId",
                principalTable: "AdvertiseStates",
                principalColumn: "AdvertiseStateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Advertises_AdvertiseStates_AdvertiseStateId",
                table: "Advertises");

            migrationBuilder.DropIndex(
                name: "IX_Advertises_AdvertiseStateId",
                table: "Advertises");

            migrationBuilder.AddColumn<int>(
                name: "AdvertiseId",
                table: "AdvertiseStates",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AdvertiseStates_AdvertiseId",
                table: "AdvertiseStates",
                column: "AdvertiseId");

            migrationBuilder.AddForeignKey(
                name: "FK_AdvertiseStates_Advertises_AdvertiseId",
                table: "AdvertiseStates",
                column: "AdvertiseId",
                principalTable: "Advertises",
                principalColumn: "AdvertiseId");
        }
    }
}
