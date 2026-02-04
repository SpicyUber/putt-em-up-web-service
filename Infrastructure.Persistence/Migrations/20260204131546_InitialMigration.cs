using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.Sql(@"
            CREATE FUNCTION dbo.GetMMR(@PlayerID BIGINT)
            RETURNS INT
            AS
            BEGIN
            DECLARE @TotalMMR INT;

            SELECT @TotalMMR = COALESCE(SUM(MMRDelta), 0)
            FROM MatchPerformances
            WHERE PlayerID = @PlayerID;

            RETURN @TotalMMR;
            END
             
             ");

            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    MatchID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Cancelled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.MatchID);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    PlayerID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MatchmakingRanking = table.Column<int>(type: "int", nullable: false, computedColumnSql: "dbo.GetMMR([PlayerID])"),
                    AccountDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AvatarFilePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.PlayerID);
                });

            migrationBuilder.CreateTable(
                name: "MatchPerformances",
                columns: table => new
                {
                    PlayerID = table.Column<long>(type: "bigint", nullable: false),
                    MatchID = table.Column<long>(type: "bigint", nullable: false),
                    WonMatch = table.Column<bool>(type: "bit", nullable: false),
                    FinalScore = table.Column<int>(type: "int", nullable: false),
                    MMRDelta = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchPerformances", x => new { x.PlayerID, x.MatchID });
                    table.ForeignKey(
                        name: "FK_MatchPerformances_Matches_MatchID",
                        column: x => x.MatchID,
                        principalTable: "Matches",
                        principalColumn: "MatchID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MatchPerformances_Players_PlayerID",
                        column: x => x.PlayerID,
                        principalTable: "Players",
                        principalColumn: "PlayerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    FromPlayerID = table.Column<long>(type: "bigint", nullable: false),
                    ToPlayerID = table.Column<long>(type: "bigint", nullable: false),
                    SentTimestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Reported = table.Column<bool>(type: "bit", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => new { x.FromPlayerID, x.ToPlayerID, x.SentTimestamp });
                    table.ForeignKey(
                        name: "FK_Messages_Players_FromPlayerID",
                        column: x => x.FromPlayerID,
                        principalTable: "Players",
                        principalColumn: "PlayerID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Messages_Players_ToPlayerID",
                        column: x => x.ToPlayerID,
                        principalTable: "Players",
                        principalColumn: "PlayerID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Matches",
                columns: new[] { "MatchID", "Cancelled", "StartDate" },
                values: new object[,]
                {
                    { 1L, false, new DateTime(2012, 3, 3, 13, 20, 20, 0, DateTimeKind.Unspecified) },
                    { 2L, false, new DateTime(2022, 7, 2, 12, 22, 33, 0, DateTimeKind.Unspecified) },
                    { 3L, false, new DateTime(2023, 7, 2, 12, 22, 33, 0, DateTimeKind.Unspecified) },
                    { 4L, false, new DateTime(2023, 3, 2, 12, 22, 33, 0, DateTimeKind.Unspecified) },
                    { 5L, false, new DateTime(2024, 12, 11, 12, 22, 33, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "PlayerID", "AccountDeleted", "AvatarFilePath", "Description", "DisplayName", "Password", "Username" },
                values: new object[,]
                {
                    { 1L, false, "", "My Description.", "Aleksandar", "admin!", "ila" },
                    { 2L, false, "magnus", "I am very good at this game.", "Magnus", "123456", "magnus" },
                    { 3L, false, "", "I am very bad at this game.", "Bob", "123abc", "bob" }
                });

            migrationBuilder.InsertData(
                table: "MatchPerformances",
                columns: new[] { "MatchID", "PlayerID", "FinalScore", "MMRDelta", "WonMatch" },
                values: new object[,]
                {
                    { 2L, 1L, 3, 100, true },
                    { 3L, 1L, 3, 100, true },
                    { 4L, 1L, 0, -100, false },
                    { 1L, 2L, 3, 100, true },
                    { 3L, 2L, 0, -100, false },
                    { 4L, 2L, 3, 100, true },
                    { 5L, 2L, 3, 100, true },
                    { 1L, 3L, 0, -100, false },
                    { 2L, 3L, 0, -100, false },
                    { 5L, 3L, 0, -100, false }
                });

            migrationBuilder.InsertData(
                table: "Messages",
                columns: new[] { "FromPlayerID", "SentTimestamp", "ToPlayerID", "Content", "Reported" },
                values: new object[,]
                {
                    { 1L, new DateTime(2012, 3, 3, 1, 33, 12, 0, DateTimeKind.Unspecified), 2L, "ez", false },
                    { 1L, new DateTime(2022, 3, 3, 1, 34, 23, 0, DateTimeKind.Unspecified), 2L, "hello", false },
                    { 1L, new DateTime(2022, 3, 3, 1, 33, 12, 0, DateTimeKind.Unspecified), 3L, "it's been 10 years, lets play?", false },
                    { 2L, new DateTime(2012, 3, 3, 1, 34, 22, 0, DateTimeKind.Unspecified), 1L, ":(", false },
                    { 2L, new DateTime(2022, 3, 3, 1, 34, 22, 0, DateTimeKind.Unspecified), 1L, "How do I block you??", false },
                    { 2L, new DateTime(2022, 3, 3, 1, 33, 13, 0, DateTimeKind.Unspecified), 3L, "hi", false }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MatchPerformances_MatchID",
                table: "MatchPerformances",
                column: "MatchID");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ToPlayerID",
                table: "Messages",
                column: "ToPlayerID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MatchPerformances");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Matches");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.Sql("DROP FUNCTION dbo.GetMMR ");
        }
    }
}
