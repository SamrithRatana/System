namespace ServiceMaintenance.Chat
{
    public static class SupportedAudioFormats
    {
        public static readonly HashSet<string> Formats = new HashSet<string>
        {
            ".mp3",
            ".wav",
            ".aac",
            ".ogg",
            ".flac",
            ".m4a",
            ".wma",
            ".amr",
            ".aiff",
            ".opus"
            // Add other supported audio formats here
        };
    }
}
