using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Movie.Database.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoryEntity",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DirectorEntity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DirectorEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RatingEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AverageRating = table.Column<double>(type: "float", nullable: false),
                    NumVotes = table.Column<long>(type: "bigint", nullable: false),
                    MovieId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RatingEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WriterEntity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WriterEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MovieEntity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RuntimeMinutes = table.Column<int>(type: "int", nullable: false),
                    StartYear = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RatingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    ProductDatabase = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsdPrice = table.Column<double>(type: "float", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovieEntity_RatingEntity_RatingId",
                        column: x => x.RatingId,
                        principalTable: "RatingEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MovieCategoryEntity",
                columns: table => new
                {
                    MovieId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CategoryId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieCategoryEntity", x => new { x.MovieId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_MovieCategoryEntity_CategoryEntity_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "CategoryEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieCategoryEntity_MovieEntity_MovieId",
                        column: x => x.MovieId,
                        principalTable: "MovieEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MovieDirectorEntity",
                columns: table => new
                {
                    MovieId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DirectorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieDirectorEntity", x => new { x.MovieId, x.DirectorId });
                    table.ForeignKey(
                        name: "FK_MovieDirectorEntity_DirectorEntity_DirectorId",
                        column: x => x.DirectorId,
                        principalTable: "DirectorEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieDirectorEntity_MovieEntity_MovieId",
                        column: x => x.MovieId,
                        principalTable: "MovieEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MovieWriterEntity",
                columns: table => new
                {
                    MovieId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    WriterId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieWriterEntity", x => new { x.MovieId, x.WriterId });
                    table.ForeignKey(
                        name: "FK_MovieWriterEntity_MovieEntity_MovieId",
                        column: x => x.MovieId,
                        principalTable: "MovieEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieWriterEntity_WriterEntity_WriterId",
                        column: x => x.WriterId,
                        principalTable: "WriterEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovieCategoryEntity_CategoryId",
                table: "MovieCategoryEntity",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieDirectorEntity_DirectorId",
                table: "MovieDirectorEntity",
                column: "DirectorId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieEntity_RatingId",
                table: "MovieEntity",
                column: "RatingId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieWriterEntity_WriterId",
                table: "MovieWriterEntity",
                column: "WriterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovieCategoryEntity");

            migrationBuilder.DropTable(
                name: "MovieDirectorEntity");

            migrationBuilder.DropTable(
                name: "MovieWriterEntity");

            migrationBuilder.DropTable(
                name: "CategoryEntity");

            migrationBuilder.DropTable(
                name: "DirectorEntity");

            migrationBuilder.DropTable(
                name: "MovieEntity");

            migrationBuilder.DropTable(
                name: "WriterEntity");

            migrationBuilder.DropTable(
                name: "RatingEntity");
        }
    }
}
