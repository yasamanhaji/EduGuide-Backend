namespace Base.Application.Contracts
{
    public interface IEmailService
    {
        void SendEmail(string verificationCode, string toEmail, string subject);
    }
}
