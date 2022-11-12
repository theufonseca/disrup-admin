using Domain.Enums;
using Domain.UseCases.GetAllCompany;
using Domain.UseCases.GetCompanyById;
using Domain.UseCases.GetCompanyByStatus;
using Domain.UseCases.NewCompany;
using Domain.UseCases.UpdateCompany;
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
            var result = await mediator.Send(request);

            return Ok(result);
        }

        [HttpGet("all")]
        public async Task<IActionResult> Get()
        {
            var request = new GetAllCompanyRequest();
            var result = await mediator.Send(request);
            return Ok(result);
        }

        [HttpGet("status/{status}")]
        public async Task<IActionResult> Get(CompanyStatus status)
        {
            var request = new GetCompanyByStatusRequest { Status = status };
            var response = await mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var request = new GetCompanyByIdRequest { CompanyId = id };
            var response = await mediator.Send(request);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateCompanyRequest request)
        {
            var response = await mediator.Send(request);
            return Ok(response);
        }

    }
}
