using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Account.Microservice.Infrastructure.Migrations;

  /// <inheritdoc />
  public partial class UpdateModelPermission : Migration
  {
      /// <inheritdoc />
      protected override void Up(MigrationBuilder migrationBuilder)
      {
          migrationBuilder.CreateIndex(
              name: "IX_Permission_ParentId",
              table: "Permission",
              column: "ParentId");

          migrationBuilder.AddForeignKey(
              name: "FK_Permission_Permission_ParentId",
              table: "Permission",
              column: "ParentId",
              principalTable: "Permission",
              principalColumn: "Id");
      }

      /// <inheritdoc />
      protected override void Down(MigrationBuilder migrationBuilder)
      {
          migrationBuilder.DropForeignKey(
              name: "FK_Permission_Permission_ParentId",
              table: "Permission");

          migrationBuilder.DropIndex(
              name: "IX_Permission_ParentId",
              table: "Permission");
      }
  }
