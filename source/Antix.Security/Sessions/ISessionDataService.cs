namespace Antix.Security.Sessions
{
    public interface ISessionDataService
    {
        SessionUser TryGetUser(string identifier, string passwordHash);
        void Add(Session session);
        Session TryGet(string identifier);
        void Update(Session session);
    }
}