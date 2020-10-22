using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SVX.Migrations
{
    public partial class Servers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Server",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ClientId = table.Column<int>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Hardware = table.Column<string>(nullable: true),
                    Hostname = table.Column<string>(nullable: true),
                    Ip = table.Column<string>(nullable: true),
                    VersionSVX = table.Column<int>(nullable: false),
                    Os = table.Column<string>(nullable: true),
                    Database = table.Column<string>(nullable: true),
                    Perl = table.Column<string>(nullable: true),
                    Application = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Server", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Server_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Server_ClientId",
                table: "Server",
                column: "ClientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Server");
        }
    }
}
