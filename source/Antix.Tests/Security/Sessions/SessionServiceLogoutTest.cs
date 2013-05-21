using Antix.Security.Sessions;
using Moq;
using Xunit;

namespace Antix.Tests.Security.Sessions
{
    public class SessionServiceLogoutTest
    {
        [Fact]
        public void logout_succeeds()
        {
            var session = new Session();
            var dataServiceMock = GetDataServiceMock(session);

            var sut = GetServiceUnderTest(dataServiceMock);

            sut.Logout("CORRECT");

            Assert.NotNull(session.LogoutOn);

            dataServiceMock
                .Verify(m => m.TryGet(It.IsAny<string>()), Times.Once());
            dataServiceMock
                .Verify(m => m.Update(It.IsAny<Session>()), Times.Once());
        }

        [Fact]
        public void update_not_called_when_session_not_found()
        {
            var dataServiceMock = GetDataServiceMock(null);
            var sut = GetServiceUnderTest(dataServiceMock);

            sut.Logout("INCORRECT");

            dataServiceMock
                .Verify(m => m.Update(It.IsAny<Session>()), Times.Never());
        }

        static ISessionService GetServiceUnderTest(
            Mock<ISessionDataService> dataServiceMock = null)
        {
            dataServiceMock = dataServiceMock ?? GetDataServiceMock(new Session());

            return new SessionService(
                dataServiceMock.Object,
                new SessionServiceSettings(v => v, 20)
                );
        }

        static Mock<ISessionDataService> GetDataServiceMock(Session session)
        {
            var dataServiceMock = new Mock<ISessionDataService>();
            dataServiceMock
                .Setup(m => m.TryGet(It.IsAny<string>()))
                .Returns(session);

            dataServiceMock
                .Setup(m => m.Update(It.IsAny<Session>()));

            return dataServiceMock;
        }
    }
}