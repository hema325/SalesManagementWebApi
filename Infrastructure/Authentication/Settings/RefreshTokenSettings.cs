namespace Infrastructure.Authentication.Settings
{
    internal class RefreshTokenSettings
    {
        public const string SectionName = "RefreshToken";
        public int Length { get; init; }
        public double ExpirationInDayes { get; init; }
    }
}
