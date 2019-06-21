using System.Collections.Generic;

namespace OpenBank.Application
{
    public class Error
    {
        public Error(params string[] messages)
        {
            Messages = messages;
        }

        public IEnumerable<string> Messages { get; }
    }
}
