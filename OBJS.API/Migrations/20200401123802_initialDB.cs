using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OBJS.API.Migrations
{
    public partial class initialDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdvertiseStates",
                columns: table => new
                {
                    AdvertiseStateId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsStarted = table.Column<bool>(nullable: false),
                    IsContinue = table.Column<bool>(nullable: false),
                    IsFinished = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvertiseStates", x => x.AdvertiseStateId);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(nullable: true),
                    ParentID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                    table.ForeignKey(
                        name: "FK_Categories_Categories_ParentID",
                        column: x => x.ParentID,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    Username = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    CompanyName = table.Column<string>(nullable: true),
                    IsCustomer = table.Column<bool>(nullable: false),
                    IsSupplier = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "Advertises",
                columns: table => new
                {
                    AdvertiseId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    Startdate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    AdvertiseStateId = table.Column<int>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false),
                    CustomerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Advertises", x => x.AdvertiseId);
                    table.ForeignKey(
                        name: "FK_Advertises_AdvertiseStates_AdvertiseStateId",
                        column: x => x.AdvertiseStateId,
                        principalTable: "AdvertiseStates",
                        principalColumn: "AdvertiseStateId");
                    table.ForeignKey(
                        name: "FK_Advertises_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId");
                    table.ForeignKey(
                        name: "FK_Advertises_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId");
                });

            migrationBuilder.CreateTable(
                name: "CustomerDetails",
                columns: table => new
                {
                    CustomerdetailId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Phone = table.Column<int>(nullable: false),
                    CustomerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerDetails", x => x.CustomerdetailId);
                    table.ForeignKey(
                        name: "FK_CustomerDetails_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AdvertiseDetails",
                columns: table => new
                {
                    AdvertiseDetailId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ImagePath = table.Column<string>(nullable: true),
                    AdvertiseId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvertiseDetails", x => x.AdvertiseDetailId);
                    table.ForeignKey(
                        name: "FK_AdvertiseDetails_Advertises_AdvertiseId",
                        column: x => x.AdvertiseId,
                        principalTable: "Advertises",
                        principalColumn: "AdvertiseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bids",
                columns: table => new
                {
                    BidId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<double>(nullable: false),
                    Duration = table.Column<int>(nullable: false),
                    AdvertiseId = table.Column<int>(nullable: false),
                    CustomerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bids", x => x.BidId);
                    table.ForeignKey(
                        name: "FK_Bids_Advertises_AdvertiseId",
                        column: x => x.AdvertiseId,
                        principalTable: "Advertises",
                        principalColumn: "AdvertiseId");
                    table.ForeignKey(
                        name: "FK_Bids_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId");
                });

            migrationBuilder.CreateTable(
                name: "Feedbacks",
                columns: table => new
                {
                    AdvertisefeedbackId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Comment = table.Column<string>(nullable: true),
                    Star = table.Column<int>(nullable: false, defaultValue: 5),
                    AdvertiseId = table.Column<int>(nullable: false),
                    OwnerID = table.Column<int>(nullable: false),
                    BidderID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedbacks", x => x.AdvertisefeedbackId);
                    table.ForeignKey(
                        name: "FK_Feedbacks_Advertises_AdvertiseId",
                        column: x => x.AdvertiseId,
                        principalTable: "Advertises",
                        principalColumn: "AdvertiseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Feedbacks_Customers_BidderID",
                        column: x => x.BidderID,
                        principalTable: "Customers",
                        principalColumn: "CustomerId");
                    table.ForeignKey(
                        name: "FK_Feedbacks_Customers_OwnerID",
                        column: x => x.OwnerID,
                        principalTable: "Customers",
                        principalColumn: "CustomerId");
                });

            migrationBuilder.InsertData(
                table: "AdvertiseStates",
                columns: new[] { "AdvertiseStateId", "IsContinue", "IsFinished", "IsStarted" },
                values: new object[,]
                {
                    { 1, false, false, true },
                    { 2, true, false, false },
                    { 3, false, true, false }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Description", "Name", "ParentID" },
                values: new object[,]
                {
                    { 1, null, "Temizlik", null },
                    { 2, null, "Tadilat", null },
                    { 3, null, "Nakliyat", null },
                    { 4, null, "Montaj-Hizmet", null }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Description", "Name", "ParentID" },
                values: new object[,]
                {
                    { 5, null, "Ev Temizliği", 1 },
                    { 6, null, "Koltuk Temizliği", 1 },
                    { 7, null, "Boyama", 2 },
                    { 8, null, "Evden Eve", 3 },
                    { 9, null, "Şehirlerarası", 3 },
                    { 10, null, "Tesisatçı", 4 }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Description", "Name", "ParentID" },
                values: new object[] { 11, null, "Elektrik Tesisatçısı", 10 });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Description", "Name", "ParentID" },
                values: new object[] { 12, null, "Su Tesisatçısı", 10 });

            migrationBuilder.CreateIndex(
                name: "IX_AdvertiseDetails_AdvertiseId",
                table: "AdvertiseDetails",
                column: "AdvertiseId");

            migrationBuilder.CreateIndex(
                name: "IX_Advertises_AdvertiseStateId",
                table: "Advertises",
                column: "AdvertiseStateId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Advertises_CategoryId",
                table: "Advertises",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Advertises_CustomerId",
                table: "Advertises",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Bids_AdvertiseId",
                table: "Bids",
                column: "AdvertiseId");

            migrationBuilder.CreateIndex(
                name: "IX_Bids_CustomerId",
                table: "Bids",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentID",
                table: "Categories",
                column: "ParentID");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerDetails_CustomerId",
                table: "CustomerDetails",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_AdvertiseId",
                table: "Feedbacks",
                column: "AdvertiseId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_BidderID",
                table: "Feedbacks",
                column: "BidderID");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_OwnerID",
                table: "Feedbacks",
                column: "OwnerID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdvertiseDetails");

            migrationBuilder.DropTable(
                name: "Bids");

            migrationBuilder.DropTable(
                name: "CustomerDetails");

            migrationBuilder.DropTable(
                name: "Feedbacks");

            migrationBuilder.DropTable(
                name: "Advertises");

            migrationBuilder.DropTable(
                name: "AdvertiseStates");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
