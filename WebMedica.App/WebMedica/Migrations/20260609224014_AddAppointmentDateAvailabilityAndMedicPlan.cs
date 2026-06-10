using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebMedical.Migrations
{
    /// <inheritdoc />
    public partial class AddAppointmentDateAvailabilityAndMedicPlan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MedicPlan",
                table: "UserProfile",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AppointmentDateAvailability",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    SelectedDays = table.Column<string>(type: "text", nullable: false),
                    StartTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    IntervalMinutes = table.Column<int>(type: "integer", nullable: false),
                    CreatedByUserLoginId_fk = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentDateAvailability", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppointmentDateAvailability_AspNetUsers_CreatedByUserLoginId_fk",
                        column: x => x.CreatedByUserLoginId_fk,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppointmentDateSlot",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Time = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    IsBooked = table.Column<bool>(type: "boolean", nullable: false),
                    AppointmentId_fk = table.Column<int>(type: "integer", nullable: true),
                    AppointmentDateAvailabilityId_fk = table.Column<int>(type: "integer", nullable: false),
                    ProviderUserLoginId_fk = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentDateSlot", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppointmentDateSlot_AppointmentDateAvailability_AppointmentDateAvailabilityId_fk",
                        column: x => x.AppointmentDateAvailabilityId_fk,
                        principalTable: "AppointmentDateAvailability",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppointmentDateSlot_Appointment_AppointmentId_fk",
                        column: x => x.AppointmentId_fk,
                        principalTable: "Appointment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_AppointmentDateSlot_AspNetUsers_ProviderUserLoginId_fk",
                        column: x => x.ProviderUserLoginId_fk,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentDateAvailability_CreatedByUserLoginId_fk",
                table: "AppointmentDateAvailability",
                column: "CreatedByUserLoginId_fk");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentDateSlot_AppointmentDateAvailabilityId_fk",
                table: "AppointmentDateSlot",
                column: "AppointmentDateAvailabilityId_fk");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentDateSlot_AppointmentId_fk",
                table: "AppointmentDateSlot",
                column: "AppointmentId_fk");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentDateSlot_Date_Time_Global",
                table: "AppointmentDateSlot",
                columns: new[] { "Date", "Time" },
                unique: true,
                filter: "\"ProviderUserLoginId_fk\" IS NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentDateSlot_Date_Time_ProviderUserLoginId_fk",
                table: "AppointmentDateSlot",
                columns: new[] { "Date", "Time", "ProviderUserLoginId_fk" },
                unique: true,
                filter: "\"ProviderUserLoginId_fk\" IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentDateSlot_ProviderUserLoginId_fk",
                table: "AppointmentDateSlot",
                column: "ProviderUserLoginId_fk");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppointmentDateSlot");

            migrationBuilder.DropTable(
                name: "AppointmentDateAvailability");

            migrationBuilder.DropColumn(
                name: "MedicPlan",
                table: "UserProfile");
        }
    }
}
