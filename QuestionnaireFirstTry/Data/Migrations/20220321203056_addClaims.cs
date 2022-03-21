using Microsoft.EntityFrameworkCore.Migrations;

namespace QuestionnaireFirstTry.Data.Migrations
{
    public partial class addClaims : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Claims",
                columns: new[] { "Id", "Type", "Value" },
                values: new object[,]
                {
                    { 1, "View Question", null },
                    { 2, "Edit Question", null },
                    { 3, "Remove Question", null },
                    { 4, "Add Question", null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Claims",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Claims",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Claims",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Claims",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
