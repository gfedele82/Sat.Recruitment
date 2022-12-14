using Microsoft.EntityFrameworkCore.Migrations;
using Sat.Recruitment.Common;

#nullable disable

namespace Sat.Recruitment.DataAccess.Migrations
{
    public partial class AddDefault : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Money = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.Sql($"INSERT INTO {DBTables.DBUsers} (Name,Email,Address,Phone,UserType,Money) Values ('Juan','Juan@marmol.com','+5491154762312','Peru 2464','Normal',1234)");
            migrationBuilder.Sql($"INSERT INTO {DBTables.DBUsers} (Name,Email,Address,Phone,UserType,Money) Values ('Franco','Franco.Perez@gmail.com','+534645213542','Alvear y Colombres','Premium',112234)");
            migrationBuilder.Sql($"INSERT INTO {DBTables.DBUsers} (Name,Email,Address,Phone,UserType,Money) Values ('Agustina','Agustina@gmail.com','+534645213542','Garay y Otra Calle','SuperUser',112234)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
