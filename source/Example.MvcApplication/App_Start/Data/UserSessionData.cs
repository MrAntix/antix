using System;

namespace Example.MvcApplication.App_Start.Data
{
    public class UserSessionData
    {
        public Guid Id { get; set; }
        public UserData User { get; set; }

        public DateTime LoginOn { get; set; }
        public DateTime? LogoutOn { get; set; }

        public DateTime ExpiresOn { get; set; }
    }
}