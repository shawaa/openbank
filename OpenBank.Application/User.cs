using System;
using System.ComponentModel.DataAnnotations;

namespace OpenBank.Application
{
    public class User
    {
        public User(string accountNumber, string name)
        {
            AccountNumber = accountNumber;
            Name = name;
        }

        [Key]
        public Guid? Id { get; private set; }
        public string AccountNumber { get; private set; }
        public string Name { get; private set; }

        public static (User user, Error error) CreateUser(CreateUserDto dto)
        {
            if (dto.AccountNumber.Length != 8)
            {
                return (null, new Error("account number must be 8 digits long"));
            }

            if (dto.AccountNumber.ToCharArray()[0] == '0')
            {
                return (null, new Error("account number must not start with 0"));
            }

            return (new User(dto.AccountNumber, dto.Name), null);
        }
    }
}
