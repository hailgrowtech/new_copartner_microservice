namespace ExpertsService.Dtos
{
    public class RADetailsReadDto
    {
        public Guid? Id { get; set; }
        public DateTime JoinDate { get; set; }
        public string Name { get; set; }
        public string SEBINo { get; set; }
        public int? FixCommission { get; set; }
        public long RAEarning { get; set; }
        public bool isActive { get; set; }

    }
}
