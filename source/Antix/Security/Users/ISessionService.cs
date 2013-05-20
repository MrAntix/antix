namespace Antix.Security.Users
{
    public interface ISessionService
    {
        Session Login(string identifier, string password);
        void Logout(string identifier);
    }
}