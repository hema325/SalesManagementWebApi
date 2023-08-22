namespace Application.Categories.Commands.UpdateCategory
{
    public record UpdateCategoryCommand(int Id, string Name, string? Notes, int? ParentId): IRequest;
}
