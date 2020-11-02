using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class ContactEntitiesModelContactContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "CNT");

            migrationBuilder.CreateTable(
                name: "ContactInfo",
                schema: "CNT",
                columns: table => new
                {
                    ContactId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(128)", nullable: false),
                    LastName = table.Column<string>(type: "varchar(128)", nullable: false),
                    Email = table.Column<string>(type: "varchar(128)", nullable: false),
                    Phone = table.Column<string>(type: "varchar(15)", nullable: false),
                    Image = table.Column<string>(type: "varchar(500)", nullable: false),
                    WebSite = table.Column<string>(type: "varchar(256)", nullable: false),
                    Note = table.Column<string>(type: "varchar(1000)", nullable: false),
                    Gender = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactInfo", x => x.ContactId);
                });

            migrationBuilder.CreateTable(
                name: "Address",
                schema: "CNT",
                columns: table => new
                {
                    AddressId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ContactId = table.Column<int>(nullable: false),
                    Street1 = table.Column<string>(type: "varchar(256)", nullable: false),
                    Street2 = table.Column<string>(type: "varchar(256)", nullable: true),
                    City = table.Column<string>(type: "varchar(128)", nullable: false),
                    State = table.Column<string>(type: "varchar(128)", nullable: false),
                    Country = table.Column<string>(type: "varchar(128)", nullable: false),
                    PostalCode = table.Column<string>(type: "varchar(128)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.AddressId);
                    table.ForeignKey(
                        name: "FK_Address_ContactInfo_ContactId",
                        column: x => x.ContactId,
                        principalSchema: "CNT",
                        principalTable: "ContactInfo",
                        principalColumn: "ContactId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Address_ContactId",
                schema: "CNT",
                table: "Address",
                column: "ContactId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Address",
                schema: "CNT");

            migrationBuilder.DropTable(
                name: "ContactInfo",
                schema: "CNT");
        }
    }
}
