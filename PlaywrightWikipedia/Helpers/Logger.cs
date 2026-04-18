namespace PlaywrightWikipedia.Helpers
{
    public static class Logger
    {
        private static string _logFilePath;

        public static void Initialize(string testName)
        {
            var projectRoot = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\"));
            var logsFolder = Path.Combine(projectRoot, "Logs");
            Directory.CreateDirectory(logsFolder);

            var timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            _logFilePath = Path.Combine(logsFolder, $"{testName}_{timestamp}.txt");
        }

        public static void Log(string message)
        {
            var line = $"[{DateTime.Now:HH:mm:ss}] {message}";
            Console.WriteLine(line);
            File.AppendAllText(_logFilePath, line + Environment.NewLine);
        }
    }
}