using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebMedical.Migrations
{
    /// <inheritdoc />
    public partial class AddUserProfileRelationToUserLogin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserProfileId_fk",
                table: "AspNetUsers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "UserProfile",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserLoginId_pk = table.Column<string>(type: "text", nullable: true),
                    Guid = table.Column<Guid>(type: "uuid", nullable: false),
                    SocialSecurityNumber = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    MiddleName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    LastName2 = table.Column<string>(type: "text", nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: false),
                    FisicalAddress = table.Column<string>(type: "text", nullable: false),
                    FisicalAddressLine2 = table.Column<string>(type: "text", nullable: false),
                    Town = table.Column<int>(type: "integer", nullable: false),
                    State = table.Column<int>(type: "integer", nullable: false),
                    Zipcode = table.Column<int>(type: "integer", nullable: false),
                    PostalAddress = table.Column<string>(type: "text", nullable: false),
                    PostalAddressLine2 = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsRegister = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfile", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserProfileId_fk",
                table: "AspNetUsers",
                column: "UserProfileId_fk",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_UserProfile_UserProfileId_fk",
                table: "AspNetUsers",
                column: "UserProfileId_fk",
                principalTable: "UserProfile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_UserProfile_UserProfileId_fk",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "UserProfile");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UserProfileId_fk",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserProfileId_fk",
                table: "AspNetUsers");
        }
    }
}
