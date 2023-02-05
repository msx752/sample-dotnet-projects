using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Payment.Database.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TransactionEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CartId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TotalCalculatedPrice = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransactionItemEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductPrice = table.Column<double>(type: "float", nullable: false),
                    ProductPriceCurrency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    CalculatedPrice = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransactionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionItemEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransactionItemEntity_TransactionEntity_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "TransactionEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransactionItemEntity_TransactionId",
                table: "TransactionItemEntity",
                column: "TransactionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransactionItemEntity");

            migrationBuilder.DropTable(
                name: "TransactionEntity");
        }
    }
}