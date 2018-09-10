namespace Actio.Services.Identity.Domain.Services
{
    public interface IEncrypter
    {
        string GetSalt(string value);
        string GetHash(string value, string salt);
        string GetSalt();
    }
}