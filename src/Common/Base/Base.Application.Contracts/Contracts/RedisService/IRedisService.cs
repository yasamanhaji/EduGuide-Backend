namespace Base.Application.Contracts
{
    public interface IRedisService
    {
        Task SetVerificationCodeAsync(string email, string code, TimeSpan expiration);
        Task<string> GetVerificationCodeAsync(string email);
        Task RemoveVerificationCodeAsync(string email);
    }
}
