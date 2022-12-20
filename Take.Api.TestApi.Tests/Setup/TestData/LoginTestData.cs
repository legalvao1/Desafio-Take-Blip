using System;
using System.Collections.Generic;
using System.Text;

namespace Take.Api.TestApi.Tests.Setup.TestData
{
    public class LoginTestData
    {

        public static IEnumerable<object[]> GetValidUserCredentials()
        {
            yield return new object[]
            {
                "Mavis", "batatinha123"
            };
        }

        public static IEnumerable<object[]> GetInvalidUserCredentials()
        {
            yield return new object[]
            {
                "Mavis", "batatinha1"
            };
        }
    }
}
