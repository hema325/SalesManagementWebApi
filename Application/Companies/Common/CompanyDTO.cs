namespace Application.Companies.Common
{
    public class CompanyDTO
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string? Notes { get; init; }
        public DateTime CreatedOn { get; init; }
    }
}
