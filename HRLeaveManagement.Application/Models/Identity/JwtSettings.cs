namespace HRLeaveManagement.Application.Models.Identity
{
    public class JwtSettings
    {
        public required string Key { get; set; }
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public double DurationInMinutes { get; set; }
    }
}
