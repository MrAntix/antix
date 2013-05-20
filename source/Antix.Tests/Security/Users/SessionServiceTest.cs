using Antix.Security.Users;
using Moq;
using Xunit;

namespace Antix.Tests.Security.Users
{
    public class SessionServiceTest
    {
        const string UserIdentifier = "UserIdentifier";
        const string UserName = "User Name";
        const string UserPassword = "UserPassword";

        [Fact]
        public void login_succeeds()
        {
            var dataServiceMock = GetDataServiceMock();
            var sut = GetServiceUnderTest(dataServiceMock);

            var result = sut.Login(UserIdentifier, UserPassword);

            Assert.NotNull(result);

            dataServiceMock.Verify();
        }

        [Fact]
        public void login_fails_if_identifer_is_incorrect()
        {
            var sut = GetServiceUnderTest();

            var result = sut.Login("INCORRECT", UserPassword);

            Assert.Null(result);
        }

        [Fact]
        public void login_fails_if_password_is_incorrect()
        {
            var sut = GetServiceUnderTest();

            var result = sut.Login(UserIdentifier, "INCORRECT");

            Assert.Null(result);
        }

        static ISessionService GetServiceUnderTest(
            Mock<ISessionDataService> dataServiceMock = null)
        {
            dataServiceMock = dataServiceMock ?? GetDataServiceMock();

            return new SessionService(
                dataServiceMock.Object,
                new SessionServiceSettings(v => v, 20)
                );
        }

        static Mock<ISessionDataService> GetDataServiceMock()
        {
            var user = new SessionUser
                {
                    Identifier = UserIdentifier,
                    Name = UserName
                };

            var dataServiceMock = new Mock<ISessionDataService>();
            dataServiceMock
                .Setup(m => m.TryGetUser(It.IsAny<string>(), It.IsAny<string>()))
                .Returns((string id, string password) =>
                         id == UserIdentifier && password == UserPassword ? user : null)
                .Verifiable();

            dataServiceMock
                .Setup(m => m.Add(It.IsAny<Session>()))
                .Verifiable();

            return dataServiceMock;
        }
    }
}