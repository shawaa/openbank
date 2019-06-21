using System;
using System.ComponentModel.DataAnnotations;

namespace OpenBank.Application
{
    public class User
    {
        public User(string accountNumber, Banks bank)
        {
            AccountNumber = accountNumber;
            Bank = bank;
        }

        [Key]
        public Guid? Id { get; private set; }
        public string AccountNumber { get; private set; }
        public Banks Bank { get; private set; }

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

            if (!int.TryParse(dto.AccountNumber, out int _))
            {
                return (null, new Error("account number must be digits only"));
            }

            Banks bank;

            if (!Enum.TryParse(dto.Bank, out bank))
            {
                return (null, new Error($"Bank {dto.Bank} is not supported"));
            }

            return (new User(dto.AccountNumber, bank), null);
        }
    }
}
