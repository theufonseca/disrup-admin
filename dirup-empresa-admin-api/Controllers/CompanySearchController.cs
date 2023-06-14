using Domain.UseCases.CompanyFilter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace dirup_empresa_admin_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanySearchController : ControllerBase
    {
        private readonly IMediator mediator;

        public CompanySearchController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post(CompanyFilterRequest request)
        {
            var result = await mediator.Send(request);
            return Ok(result);
        }
    }
}
