using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SignInUserService.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PotentialCustomer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CountryCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublicIP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OTP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsOTPValidated = table.Column<bool>(type: "bit", nullable: true),
                    OtpExpiryTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OTPGenAttemptForMobileNumberCount = table.Column<int>(type: "int", nullable: true),
                    CurrentOTPValidationAttempt = table.Column<int>(type: "int", nullable: false),
                    OTPGenAttemptForPublicIpCount = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PotentialCustomer", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PotentialCustomer");
        }
    }
}
