namespace Project.Domain.Common.Interfaces;

public interface IPasswordEncryptor
{
    string Encrypt (string plainText);
    string Decrypt (string cipherText);
}