namespace ServiceMaintenance.Chat
{
    public static class SupportedImageFormats
    {
        public static readonly HashSet<string> Formats = new HashSet<string>
        {
            ".jpg",
            ".jpeg",
            ".png",
            ".gif",
            ".bmp",
            ".tiff",
            ".webp"
        };
    }
}
