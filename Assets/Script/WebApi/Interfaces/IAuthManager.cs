// Script/Interfaces/IAuthManager.cs
using System.Threading.Tasks;

public interface IAuthManager
{
    Task<bool> Login(string username, string password);
    void Logout();
    bool IsLoggedIn();
    string GetAuthToken();
}