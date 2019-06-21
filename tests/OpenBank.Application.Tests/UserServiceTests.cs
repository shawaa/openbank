using System.Threading.Tasks;
using NUnit.Framework;
using OpenBank.Application;
using Microsoft.EntityFrameworkCore;

namespace Tests
{
    public class UserServiceTests
    {
        [Test]
        public async Task GivenUserExists_ThenReturnsError()
        {
            var options = new DbContextOptionsBuilder<UserContext>()
                .UseInMemoryDatabase(databaseName: "test")
                .Options;

            using(var context = new UserContext(options))
            {
                string accountNumber = "39009209";

                context.Users.Add(User.CreateUser(new CreateUserDto
                {
                    Bank = Banks.BizFiBank.ToString(),
                    AccountNumber = accountNumber
                }).user);

                context.SaveChanges();

                UserService userService = new UserService(context);

                var result = await userService.AddUser(new CreateUserDto
                {
                    Bank = Banks.BizFiBank.ToString(),
                    AccountNumber = accountNumber
                });

                Assert.NotNull(result.error);
            }
        }
    }
}