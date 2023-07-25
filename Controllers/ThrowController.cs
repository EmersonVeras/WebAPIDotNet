using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Diagnostics;
namespace WebApi.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ThrowController : Controller
    {
        [Route("/error")]
        public IActionResult HandleError() =>
        Problem();


        [Route("/error-development")]
        public IActionResult HandleErrorDevelopment(
        [FromServices] IHostEnvironment hostEnvironment)
        {
            if (!hostEnvironment.IsDevelopment())
            {
                return NotFound();
            }

            var exceptionHandlerFeature = 
                HttpContext.Features.Get<IExceptionHandlerFeature>()!;

            return Problem(
                title: exceptionHandlerFeature.Error.Message,
                detail: exceptionHandlerFeature.Error.StackTrace
            );
        }
    }
}