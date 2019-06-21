using NUnit.Framework;
using OpenBank.Application;
using Microsoft.EntityFrameworkCore;

namespace Tests
{
    public class UserTests
    {
        [Test]
        public void GivenValidDto_ThenCreatesUser()
        {
            (User user, Error error) result = User.CreateUser(new CreateUserDto
            {
                Bank = Banks.BizFiBank.ToString(),
                AccountNumber = "11112222"
            });

            Assert.Null(result.error);
            Assert.NotNull(result.user);
        }

        [TestCase("01112222")]
        [TestCase("1111222")]
        [TestCase("111122222")]
        public void GivenInvalidAccountNumber_ThenReturnsError(string accountNumber)
        {
            (User user, Error error) result = User.CreateUser(new CreateUserDto
            {
                Bank = Banks.BizFiBank.ToString(),
                AccountNumber = accountNumber
            });

            Assert.NotNull(result.error);
            Assert.Null(result.user);
        }
    }
}