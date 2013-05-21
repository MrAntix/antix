using System;
using System.Diagnostics.Contracts;

namespace Antix.Security.Sessions
{
    public class SessionService :
        ISessionService
    {
        readonly ISessionDataService _dataService;
        readonly SessionServiceSettings _settings;

        public SessionService(
            ISessionDataService dataService, SessionServiceSettings settings)
        {
            _dataService = dataService;
            _settings = settings;
        }

        public Session Login(string identifier, string password)
        {
            Contract.Ensures(!string.IsNullOrWhiteSpace(identifier));
            Contract.Ensures(!string.IsNullOrWhiteSpace(password));

            var passwordHashed = _settings.Hash(password);
            var user = _dataService.TryGetUser(identifier, passwordHashed);

            if (user == null) return null;

            var session = new Session
                {
                    Identifier = Guid.NewGuid().ToString("D"),
                    ExpiresOn = DateTime.UtcNow.AddMinutes(_settings.ExpireMinutes),
                    LoginOn = DateTime.UtcNow,
                    User = user
                };

            _dataService.Add(session);

            return session;
        }

        public void Logout(string identifier)
        {
            Contract.Ensures(!string.IsNullOrWhiteSpace(identifier));

            var session = _dataService.TryGet(identifier);

            if (session == null) return;

            session.LogoutOn = DateTime.UtcNow;
            _dataService.Update(session);
        }
    }
}