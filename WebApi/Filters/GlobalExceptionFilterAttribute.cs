using Application.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApi.Filters
{
    public class GlobalExceptionFilterAttribute: ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var exType = context.Exception.GetType();

            var status = Convert.ToInt32(exType.GetProperty(nameof(CustomExceptionBase.Status))?.GetValue(context.Exception));
            var errors = exType.GetProperty(nameof(CustomExceptionBase.Errors))?.GetValue(context.Exception);
            var message = context.Exception?.InnerException?.Message ?? context.Exception.Message;

            status = status < 100 || status > 599 ? StatusCodes.Status500InternalServerError : status;

            if(errors == null)
            {
                context.Result = new BadRequestObjectResult(new { Status = status, Message = message }) { StatusCode = status };
                return;
            }

            context.Result = new BadRequestObjectResult(new { Status = status, Message = message, Errors = errors }) { StatusCode = status };
        }

    }
}
