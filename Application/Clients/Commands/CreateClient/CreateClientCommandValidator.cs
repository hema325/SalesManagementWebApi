using Microsoft.EntityFrameworkCore;

namespace Application.Clients.Commands.CreateClient
{
    public class CreateClientCommandValidator: AbstractValidator<CreateClientCommand>
    {
        public CreateClientCommandValidator(IApplicationDbContext context)
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MaximumLength(256).WithMessage("{PropertyName} length must be between {MinLength} and {MaxLength}")
                .MustAsync(async (n, ct) => !await context.Clients.AnyAsync(c => c.Name == n)).WithMessage("{PropertyName} is existed");

            RuleFor(c => c.DateOfBirth)
                .NotEmpty().WithMessage("{PropertyName} is required");

            RuleFor(c => c.Gender)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .IsEnumName(typeof(Gender),false).WithMessage("{PropertyName} is not valid");

            RuleFor(c => c.PhoneNumbers)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .ForEach(builder =>
                {
                    builder.NotEmpty().WithMessage("{PropertyName} is required");
                    builder.MaximumLength(24).WithMessage("{PropertyName} length must be less than {MaxLength}");
                    builder.Must(ph => ph.All(c => char.IsDigit(c))).WithMessage("{PropertyName} must contains numbers only");
                });

            RuleFor(c => c.Address)
                .NotEmpty().WithMessage("{PropertyName} is required");

            RuleFor(c => c.Images)
               .ForEach(builder =>
               {
                   builder.IsImage().WithMessage("{PropertyName} is not a valid image");
               });
        }
    }
}
