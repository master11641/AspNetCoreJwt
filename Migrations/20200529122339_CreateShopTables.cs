using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace core.Migrations
{
    public partial class CreateShopTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Advers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    ImageUrl = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true),
                    AddedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Advers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TcategoryGoodss",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    ParentID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TcategoryGoodss", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TcategoryGoodss_TcategoryGoodss_ParentID",
                        column: x => x.ParentID,
                        principalTable: "TcategoryGoodss",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TcategoryStores",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    ParentID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TcategoryStores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TcategoryStores_TcategoryStores_ParentID",
                        column: x => x.ParentID,
                        principalTable: "TcategoryStores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tstores",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    TcategoryStoreId = table.Column<int>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    IsFreeDeliveryExist = table.Column<bool>(nullable: false),
                    ImageUrl = table.Column<string>(nullable: true),
                    IsConfirm = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    UserOwner = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tstores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tstores_TcategoryStores_TcategoryStoreId",
                        column: x => x.TcategoryStoreId,
                        principalTable: "TcategoryStores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tgoodses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    TstoreId = table.Column<int>(nullable: false),
                    Abstract = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    TcategoryGoodsId = table.Column<int>(nullable: false),
                    IsFreeDeliveryExist = table.Column<bool>(nullable: false),
                    GoodsStatus = table.Column<int>(nullable: false),
                    GoodsType = table.Column<int>(nullable: false),
                    IsConfirm = table.Column<bool>(nullable: false),
                    Count = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tgoodses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tgoodses_TcategoryGoodss_TcategoryGoodsId",
                        column: x => x.TcategoryGoodsId,
                        principalTable: "TcategoryGoodss",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tgoodses_Tstores_TstoreId",
                        column: x => x.TstoreId,
                        principalTable: "Tstores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TgoodsAttributes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Required = table.Column<bool>(nullable: false),
                    GoodsCount = table.Column<int>(nullable: true),
                    DisplayOrder = table.Column<int>(nullable: false),
                    TgoodsId = table.Column<int>(nullable: false),
                    TattributeType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TgoodsAttributes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TgoodsAttributes_Tgoodses_TgoodsId",
                        column: x => x.TgoodsId,
                        principalTable: "Tgoodses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TgoodsImages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(nullable: true),
                    TgoodsId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TgoodsImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TgoodsImages_Tgoodses_TgoodsId",
                        column: x => x.TgoodsId,
                        principalTable: "Tgoodses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TgoodsPrices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<decimal>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    TgoodsId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TgoodsPrices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TgoodsPrices_Tgoodses_TgoodsId",
                        column: x => x.TgoodsId,
                        principalTable: "Tgoodses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Torders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderDate = table.Column<DateTime>(nullable: false),
                    TgoodsId = table.Column<int>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    Count = table.Column<int>(nullable: false),
                    TfactorId = table.Column<int>(nullable: true),
                    TorderStatus = table.Column<int>(nullable: false),
                    AddressId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Torders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Torders_Tgoodses_TgoodsId",
                        column: x => x.TgoodsId,
                        principalTable: "Tgoodses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TgoodsAttributeValues",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(nullable: true),
                    Caption = table.Column<string>(nullable: true),
                    TgoodsAttributeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TgoodsAttributeValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TgoodsAttributeValues_TgoodsAttributes_TgoodsAttributeId",
                        column: x => x.TgoodsAttributeId,
                        principalTable: "TgoodsAttributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TordergoodsAttributeValues",
                columns: table => new
                {
                    TorderId = table.Column<int>(nullable: false),
                    GoodsAttributeValueId = table.Column<int>(nullable: false),
                    TgoodsAttributeValueId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TordergoodsAttributeValues", x => new { x.TorderId, x.GoodsAttributeValueId });
                    table.ForeignKey(
                        name: "FK_TordergoodsAttributeValues_TgoodsAttributeValues_TgoodsAttributeValueId",
                        column: x => x.TgoodsAttributeValueId,
                        principalTable: "TgoodsAttributeValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TordergoodsAttributeValues_Torders_TorderId",
                        column: x => x.TorderId,
                        principalTable: "Torders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TcategoryGoodss_ParentID",
                table: "TcategoryGoodss",
                column: "ParentID");

            migrationBuilder.CreateIndex(
                name: "IX_TcategoryStores_ParentID",
                table: "TcategoryStores",
                column: "ParentID");

            migrationBuilder.CreateIndex(
                name: "IX_TgoodsAttributes_TgoodsId",
                table: "TgoodsAttributes",
                column: "TgoodsId");

            migrationBuilder.CreateIndex(
                name: "IX_TgoodsAttributeValues_TgoodsAttributeId",
                table: "TgoodsAttributeValues",
                column: "TgoodsAttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_Tgoodses_TcategoryGoodsId",
                table: "Tgoodses",
                column: "TcategoryGoodsId");

            migrationBuilder.CreateIndex(
                name: "IX_Tgoodses_TstoreId",
                table: "Tgoodses",
                column: "TstoreId");

            migrationBuilder.CreateIndex(
                name: "IX_TgoodsImages_TgoodsId",
                table: "TgoodsImages",
                column: "TgoodsId");

            migrationBuilder.CreateIndex(
                name: "IX_TgoodsPrices_TgoodsId",
                table: "TgoodsPrices",
                column: "TgoodsId");

            migrationBuilder.CreateIndex(
                name: "IX_TordergoodsAttributeValues_TgoodsAttributeValueId",
                table: "TordergoodsAttributeValues",
                column: "TgoodsAttributeValueId");

            migrationBuilder.CreateIndex(
                name: "IX_Torders_TgoodsId",
                table: "Torders",
                column: "TgoodsId");

            migrationBuilder.CreateIndex(
                name: "IX_Tstores_TcategoryStoreId",
                table: "Tstores",
                column: "TcategoryStoreId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Advers");

            migrationBuilder.DropTable(
                name: "TgoodsImages");

            migrationBuilder.DropTable(
                name: "TgoodsPrices");

            migrationBuilder.DropTable(
                name: "TordergoodsAttributeValues");

            migrationBuilder.DropTable(
                name: "TgoodsAttributeValues");

            migrationBuilder.DropTable(
                name: "Torders");

            migrationBuilder.DropTable(
                name: "TgoodsAttributes");

            migrationBuilder.DropTable(
                name: "Tgoodses");

            migrationBuilder.DropTable(
                name: "TcategoryGoodss");

            migrationBuilder.DropTable(
                name: "Tstores");

            migrationBuilder.DropTable(
                name: "TcategoryStores");
        }
    }
}
