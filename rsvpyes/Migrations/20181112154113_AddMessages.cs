using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace rsvpyes.Migrations
{
    public partial class AddMessages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Organization",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MessageId",
                table: "RsvpRequests",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Fee",
                table: "Meetings",
                nullable: true,
                oldClrType: typeof(decimal));

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Body = table.Column<string>(nullable: true),
                    SendTimestamp = table.Column<DateTime>(nullable: false),
                    SenderUserId = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropColumn(
                name: "Organization",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "MessageId",
                table: "RsvpRequests");

            migrationBuilder.AlterColumn<decimal>(
                name: "Fee",
                table: "Meetings",
                nullable: false,
                oldClrType: typeof(decimal),
                oldNullable: true);
        }
    }
}
