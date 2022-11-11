using Domain.UseCases.NewCompany;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace dirup_empresa_admin_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly IMediator mediator;

        public CompanyController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post(NewCompanyRequest request)
        {
            try
            {
                var result = await mediator.Send(request);

                return Ok(result);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
