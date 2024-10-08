namespace ServiceMaintenance.Chat
{
    public static class SupportedVideoFormats
    {
        public static readonly HashSet<string> Formats = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            ".mp4",
            ".avi",
            ".mov",
            ".mkv",
            ".webm",
            // Add other supported video formats here
        };
    }
}
