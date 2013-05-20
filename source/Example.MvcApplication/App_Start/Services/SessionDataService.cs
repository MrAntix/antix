using System;
using System.Linq;
using Antix.Security.Users;
using Example.MvcApplication.App_Start.Data;

namespace Example.MvcApplication.App_Start.Services
{
    public class SessionDataService :
        ISessionDataService
    {
        readonly DataContext _dataContext;

        public SessionDataService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public SessionUser TryGetUser(string identifier, string passwordHash)
        {
            return _dataContext
                .Users
                .Where(e => e.Email == identifier && e.Password == passwordHash)
                .Select(e => new SessionUser
                    {
                        Identifier = e.Email,
                        Name = e.Name
                    })
                .SingleOrDefault();
        }

        public void Add(Session session)
        {
            var user = _dataContext
                .Users
                .Single(e => e.Email == session.User.Identifier);

            _dataContext.UserSessions.Add(
                new UserSessionData
                    {
                        Id = Guid.Parse(session.Identifier),
                        User = user,
                        ExpiresOn = session.ExpiresOn,
                        LoginOn = session.LoginOn
                    });
        }

        public Session TryGet(string identifier)
        {
            var id = Guid.Parse(identifier);

            return _dataContext
                .UserSessions
                .Where(e => e.Id == id)
                .Select(e => new Session
                    {
                        Identifier = identifier,
                        ExpiresOn = e.ExpiresOn,
                        LoginOn = e.LoginOn,
                        LogoutOn = e.LogoutOn,
                        User = new SessionUser
                            {
                                Identifier = e.User.Email,
                                Name = e.User.Name
                            }
                    })
                .FirstOrDefault();
        }

        public void Update(Session session)
        {
            var id = Guid.Parse(session.Identifier);

            var userSession = _dataContext
                .UserSessions
                .Single(e => e.Id == id);

            userSession.ExpiresOn = session.ExpiresOn;
            userSession.LoginOn = session.LoginOn;
            userSession.LogoutOn = session.LogoutOn;

            _dataContext.SaveChanges();
        }
    }
}