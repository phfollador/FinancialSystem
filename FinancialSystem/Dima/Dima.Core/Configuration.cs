namespace Dima.Core
{
    public static class Configuration
    {
        public const int DefaultPageSige = 25;
        public const int DefaultPageNumber = 1;
        public const int defaultStatusCode = 200;

        public static string ConnectionString { get; set; } = string.Empty;

        public static string BackendUrl { get; set; }

        public static string FrontendUrl { get; set; }
    }
}
