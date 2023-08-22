using Microsoft.AspNetCore.Http;

namespace Application.Common.Extensions
{
    internal static class RuleBuilderExtensions
    {
        public static IRuleBuilderOptions<TObject,IFormFile> IsImage<TObject>(this IRuleBuilder<TObject,IFormFile> builder)
        {
            return builder.Must(f => f.ContentType.Contains("image")).WithMessage("{PropertyName} is not a valid image");
        }
    }
}
