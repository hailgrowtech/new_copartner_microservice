namespace AdminDashboardService.Dtos
{
    public class RATransactionsDetailsCreateDto
    {
        public DateTime Date {  get; set; }
        public string RAName  {  get; set; }
        public string SEBINo {  get; set; }
        public long Amount {  get; set; }
        public bool Request {  get; set; }
    }
}
