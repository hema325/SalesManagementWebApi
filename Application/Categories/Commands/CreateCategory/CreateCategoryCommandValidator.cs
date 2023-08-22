using Microsoft.EntityFrameworkCore;

namespace Application.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandValidator: AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator(IApplicationDbContext context)
        {
            RuleFor(c => c.Name)
               .NotEmpty().WithMessage("{PropertyName} is required")
               .MaximumLength(256).WithMessage("{PropertyName} length must be between {MinLength} and {MaxLength}")
               .MustAsync(async (n, ct) => !await context.Categories.AnyAsync(c => c.Name == n)).WithMessage("{PropertyName} is existed");

            RuleFor(c => c.ParentId)
                .MustAsync(async (i, ct) => await context.Categories.AnyAsync(c => c.Id == i && c.ParentId == null)).WithMessage("{PropertyName} can't be a sub category")
                .When(c => c.ParentId != 0 && c.ParentId != null);
        }
    }
}
