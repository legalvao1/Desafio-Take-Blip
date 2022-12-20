using System;
using System.Collections.Generic;
using System.Text;

using Moq;
using Xunit;

using Take.Api.TestApi.Facades.Interfaces;
using Take.Api.TestApi.Controllers;
using FluentAssertions;
using Take.Api.TestApi.Tests.Setup.TestData;
using Take.Api.BancoPanCartoes.Models.Exceptions;

namespace Take.Api.TestApi.Tests.Setup.Controller
{
    public class OrganizationRepositoryLanguagesControllerTests
    {
        private readonly Mock<IOrganizationRepositoryLanguagesFacade> _organizationRepositoryLanguagesFacade;

        public OrganizationRepositoryLanguagesControllerTests()
        {
            _organizationRepositoryLanguagesFacade = new Mock<IOrganizationRepositoryLanguagesFacade>();
        }

        [Theory]
        [MemberData(nameof(OrganizationTesteData.GetValidOrganization), MemberType = typeof(OrganizationTesteData))]
        public void CheckIfOrganizationIsValid(string organization)
        {
            var service = ServiceInit();

            Action result = () => service.GetOrganizationLanguagesAsync(organization);

            result.Should().NotThrow<BaseException>();
            result.Should().BeOfType<Action>();
        }

        [Theory]
        [MemberData(nameof(OrganizationTesteData.GetValidOrganization), MemberType = typeof(OrganizationTesteData))]
        public void CheckIfOrganizationIsInvalid(string organization)
        {
            var service = ServiceInit();

            Action result = () => service.GetOrganizationLanguagesAsync(organization);

            result.Should().NotThrow<BaseException>();
            result.Should().BeOfType<Action>();
        }

        public OrganizationRepositoryLanguagesController ServiceInit()
        {
            return new OrganizationRepositoryLanguagesController(_organizationRepositoryLanguagesFacade.Object);
        }
    }
}
