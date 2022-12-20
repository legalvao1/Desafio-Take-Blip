using System;
using System.Collections.Generic;
using System.Text;

using FluentAssertions;

using Moq;

using Take.Api.BancoPanCartoes.Models.Exceptions;
using Take.Api.TestApi.Controllers;
using Take.Api.TestApi.Facades.Interfaces;
using Take.Api.TestApi.Models;
using Take.Api.TestApi.Tests.Setup.TestData;

using Xunit;

namespace Take.Api.TestApi.Tests.Setup.Controller
{
    public class GithubRepositoriesControllerTests
    {
        private readonly Mock<IGithubRepositoriesFacade> _githubRepositoriesFacade;
        private readonly Mock<IOrganizationRepositoryLanguagesFacade> _organizationRepositoryLanguagesFacade;

        public GithubRepositoriesControllerTests
()
        {
            _githubRepositoriesFacade = new Mock<IGithubRepositoriesFacade>();
            _organizationRepositoryLanguagesFacade = new Mock<IOrganizationRepositoryLanguagesFacade>();
        }

        [Theory]
        [MemberData(nameof(OrganizationRepositoriesTestData.GetValidOrganizationRepositories), MemberType = typeof(OrganizationRepositoriesTestData))]
        public void CheckIfOrganizationRepositoriesIsValid(string language, 
                                               SortDirectionType sortDirection,
                                               SortParameterType sortParameter, 
                                               int quantity, 
                                               string organization)
        {
            var service = ServiceInit();

            Action result = () => service.GetOrganizationRepositoriesAsync(language, sortDirection, sortParameter, quantity, organization);

            result.Should().NotThrow<BaseException>();
            result.Should().BeOfType<Action>();
        }

        [Theory]
        [MemberData(nameof(OrganizationRepositoriesTestData.GetInvalidOrganizationRepositories), MemberType = typeof(OrganizationRepositoriesTestData))]
        public void CheckIfOrganizationIsInvalid(string language,
                                                 SortDirectionType sortDirection,
                                                 SortParameterType sortParameter,
                                                 int quantity,
                                                 string organization)
        {
            var service = ServiceInit();

            Assert.ThrowsAsync<BaseException>(async () => await service.GetOrganizationRepositoriesAsync(language, sortDirection, sortParameter, quantity, organization));

        }

        [Theory]
        [MemberData(nameof(OrganizationRepositoriesTestData.GetInvalidRepositoriesRange), MemberType = typeof(OrganizationRepositoriesTestData))]
        public void CheckIfRepositoriesRangeInvalid(string language,
                                                 SortDirectionType sortDirection,
                                                 SortParameterType sortParameter,
                                                 int quantity,
                                                 string organization)
        {
            var service = ServiceInit();

            //Action result = () => service.GetOrganizationRepositoriesAsync(language, sortDirection, sortParameter, quantity, organization);

            Assert.ThrowsAsync<BaseException>(async () => await service.GetOrganizationRepositoriesAsync(language, sortDirection, sortParameter, quantity, organization));
            //result.Should().Throw<BaseException>();
            //result.Should().BeOfType<Action>();
        }

        public GithubRepositoriesController ServiceInit()
        {
            return new GithubRepositoriesController(_githubRepositoriesFacade.Object,
                                                    _organizationRepositoryLanguagesFacade.Object);
        }
    }
}
