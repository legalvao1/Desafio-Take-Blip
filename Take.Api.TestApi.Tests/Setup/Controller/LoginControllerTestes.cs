using System;

using FluentAssertions;

using Take.Api.BancoPanCartoes.Models.Exceptions;
using Take.Api.TestApi.Controllers;
using Take.Api.TestApi.Models.UI;
using Take.Api.TestApi.Tests.Setup.TestData;

using Xunit;

namespace Take.Api.TestApi.Tests.Setup.Controller
{
    public class LoginControllerTestes
    {
        private readonly ApiSettings _apiSettings;

        [Theory]
        [MemberData(nameof(LoginTestData.GetValidUserCredentials), MemberType = typeof(LoginTestData))]

        public void CheckUserTestValid(string username, string password)
        {
            var service = new LoginController(_apiSettings);

            Action result = () => service.AuthenticateAsync(username, password);

            result.Should().NotThrow<BaseException>();
            result.Should().BeOfType<Action>();

        }

        [Theory]
        [MemberData(nameof(LoginTestData.GetInvalidUserCredentials), MemberType = typeof(LoginTestData))]
        public void CheckUserTestInvalid(string username, string password)
        {
            var service = new LoginController(_apiSettings);

            Action result = () => service.AuthenticateAsync(username, password);

            result.Should().NotThrow<BaseException>();
            result.Should().BeOfType<Action>();

        }
    }
}
