using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PermissionBasedAuthorizationIntDotNet5.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAdminUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO [security].[Users] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LastName], [ProfilePicture]) VALUES (N'f224514c-158a-4055-92e0-7d7de2005787', N'ratana', N'RATANA', N'ratana@gmail.com', N'RATANA@GMAIL.COM', 0, N'AQAAAAIAAYagAAAAEFZdwKehgeEoy5rYYIwD7tWNgpGyZux/doN3ARmLfYk/TV+PQ649kIYeuT3KDcDIhQ==', N'47ZUJAWQMAR2QP74JCTKA5WSZ3KPYKX7', N'8cf42df6-e756-4641-8e99-8f5dbe92c26e', NULL, 0, 0, NULL, 1, 0, N'Samrith', N'Ratana', NULL)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [security].[Users] WHERE Id = '3c9c4488-124a-4673-94fd-55de592ad748'");
        }
    }
} 
