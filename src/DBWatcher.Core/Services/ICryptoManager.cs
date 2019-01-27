namespace DBWatcher.Core.Services
{
    public interface ICryptoManager
    {
        string Encrypt(string data);
        string Decrypt(string encryptedData);
    }
}