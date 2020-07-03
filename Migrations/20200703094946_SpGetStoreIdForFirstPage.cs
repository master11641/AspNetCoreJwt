using Microsoft.EntityFrameworkCore.Migrations;

namespace core.Migrations {
    public partial class SpGetStoreIdForFirstPage : Migration {
        protected override void Up (MigrationBuilder migrationBuilder) {
            migrationBuilder.CreateTable (
                name: "hierarchyDTO",
                columns : table => new {
                    Item_id = table.Column<int> (nullable: false),
                        Depth = table.Column<int> (nullable: false)
                },
                constraints : table => { });
            var sp = @"CREATE PROCEDURE [dbo].[Sp_GetStoreIdForFirstPage]  
              AS   
  SET NOCOUNT ON;  
WITH myCTE (Item_id, Depth)
 AS
 (
    Select TcategoryStores.Id, 0 as Depth From dbo.TcategoryStores  where TcategoryStores.ShowInFirstPage = 1
    Union ALL
    Select TcategoryStores.Id, Depth + 1 
    From TcategoryStores 
    inner join myCte on TcategoryStores.ParentID = myCte.Item_id
 )
 Select Item_id, Depth from myCTE";

            migrationBuilder.Sql (sp);
        }

        protected override void Down (MigrationBuilder migrationBuilder) {
            migrationBuilder.DropTable (
                name: "hierarchyDTO");
        }
    }
}