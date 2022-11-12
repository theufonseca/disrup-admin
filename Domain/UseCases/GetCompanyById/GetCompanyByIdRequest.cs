using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UseCases.GetCompanyById
{
    public class GetCompanyByIdRequest : IRequest<GetCompanyByIdResponse>
    {
        public int CompanyId { get; set; }
    }

    public class GetCompanyByIdResponse
    {
        public Company Company { get; set; }
    }

    public class GetCompanyByIdRequestHandler : IRequestHandler<GetCompanyByIdRequest, GetCompanyByIdResponse>
    {
        private readonly ICompanyService companyService;

        public GetCompanyByIdRequestHandler(ICompanyService companyService)
        {
            this.companyService = companyService;
        }
        public async Task<GetCompanyByIdResponse> Handle(GetCompanyByIdRequest request, CancellationToken cancellationToken)
        {
            if (request.CompanyId == 0)
                throw new ArgumentException("Company id needs to be filled");

            var company = await companyService.GetById(request.CompanyId);

            return new GetCompanyByIdResponse
            {
                Company = company
            };
        }
    }
}
