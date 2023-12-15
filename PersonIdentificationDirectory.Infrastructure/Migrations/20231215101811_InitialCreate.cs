using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonIdentificationDirectory.Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PersonalNumber = table.Column<string>(type: "varchar(11)", unicode: false, maxLength: 11, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "date", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecondaryId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastChangeDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    CreateDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DeleteAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RelationReadModel",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonId = table.Column<long>(type: "bigint", nullable: false),
                    Counter = table.Column<int>(type: "int", nullable: false),
                    RelationType = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelationReadModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonRelation",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    RelatedPersonId = table.Column<long>(type: "bigint", nullable: false),
                    PersonId = table.Column<long>(type: "bigint", nullable: false),
                    SecondaryId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonRelation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonRelation_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonRelation_Person_RelatedPersonId",
                        column: x => x.RelatedPersonId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhoneNumber",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhoneNumberType = table.Column<byte>(type: "tinyint", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersonId = table.Column<long>(type: "bigint", nullable: true),
                    SecondaryId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneNumber", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhoneNumber_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Person_PersonalNumber",
                table: "Person",
                column: "PersonalNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonRelation_PersonId",
                table: "PersonRelation",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonRelation_RelatedPersonId",
                table: "PersonRelation",
                column: "RelatedPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PhoneNumber_PersonId",
                table: "PhoneNumber",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonId_RelationType",
                table: "RelationReadModel",
                columns: new[] { "PersonId", "RelationType" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonRelation");

            migrationBuilder.DropTable(
                name: "PhoneNumber");

            migrationBuilder.DropTable(
                name: "RelationReadModel");

            migrationBuilder.DropTable(
                name: "Person");
        }
    }
}
