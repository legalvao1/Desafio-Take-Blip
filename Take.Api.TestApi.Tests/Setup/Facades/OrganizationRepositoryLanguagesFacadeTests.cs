using System;
using System.Threading.Tasks;

using AutoFixture;

using FluentAssertions;

using Moq;

using Serilog;

using Take.Api.BancoPanCartoes.Models.Exceptions;
using Take.Api.TestApi.Facades;
using Take.Api.TestApi.Services;

using Xunit;

namespace Take.Api.TestApi.Tests.Setup.Facades
{
    public class OrganizationRepositoryLanguagesFacadeTests
    {
        private readonly Fixture _fixture;
        private readonly Mock<ILogger> _logger;
        private readonly Mock<IRestEaseService> _restease;
        private const string VALID_ORGANIZATION = "takenet";
        private const string INVALID_ORGANIZATION = "take";

        public OrganizationRepositoryLanguagesFacadeTests()
        {
            _fixture = new Fixture();
            _logger = new Mock<ILogger>();
            _restease = new Mock<IRestEaseService>();
        }

        [Fact]
        public void GetOrganizationLanguagesAsync_NotThrowingException()
        {
            var service = ServiceInit();

            _restease.Setup(mock => mock.GetOrganizationRepositoriesAsync(VALID_ORGANIZATION))
                .Returns(_fixture.Create<Task<string>>());

            Func<Task> result = async () => await service.GetOrganizationLanguagesAsync(VALID_ORGANIZATION);

            result.Should().NotBeNull();
            result.Should().NotThrowAsync();
        }

        [Fact]
        public void GetOrganizationLanguagesAsync_ThrowingException()
        {
            var service = ServiceInit();

            _restease.Setup(mock => mock.GetOrganizationRepositoriesAsync(INVALID_ORGANIZATION))
                .Returns(_fixture.Create<Task<string>>());

            Func<Task> result = async () => await service.GetOrganizationLanguagesAsync(INVALID_ORGANIZATION);

            result.Should().ThrowAsync<BaseException>();
        }

        private OrganizationRepositoryLanguagesFacade ServiceInit()
        {
            return new OrganizationRepositoryLanguagesFacade(_restease.Object, _logger.Object);
        }

    }
}
