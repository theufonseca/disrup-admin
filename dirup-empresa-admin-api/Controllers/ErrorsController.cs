using Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace dirup_empresa_admin_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorsController : ControllerBase
    {
        [HttpGet("/error")]
        public IActionResult Error()
        {
            ExceptionBase exceptionBase = new();
            var exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

            if (exception is ExceptionBase)
            {
                exceptionBase = (ExceptionBase)exception;
                return Problem(title: exceptionBase.Title, detail: exceptionBase.Detail, statusCode: (int)HttpStatusCode.BadRequest);
            }

            return Problem(title: "Unexpected Error", detail: exception.Message, statusCode: (int)HttpStatusCode.InternalServerError);
        }
    }
}
