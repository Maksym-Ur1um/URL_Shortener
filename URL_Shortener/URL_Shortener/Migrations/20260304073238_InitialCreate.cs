using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace URL_Shortener.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PageContents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PageName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TextContent = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PageContents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShortenedUrls",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OriginalUrl = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ShortUrl = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShortenedUrls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShortenedUrls_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "PageContents",
                columns: new[] { "Id", "PageName", "TextContent" },
                values: new object[] { 1, "About", "Our application uses a highly efficient mathematical approach called Base62 Encoding. When you submit a long URL, the system first securely saves it into our database and assigns it a unique numeric ID. We then take this standard base-10 numeric ID and convert it into a Base62 string. The Base62 alphabet consists of 62 characters: lowercase letters (a-z), uppercase letters (A-Z), and digits (0-9). This conversion guarantees that every generated short link is absolutely unique, collision-free, and as short as mathematically possible." });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "PasswordHash", "Role", "UserName" },
                values: new object[,]
                {
                    { 1, "$2a$11$qtEm9WNPMHdEIncqv1ixk.RzjXsWNrlj4aFVR8YHOSMvovOilksfy", 1, "Ivan" },
                    { 2, "$2a$11$1kz2UdYbg02yM4sLj2l/Be179mbx90kr765RahKV7lmCw56azMtuy", 0, "Admin" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShortenedUrls_OriginalUrl",
                table: "ShortenedUrls",
                column: "OriginalUrl",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShortenedUrls_ShortUrl",
                table: "ShortenedUrls",
                column: "ShortUrl",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShortenedUrls_UserId",
                table: "ShortenedUrls",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PageContents");

            migrationBuilder.DropTable(
                name: "ShortenedUrls");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
