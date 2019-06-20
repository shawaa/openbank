namespace OpenBank.Application
{
    public class UserDetailsDto
    {
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public string SortCode { get; set; }
        public decimal Balance { get; set; }
        public decimal AvailableBalance { get; set; }
        public decimal? Overdraft { get; set; }
    }
}
