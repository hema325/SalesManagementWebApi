namespace Application.Categories.Common
{
    public class CategoryDTO
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string? Notes { get; init; }
        public DateTime CreatedOn { get; init; }
        public CategoryDTO Parent { get; init; }
    }
}
