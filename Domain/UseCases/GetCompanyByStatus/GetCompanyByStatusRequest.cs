using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UseCases.GetCompanyByStatus
{
    public class GetCompanyByStatusRequest : IRequest<GetCompanyByStatusResponse>
    {
        public CompanyStatus Status { get; set; }
    }

    public class GetCompanyByStatusResponse
    {
        public IEnumerable<Company> Companies { get; set; }
    }

    public class GetCompanyByStatusRequestHandler : IRequestHandler<GetCompanyByStatusRequest, GetCompanyByStatusResponse>
    {
        private readonly ICompanyService companyService;

        public GetCompanyByStatusRequestHandler(ICompanyService companyService)
        {
            this.companyService = companyService;
        }

        public async Task<GetCompanyByStatusResponse> Handle(GetCompanyByStatusRequest request, CancellationToken cancellationToken)
        {
            var companies = await companyService.GetByStatus(request.Status);
            var sortedCompanies = companies.OrderBy(x => x.CreateDate);

            return new GetCompanyByStatusResponse
            {
                Companies = sortedCompanies
            };
        }
    }
}
