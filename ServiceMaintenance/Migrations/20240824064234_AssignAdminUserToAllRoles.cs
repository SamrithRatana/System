using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PermissionBasedAuthorizationIntDotNet5.Data.Migrations
{
    /// <inheritdoc />
    public partial class AssignAdminUserToAllRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO [security].UserRoles (UserId,RoleId) SELECT 'f224514c-158a-4055-92e0-7d7de2005787', Id FROM security.Roles");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [security].UserRoles WHERE UserId= 'f224514c-158a-4055-92e0-7d7de2005787'");
        }
    }
}
