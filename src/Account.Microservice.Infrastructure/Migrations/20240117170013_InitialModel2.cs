using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Account.Microservice.Infrastructure.Migrations;

  /// <inheritdoc />
  public partial class InitialModel2 : Migration
  {
      /// <inheritdoc />
      protected override void Up(MigrationBuilder migrationBuilder)
      {
          migrationBuilder.CreateTable(
              name: "EmailAccount",
              columns: table => new
              {
                  Id = table.Column<int>(type: "int", nullable: false)
                      .Annotation("SqlServer:Identity", "1, 1"),
                  AddressEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                  Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                  Host = table.Column<string>(type: "nvarchar(max)", nullable: false),
                  UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                  Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                  Port = table.Column<int>(type: "int", nullable: false),
                  Ssl = table.Column<bool>(type: "bit", nullable: false),
                  IsDefault = table.Column<bool>(type: "bit", nullable: false)
              },
              constraints: table =>
              {
                  table.PrimaryKey("PK_EmailAccount", x => x.Id);
              });

          migrationBuilder.CreateTable(
              name: "EmailRelated",
              columns: table => new
              {
                  Id = table.Column<int>(type: "int", nullable: false)
                      .Annotation("SqlServer:Identity", "1, 1"),
                  UserId = table.Column<int>(type: "int", nullable: false),
                  Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
              },
              constraints: table =>
              {
                  table.PrimaryKey("PK_EmailRelated", x => x.Id);
                  table.ForeignKey(
                      name: "FK_EmailRelated_Users_UserId",
                      column: x => x.UserId,
                      principalTable: "Users",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
              });

          migrationBuilder.CreateTable(
              name: "EmailTemplate",
              columns: table => new
              {
                  Id = table.Column<int>(type: "int", nullable: false)
                      .Annotation("SqlServer:Identity", "1, 1"),
                  SystemName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                  EmailSubject = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                  ContentIsHtml = table.Column<bool>(type: "bit", nullable: false),
                  Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                  Enable = table.Column<bool>(type: "bit", nullable: false),
                  CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
              },
              constraints: table =>
              {
                  table.PrimaryKey("PK_EmailTemplate", x => x.Id);
              });

          migrationBuilder.CreateTable(
              name: "Media",
              columns: table => new
              {
                  Id = table.Column<int>(type: "int", nullable: false)
                      .Annotation("SqlServer:Identity", "1, 1"),
                  MimeType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                  SeoFilename = table.Column<string>(type: "nvarchar(max)", nullable: true),
                  AltAttribute = table.Column<string>(type: "nvarchar(max)", nullable: true),
                  TitleAttribute = table.Column<string>(type: "nvarchar(max)", nullable: true),
                  IsNew = table.Column<bool>(type: "bit", nullable: false),
                  VirtualPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                  MediaBinaryId = table.Column<long>(name: "MediaBinary_Id", type: "bigint", nullable: false),
                  Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                  Size = table.Column<long>(type: "bigint", nullable: false),
                  MediaType = table.Column<int>(type: "int", nullable: false),
                  CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                  IsShow = table.Column<bool>(type: "bit", nullable: false),
                  CreateUid = table.Column<int>(type: "int", nullable: false)
              },
              constraints: table =>
              {
                  table.PrimaryKey("PK_Media", x => x.Id);
              });

          migrationBuilder.CreateTable(
              name: "MediaBinary",
              columns: table => new
              {
                  Id = table.Column<int>(type: "int", nullable: false)
                      .Annotation("SqlServer:Identity", "1, 1"),
                  BinaryData = table.Column<byte>(type: "tinyint", nullable: false),
                  MediaId = table.Column<int>(type: "int", nullable: false)
              },
              constraints: table =>
              {
                  table.PrimaryKey("PK_MediaBinary", x => x.Id);
              });

          migrationBuilder.CreateTable(
              name: "Permission",
              columns: table => new
              {
                  Id = table.Column<int>(type: "int", nullable: false)
                      .Annotation("SqlServer:Identity", "1, 1"),
                  Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                  SystemName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                  Category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                  IsActive = table.Column<bool>(type: "bit", nullable: false),
                  ParentId = table.Column<int>(type: "int", nullable: true),
                  DisplayOrder = table.Column<int>(type: "int", nullable: false)
              },
              constraints: table =>
              {
                  table.PrimaryKey("PK_Permission", x => x.Id);
              });

          migrationBuilder.CreateTable(
              name: "QueuedEmail",
              columns: table => new
              {
                  Id = table.Column<int>(type: "int", nullable: false)
                      .Annotation("SqlServer:Identity", "1, 1"),
                  PriorityId = table.Column<int>(type: "int", nullable: false),
                  Status = table.Column<int>(type: "int", nullable: false),
                  From = table.Column<string>(type: "nvarchar(max)", nullable: false),
                  FromName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                  To = table.Column<string>(type: "nvarchar(max)", nullable: false),
                  ToName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                  ReplyTo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                  ReplyToName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                  CC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                  Bcc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                  Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                  Body = table.Column<string>(type: "nvarchar(max)", nullable: false),
                  IsBodyHtml = table.Column<bool>(type: "bit", nullable: false),
                  AttachmentFilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                  AttachmentFileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                  AttachedDownloadId = table.Column<int>(type: "int", nullable: false),
                  DontSendBeforeDateUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                  SentTries = table.Column<int>(type: "int", nullable: false),
                  SentOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                  CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
              },
              constraints: table =>
              {
                  table.PrimaryKey("PK_QueuedEmail", x => x.Id);
              });

          migrationBuilder.CreateTable(
              name: "Setting",
              columns: table => new
              {
                  Id = table.Column<int>(type: "int", nullable: false)
                      .Annotation("SqlServer:Identity", "1, 1"),
                  Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                  Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                  CompanyId = table.Column<int>(type: "int", nullable: false)
              },
              constraints: table =>
              {
                  table.PrimaryKey("PK_Setting", x => x.Id);
              });

          migrationBuilder.CreateIndex(
              name: "IX_EmailRelated_UserId",
              table: "EmailRelated",
              column: "UserId");
      }

      /// <inheritdoc />
      protected override void Down(MigrationBuilder migrationBuilder)
      {
          migrationBuilder.DropTable(
              name: "EmailAccount");

          migrationBuilder.DropTable(
              name: "EmailRelated");

          migrationBuilder.DropTable(
              name: "EmailTemplate");

          migrationBuilder.DropTable(
              name: "Media");

          migrationBuilder.DropTable(
              name: "MediaBinary");

          migrationBuilder.DropTable(
              name: "Permission");

          migrationBuilder.DropTable(
              name: "QueuedEmail");

          migrationBuilder.DropTable(
              name: "Setting");
      }
  }
