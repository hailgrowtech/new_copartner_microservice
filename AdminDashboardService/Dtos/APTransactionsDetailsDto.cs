namespace AdminDashboardService.Dtos
{
    public class APTransactionsDetailsDto
    {
        public DateTime Date { get; set; }
        public string APName { get; set; }
        public long Mobile { get; set; }
        public long Amount { get; set; }
        public bool Request { get; set; }
    }
}
