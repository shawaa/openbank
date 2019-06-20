using System;

namespace OpenBank.Application
{
    public class UserDto
    {
        public Guid Id { get; set; }

        public string Bank { get; set; }

        public string AccountNumber { get; set; }
    }
}
