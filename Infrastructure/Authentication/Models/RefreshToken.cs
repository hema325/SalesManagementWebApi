namespace Infrastructure.Authentication.Models
{
    internal class RefreshToken
    {
        public string Value { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ExpiresOn { get; set; }
        public DateTime? RevokedOn { get; set; }
    }
}
