using System.Security.Cryptography;
using Evently.Core.Helpers;

namespace Evently.Services.Helpers;

public class Sha256PasswordHasher : IPasswordHasher
{
    private const int RehashCount = 10000;

    private const int HashSizeBytes = 16;

    private const int SaltSizeBytes = 16;

    public string HashPassword(string password)
    {
        var saltBytes = RandomNumberGenerator.GetBytes(SaltSizeBytes);

        using var deriveBytes = GetDeriveBytes(password, saltBytes);

        var hashBytes = deriveBytes.GetBytes(HashSizeBytes);

        var fullHashBytes = new byte[SaltSizeBytes + HashSizeBytes];

        Array.Copy(saltBytes, 0, fullHashBytes, 0, SaltSizeBytes);
        Array.Copy(hashBytes, 0, fullHashBytes, SaltSizeBytes, HashSizeBytes);

        var hashedPassword = Convert.ToBase64String(fullHashBytes);

        return hashedPassword;
    }

    public bool Verify(string password, string hashedPassword)
    {
        var fullHashBytes = Convert.FromBase64String(hashedPassword);

        var saltBytes = new byte[SaltSizeBytes];

        Array.Copy(fullHashBytes, 0, saltBytes, 0, SaltSizeBytes);

        using var deriveBytes = GetDeriveBytes(password, saltBytes);

        var hashBytes = deriveBytes.GetBytes(HashSizeBytes);

        var isMatch = CompareHashes(hashBytes, fullHashBytes);

        return isMatch;
    }

    private static bool CompareHashes(byte[] hashBytes, byte[] fullHashBytes)
    {
        for (var i = 0; i < HashSizeBytes; i++)
            if (hashBytes[i] != fullHashBytes[i + SaltSizeBytes])
                return false;

        return true;
    }

    private static Rfc2898DeriveBytes GetDeriveBytes(string password, byte[] saltBytes)
    {
        return new Rfc2898DeriveBytes(password, saltBytes, RehashCount, HashAlgorithmName.SHA256);
    }
}