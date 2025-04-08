
namespace TestProject.BaseService.Helpers;

public class PasswordHelper
{
    private const string _key = "e3ba3eeb - 032a-472d-8f90-2e18e127f4t2";
    public static string Hash(string passowrd)
    {
        string hash = BCrypt.Net.BCrypt.HashPassword(passowrd + _key);
        return hash;
    }
    public static bool Verify(string passowrd, string hash)
    {
        var result = BCrypt.Net.BCrypt.Verify(passowrd + _key, hash);
        return result;
    }
}
