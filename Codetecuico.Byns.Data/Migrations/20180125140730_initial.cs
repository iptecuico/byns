using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Codetecuico.Byns.Data.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "Users",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        CreatedBy = table.Column<int>(nullable: true),
            //        DateCreated = table.Column<DateTime>(nullable: false),
            //        DateModified = table.Column<DateTime>(nullable: false),
            //        DateRegistered = table.Column<DateTime>(nullable: false),
            //        Email = table.Column<string>(maxLength: 50, nullable: true),
            //        ExternalId = table.Column<string>(nullable: true),
            //        FirstName = table.Column<string>(maxLength: 50, nullable: true),
            //        Image = table.Column<string>(nullable: true),
            //        LastName = table.Column<string>(maxLength: 50, nullable: true),
            //        Location = table.Column<string>(nullable: true),
            //        MobileNumber = table.Column<string>(maxLength: 20, nullable: true),
            //        ModifiedBy = table.Column<int>(nullable: true),
            //        PersonalWebsite = table.Column<string>(maxLength: 50, nullable: true),
            //        Username = table.Column<string>(maxLength: 20, nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Users", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Items",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        Category = table.Column<string>(maxLength: 50, nullable: true),
            //        Condition = table.Column<string>(maxLength: 50, nullable: true),
            //        CreatedBy = table.Column<int>(nullable: true),
            //        Currency = table.Column<string>(maxLength: 10, nullable: true),
            //        DateCreated = table.Column<DateTime>(nullable: false),
            //        DateModified = table.Column<DateTime>(nullable: false),
            //        DatePosted = table.Column<DateTime>(nullable: false),
            //        Description = table.Column<string>(maxLength: 500, nullable: false),
            //        Image = table.Column<string>(nullable: true),
            //        IsSold = table.Column<bool>(nullable: false),
            //        ModifiedBy = table.Column<int>(nullable: true),
            //        Name = table.Column<string>(maxLength: 100, nullable: false),
            //        Price = table.Column<double>(nullable: false),
            //        Remarks = table.Column<string>(maxLength: 1500, nullable: true),
            //        StarCount = table.Column<int>(nullable: false),
            //        Status = table.Column<string>(maxLength: 20, nullable: true),
            //        StockCount = table.Column<int>(nullable: false),
            //        UserId = table.Column<int>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Items", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Items_Users_UserId",
            //            column: x => x.UserId,
            //            principalTable: "Users",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_Items_UserId",
            //    table: "Items",
            //    column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
