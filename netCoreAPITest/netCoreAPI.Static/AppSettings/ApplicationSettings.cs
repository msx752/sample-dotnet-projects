namespace netCoreAPI.Static.AppSettings
{
    public partial class ApplicationSettings
    {
        public Logging Logging { get; set; }
        public string AllowedHosts { get; set; }
        public string ASPNETCORE_ENVIRONMENT { get; set; }
    }
}