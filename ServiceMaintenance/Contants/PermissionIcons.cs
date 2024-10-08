namespace ServiceMaintenance.Contants
{
    public static class PermissionIcons
    {
        public static Dictionary<string, string> Icons = new Dictionary<string, string>
    {
        { "Access", "fas fa-lock" },        // Access icon
        { "View", "fas fa-eye" },          // View icon
        { "Create", "fas fa-plus" },        // Create icon
        { "Edit", "fas fa-edit" },          // Edit icon
        { "Delete", "fas fa-trash" }        // Delete icon
    };

        public static string GetIcon(string permission)
        {
            var key = permission.Split('.').Last(); // Extract the permission type
            return Icons.ContainsKey(key) ? Icons[key] : "fas fa-question"; // Default icon for unknown permissions
        }
    }
}
