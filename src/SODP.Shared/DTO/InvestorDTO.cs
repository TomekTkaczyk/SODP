namespace SODP.Shared.DTO
{
    public record InvestorDTO // : BaseDTO
    {
		public int Id { get; set; }
		public string Name { get; set; } 
        public bool ActiveStatus { get; set; }
	}
}
