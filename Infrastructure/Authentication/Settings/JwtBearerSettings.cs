namespace Infrastructure.Authentication.Settings
{
    internal class JwtBearerSettings
    {
        public const string SectionName = "Jwt";

        public string Key { get; init; }
        public string Issuer { get; init; }
        public string Audience { get; init; }
        public double DurationInMinutes { get; init; } 
    }
}
