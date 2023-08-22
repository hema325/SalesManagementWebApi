using Microsoft.EntityFrameworkCore;

namespace Application.Clients.Commands.UpdateClient
{
    public class UpdateClientCommandValidator: AbstractValidator<UpdateClientCommand>
    {
        public UpdateClientCommandValidator(IApplicationDbContext context)
        {
            RuleFor(c => c.Id)
                .NotEmpty().WithMessage("{PropertyName} is required");

            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MaximumLength(256).WithMessage("{PropertyName} length must be between {MinLength} and {MaxLength}")
                .MustAsync(async (c, n, ct) => !await context.Clients.AnyAsync(cl => cl.Name == n && cl.Id != c.Id)).WithMessage("{PropertyName} is existed");

            RuleFor(c => c.DateOfBirth)
                .NotEmpty().WithMessage("{PropertyName} is required");

            RuleFor(c => c.Gender)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .IsInEnum().WithMessage("{PropertyName} is not valid");

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
        }
    }
}
