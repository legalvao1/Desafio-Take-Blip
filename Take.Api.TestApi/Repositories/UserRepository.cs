using System.Collections.Generic;
using System.Linq;

using Take.Api.TestApi.Models;

namespace Take.Api.TestApi.Services.Repository
{
    /// <summary>
    /// Simulates the Database to get a registered user
    /// </summary>
    public static class UserRepository
    {
        /// <summary>
        /// Gets the user if its exists in the current context
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static User Get(string username, string password)
        {
            var users = new List<User>();
            users.Add(new User { Id = 1, UserName = "Leticia", Password = "batatinha123", Role = "employee" });
            users.Add(new User { Id = 2, UserName = "Mavis", Password = "batatinha123", Role = "employee" });

            return users.FirstOrDefault(x => x.UserName.Equals(username) && x.Password.Equals(password));
        }

    }
}
