using Microsoft.EntityFrameworkCore;

namespace Application.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandValidator: AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryCommandValidator(IApplicationDbContext context)
        {
            RuleFor(c => c.Id)
               .NotEmpty().WithMessage("{PropertyName} is required");

            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MaximumLength(256).WithMessage("{PropertyName} length must be between {MinLength} and {MaxLength}")
                .MustAsync(async (c, n, ct) => !await context.Categories.AnyAsync(catgy => catgy.Name == n && catgy.Id != c.Id)).WithMessage("{PropertyName} is existed");

            RuleFor(c => c.ParentId)
                .MustAsync(async (i, ct) => await context.Categories.AnyAsync(c => c.Id == i && c.ParentId == null)).WithMessage("{PropertyName} can't be a sub category");
        }
    }
}
