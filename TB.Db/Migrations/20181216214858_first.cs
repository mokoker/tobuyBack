using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using NpgsqlTypes;

namespace TB.Db.Migrations
{
    public partial class first : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: true),
                    ParentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Categories_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    UserName = table.Column<string>(nullable: true),
                    PassHash = table.Column<string>(nullable: true),
                    UserRoles = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ads",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Title = table.Column<string>(nullable: true),
                    CategoryId = table.Column<int>(nullable: false),
                    PosterId = table.Column<int>(nullable: false),
                    Message = table.Column<string>(nullable: true),
                    City = table.Column<int>(nullable: false),
                    PostDate = table.Column<DateTime>(nullable: false),
                    SearchVector = table.Column<NpgsqlTsVector>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ads", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ads_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ads_Users_PosterId",
                        column: x => x.PosterId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ads_CategoryId",
                table: "Ads",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Ads_PosterId",
                table: "Ads",
                column: "PosterId");

            migrationBuilder.CreateIndex(
                name: "IX_Ads_SearchVector",
                table: "Ads",
                column: "SearchVector")
                .Annotation("Npgsql:IndexMethod", "GIN");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentId",
                table: "Categories",
                column: "ParentId");

            migrationBuilder.Sql(
             @"CREATE TRIGGER product_search_vector_update BEFORE INSERT OR UPDATE
              ON ""Ads"" FOR EACH ROW EXECUTE PROCEDURE
              tsvector_update_trigger(""SearchVector"", 'pg_catalog.turkish', ""Title"", ""Message"");");
            migrationBuilder.InsertData(
            table: "Categories",
            columns: new[] { "Name","ParentId" },
            values: new object[] { "Parent", null });

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ads");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
