namespace Antix.Security
{
    public interface IUserService
    {
        Session Login(string identifier, string password);
        bool Logout(string identifier);
    }
}