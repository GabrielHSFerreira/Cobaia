using Cobaia.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace Cobaia.WebApi.Filters
{
    internal class ExceptionActionFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var result = await next();

            if (result.Exception is EntityNotFoundException exception)
            {
                result.Result = new NotFoundObjectResult(exception.Message);
                result.ExceptionHandled = true;
            }
        }
    }
}