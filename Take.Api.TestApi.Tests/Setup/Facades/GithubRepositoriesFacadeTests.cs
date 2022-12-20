using AutoFixture;

using Serilog;

using Moq;

using Take.Api.TestApi.Facades;
using Take.Api.TestApi.Facades.Interfaces;
using Take.Api.TestApi.Services;

using Xunit;
using System.Threading.Tasks;
using System;
using Take.Api.TestApi.Models;
using FluentAssertions;

namespace Take.Api.TestApi.Tests.Setup.Facades
{
    public class GithubRepositoriesFacadeTests
    {
        private readonly Fixture _fixture;
        private readonly Mock<ILogger> _logger;
        private readonly Mock<IRestEaseService> _restease;
        private readonly Mock<IOrganizationRepositoryLanguagesFacade> _organizationRepositoryLanguagesFacade;
        private const string VALID_ORGANIZATION = "takenet";

        public GithubRepositoriesFacadeTests()
        {
            _fixture = new Fixture();
            _logger = new Mock<ILogger>();
            _restease = new Mock<IRestEaseService>();
            _organizationRepositoryLanguagesFacade = new Mock<IOrganizationRepositoryLanguagesFacade>();
        }

        [Fact]
        public void GetOrganizationRepositoriesAsyncTest_NotThrowingException()
        {
            var service = ServiceInit();

            _restease.Setup(mock => mock.GetOrganizationRepositoriesAsync(VALID_ORGANIZATION))
                .Returns(_fixture.Create<Task<string>>());

            Func<Task> result = async () => await service.GetOrganizationRepositoriesAsync(It.IsAny<string>(),
                                                                                           It.IsAny<SortDirectionType>(),
                                                                                           It.IsAny<SortParameterType>(),
                                                                                           It.IsAny<int>(),
                                                                                           VALID_ORGANIZATION);

            result.Should().NotBeNull();
            result.Should().NotThrowAsync();
        }

        private GithubRepositoriesFacade ServiceInit()
        {
            return new GithubRepositoriesFacade(_logger.Object,
                                                _restease.Object,
                                                _organizationRepositoryLanguagesFacade.Object);
        }
    }
}
