using SpaceXunit.Base.Test;
using SpaceXunit.Base.Test.Helpers;
using SpaceXunit.Controllers;

namespace SpaceXunit.Test.Controller;

[CollectionDefinition("UserCollection")]
public class UserControllerTests : BaseTest<UserController>
{
    private UserController _sut;

    public UserControllerTests()
    {
        _sut = new UserController(MapperMock.Object);
    }

    [Fact]
    [Trait("UserController", "IpAddress")]
    public void GetIpAddress_ReturnsForwardedForHeader()
    {
        using (var mock = AutoMock.GetLoose())
        using (var scope = new AssertionScope())
        {
            // Arrange
            var context = new DefaultHttpContext();
            context.Request.Headers["X-Forwarded-For"] = "192.168.1.1, 192.168.1.2";
            _sut.ControllerContext = new ControllerContext
            {
                HttpContext = context
            };

            var expectedIpAddress = "192.168.1.1, 192.168.1.2";

            // Act
            var (Method, Parameters) = MethodHelper.GetPrivateMethodInfo<UserController>("GetIpAddress");
            var result = Method?.Invoke(_sut, Parameters);

            // Assert
            result.Should().BeSameAs(expectedIpAddress);
        }
    }

    [Fact]
    [Trait("UserController", "IpAddress")]
    public void GetIpAddress_ReturnsRemoteIpAddress_WhenForwardedForNotPresent()
    {
        using (var mock = AutoMock.GetLoose())
        using (var scope = new AssertionScope())
        {
            // Arrange
            var context = new DefaultHttpContext();
            context.Connection.RemoteIpAddress = new System.Net.IPAddress(new byte[] { 192, 168, 1, 1 });
            _sut.ControllerContext = new ControllerContext
            {
                HttpContext = context
            };

            var expectedRemoteIpAddress = "192.168.1.1";

            // Act
            var (Method, Parameters) = MethodHelper.GetPrivateMethodInfo<UserController>("GetIpAddress");
            var result = Method?.Invoke(_sut, Parameters);

            // Assert
            result.Should().Be(expectedRemoteIpAddress);
        }
    }

    [Fact]
    [Trait("UserController", "IpAddress")]
    public void GetIpAddress_ReturnsEmptyString_WhenContextNull()
    {
        using (var mock = AutoMock.GetLoose())
        using (var scope = new AssertionScope())
        {
            // Act
            var (Method, Parameters) = MethodHelper.GetPrivateMethodInfo<UserController>("GetIpAddress");
            var result = Method?.Invoke(_sut, Parameters);

            // Assert
            result.Should().Be(string.Empty);
        }
    }
}