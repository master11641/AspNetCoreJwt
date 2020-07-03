﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace core.Migrations
{
    public partial class SpStoreCategoryRecursive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
 var sp = @"CREATE PROCEDURE [dbo].[Sp_StoreCategoryRecursive]  
@RootId int
  AS   
  SET NOCOUNT ON;  
  WITH myCTE (Item_id, Depth)
 AS
 (
    Select TcategoryStores.Id, 0 as Depth From dbo.TcategoryStores  where TcategoryStores.ParentID = @RootId
    Union ALL
    Select TcategoryStores.Id, Depth + 1 
    From TcategoryStores 
    inner join myCte on TcategoryStores.ParentID = myCte.Item_id
 )
 Select Item_id, Depth from myCTE WHERE myCTE.Depth=0";

            migrationBuilder.Sql (sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
