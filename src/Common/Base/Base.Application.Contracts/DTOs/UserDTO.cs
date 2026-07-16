namespace Base.Application.Contracts.DTOs
{
    public class UserDTO
    {
        public long Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Email { get; set; }
        public string AccessToken { get; set; }
        public DateTime AccessTokenExpirtTime { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime RefreshTokenExpiryTime { get; set; }
        public bool IsProfileComplete { get; set; }

    }
}