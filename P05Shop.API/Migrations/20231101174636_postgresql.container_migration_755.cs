using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace P05Shop.API.Migrations
{
    /// <inheritdoc />
    public partial class postgresqlcontainer_migration_755 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarBrands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    OriginCountry = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarBrands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Model = table.Column<string>(type: "text", nullable: false),
                    Power = table.Column<int>(type: "integer", nullable: false),
                    CarBrandId = table.Column<int>(type: "integer", nullable: false),
                    PreviousOwnerId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cars_CarBrands_CarBrandId",
                        column: x => x.CarBrandId,
                        principalTable: "CarBrands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cars_People_PreviousOwnerId",
                        column: x => x.PreviousOwnerId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CarBrands",
                columns: new[] { "Id", "Name", "OriginCountry" },
                values: new object[,]
                {
                    { 1, "Jaguar", "Japan" },
                    { 2, "Honda", "Yemen" },
                    { 3, "Porsche", "Bermuda" },
                    { 4, "Chrysler", "Poland" },
                    { 5, "Toyota", "Anguilla" },
                    { 6, "Hyundai", "Lesotho" },
                    { 7, "Jeep", "Finland" },
                    { 8, "Fiat", "Guinea" },
                    { 9, "Honda", "Ukraine" },
                    { 10, "Bugatti", "Andorra" },
                    { 11, "Rolls Royce", "Germany" },
                    { 12, "Ford", "Australia" },
                    { 13, "Cadillac", "Virgin Islands, British" },
                    { 14, "Lamborghini", "Ukraine" },
                    { 15, "Dodge", "Solomon Islands" },
                    { 16, "Porsche", "Sao Tome and Principe" },
                    { 17, "Ferrari", "Netherlands" },
                    { 18, "Kia", "Virgin Islands, U.S." },
                    { 19, "Cadillac", "Turkmenistan" },
                    { 20, "Dodge", "Thailand" },
                    { 21, "Bentley", "Djibouti" },
                    { 22, "Chevrolet", "Aruba" },
                    { 23, "Chevrolet", "Faroe Islands" },
                    { 24, "BMW", "Northern Mariana Islands" },
                    { 25, "Hyundai", "El Salvador" }
                });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "Id", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "Jody Welch", "1-288-867-1127" },
                    { 2, "Allen Pfannerstill", "542.760.3020" },
                    { 3, "Miranda Stiedemann", "1-815-206-8660 x7061" },
                    { 4, "Angela Wiegand", "428.924.1945" },
                    { 5, "Cedric Gaylord", "(623) 727-5335" },
                    { 6, "Becky Crist", "(757) 768-8817 x29416" },
                    { 7, "Cecilia Collier", "208-902-5669" },
                    { 8, "Jorge Ebert", "731-984-1561" },
                    { 9, "Eric Monahan", "(656) 689-5335 x9272" },
                    { 10, "Marc Rowe", "248-999-2545" },
                    { 11, "Rickey Borer", "951-749-9846" },
                    { 12, "Alejandro Greenfelder", "(380) 593-4810 x0519" },
                    { 13, "Kristine Frami", "630-401-7646 x97758" },
                    { 14, "Jeannie Mohr", "(425) 672-6787 x18301" },
                    { 15, "Calvin Weissnat", "939-275-6526 x65504" },
                    { 16, "Ramona Erdman", "719.949.1999 x94483" },
                    { 17, "Dave Gislason", "1-770-501-2362" },
                    { 18, "Robert Witting", "753-934-1482 x01292" },
                    { 19, "Ted Kerluke", "908-320-7189 x0777" },
                    { 20, "Jimmy Boyle", "1-677-501-8964" },
                    { 21, "Luz Klocko", "836.971.4523 x325" },
                    { 22, "Pete Lakin", "1-860-390-6697" },
                    { 23, "Wanda Hoeger", "495-653-5319" },
                    { 24, "Darrin Davis", "541-902-0514" },
                    { 25, "Wendy Witting", "727-493-9137" }
                });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "CarBrandId", "Model", "Power", "PreviousOwnerId" },
                values: new object[,]
                {
                    { 1, 22, "Bugatti", 1954798937, 9 },
                    { 2, 21, "Bugatti", 991154489, 21 },
                    { 3, 3, "Lamborghini", 1411729325, 6 },
                    { 4, 20, "Mazda", 866930202, 10 },
                    { 5, 7, "Cadillac", 554013003, 24 },
                    { 6, 22, "Maserati", 949138117, 23 },
                    { 7, 1, "Smart", 2029260696, 18 },
                    { 8, 8, "Volvo", 966978416, 10 },
                    { 9, 12, "Tesla", 165085211, 9 },
                    { 10, 21, "Polestar", 1924951637, 14 },
                    { 11, 2, "Bentley", 1334382263, 2 },
                    { 12, 24, "Mazda", 1240494303, 19 },
                    { 13, 24, "Aston Martin", 1891364421, 25 },
                    { 14, 11, "Mazda", 214606913, 6 },
                    { 15, 10, "Lamborghini", 256742547, 12 },
                    { 16, 23, "Fiat", 1499469656, 5 },
                    { 17, 8, "Rolls Royce", 1040844130, 21 },
                    { 18, 24, "Maserati", 7870134, 2 },
                    { 19, 14, "Rolls Royce", 1783272972, 9 },
                    { 20, 13, "Bugatti", 1479763268, 18 },
                    { 21, 16, "Volkswagen", 682772295, 1 },
                    { 22, 15, "Fiat", 1785880164, 19 },
                    { 23, 19, "Chrysler", 74386829, 23 },
                    { 24, 4, "Volvo", 1374334807, 9 },
                    { 25, 10, "Mercedes Benz", 706744980, 10 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_CarBrandId",
                table: "Cars",
                column: "CarBrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_PreviousOwnerId",
                table: "Cars",
                column: "PreviousOwnerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "CarBrands");

            migrationBuilder.DropTable(
                name: "People");
        }
    }
}
