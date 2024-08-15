using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using Project.Domain.Common.Interfaces;

namespace Project.Infrastructure.Authentication.PasswordEncryption;

public class PasswordEncryptor: IPasswordEncryptor
{
    private readonly string _key;

    public PasswordEncryptor (IOptions<PasswordEncryptionSettings> options)
    {
        var passwordEncryptionSettings = options.Value;
        _key = passwordEncryptionSettings.Key;
    }
    
    public string Encrypt (string plainText)
    {
        //TODO:
        throw new NotImplementedException();
    }

    public string Decrypt (string cipherText)
    {
        //TODO:
        throw new NotImplementedException();
    }
}