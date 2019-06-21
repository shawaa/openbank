using System;

namespace OpenBank.Application
{
    public class TransactionDto
    {
        public decimal Amount { get; set; }

        public string Merchant { get; set; }

        public DateTime ClearedDate { get; set; }
    }
}
