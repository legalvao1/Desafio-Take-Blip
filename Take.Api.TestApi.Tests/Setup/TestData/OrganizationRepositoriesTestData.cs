using System.Collections.Generic;

using Take.Api.TestApi.Models;

namespace Take.Api.TestApi.Tests.Setup.TestData
{
    public class OrganizationRepositoriesTestData
    {
        public static IEnumerable<object[]> GetValidOrganizationRepositories()
        {
            yield return new object[]
            {
                "C#", SortDirectionType.ASC, SortParameterType.Language, 3, "takenet"
            };
        }

        public static IEnumerable<object[]> GetInvalidOrganizationRepositories()
        {
            yield return new object[]
            {
                "C#", SortDirectionType.ASC, SortParameterType.Language, 3, "take"
            };
        }

        public static IEnumerable<object[]> GetInvalidRepositoriesRange()
        {
            yield return new object[]
            {
                "C#", SortDirectionType.ASC, SortParameterType.Language, -3, "takenet"
            };
        }
    }
}
