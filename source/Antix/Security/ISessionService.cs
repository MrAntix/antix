namespace Antix.Security
{
    public interface ISessionService
    {
        Session Login(string identifier, string password);
        bool Logout(string identifier);
    }
}