using System;
using System.Collections.Generic;
using System.Text;

namespace Take.Api.TestApi.Tests.Setup.TestData
{
    public class OrganizationTesteData
    {
        public static IEnumerable<object[]> GetValidOrganization()
        {
            yield return new object[]
            {
                "takenet"
            };
        }

        public static IEnumerable<object[]> GetInvalidOrganization()
        {
            yield return new object[]
            {
                "Take"
            };
        }
    }
}
