using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Account.Microservice.Infrastructure.Migrations;

  /// <inheritdoc />
  public partial class InitialModel : Migration
  {
      /// <inheritdoc />
      protected override void Up(MigrationBuilder migrationBuilder)
      {
          migrationBuilder.CreateTable(
              name: "Role",
              columns: table => new
              {
                  Id = table.Column<int>(type: "int", nullable: false)
                      .Annotation("SqlServer:Identity", "1, 1"),
                  RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                  SystemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                  IsActive = table.Column<bool>(type: "bit", nullable: false),
                  IsSystemRole = table.Column<bool>(type: "bit", nullable: false),
                  Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                  Level = table.Column<int>(type: "int", nullable: false),
                  DisplayOrder = table.Column<int>(type: "int", nullable: false)
              },
              constraints: table =>
              {
                  table.PrimaryKey("PK_Role", x => x.Id);
              });

          migrationBuilder.CreateTable(
              name: "RolePermission",
              columns: table => new
              {
                  Id = table.Column<int>(type: "int", nullable: false)
                      .Annotation("SqlServer:Identity", "1, 1"),
                  RoleId = table.Column<int>(type: "int", nullable: false),
                  PermissionId = table.Column<int>(type: "int", nullable: false)
              },
              constraints: table =>
              {
                  table.PrimaryKey("PK_RolePermission", x => x.Id);
              });

          migrationBuilder.CreateTable(
              name: "Users",
              columns: table => new
              {
                  Id = table.Column<int>(type: "int", nullable: false)
                      .Annotation("SqlServer:Identity", "1, 1"),
                  RoleId = table.Column<int>(type: "int", nullable: false),
                  Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                  FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                  LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                  Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                  Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                  Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                  Salt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                  PasswordFormat = table.Column<int>(type: "int", nullable: false),
                  IsSystemRole = table.Column<bool>(type: "bit", nullable: false),
                  IsActive = table.Column<bool>(type: "bit", nullable: false),
                  LastActivityDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                  LastLoginDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                  FailedLoginAttempts = table.Column<int>(type: "int", nullable: false),
                  ExpireKeyConvertPassword = table.Column<DateTime>(type: "datetime2", nullable: true),
                  KeyConvertPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                  CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                  CreateUid = table.Column<int>(type: "int", nullable: false),
                  UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                  MediaId = table.Column<int>(type: "int", nullable: true),
                  EmailSecondary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                  UpdateUid = table.Column<int>(type: "int", nullable: true),
                  CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                  Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                  Status = table.Column<int>(type: "int", nullable: false),
                  SellerId = table.Column<int>(type: "int", nullable: false),
                  RememberToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                  Profile = table.Column<string>(type: "nvarchar(max)", nullable: false)
              },
              constraints: table =>
              {
                  table.PrimaryKey("PK_Users", x => x.Id);
                  table.ForeignKey(
                      name: "FK_Users_Role_RoleId",
                      column: x => x.RoleId,
                      principalTable: "Role",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
              });

          migrationBuilder.CreateIndex(
              name: "IX_Users_RoleId",
              table: "Users",
              column: "RoleId");
      }

      /// <inheritdoc />
      protected override void Down(MigrationBuilder migrationBuilder)
      {
          migrationBuilder.DropTable(
              name: "RolePermission");

          migrationBuilder.DropTable(
              name: "Users");

          migrationBuilder.DropTable(
              name: "Role");
      }
  }
