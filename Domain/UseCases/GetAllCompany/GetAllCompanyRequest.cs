using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UseCases.GetAllCompany
{
    public class GetAllCompanyRequest : IRequest<GetAllCompanyResponse>
    {

    }

    public class GetAllCompanyResponse
    {
        public IEnumerable<Company> Companies { get; set; }
    }

    public class GetAllCompanyRequestHandler : IRequestHandler<GetAllCompanyRequest, GetAllCompanyResponse>
    {
        private readonly ICompanyService companyService;

        public GetAllCompanyRequestHandler(ICompanyService companyService)
        {
            this.companyService = companyService;
        }
        public async Task<GetAllCompanyResponse> Handle(GetAllCompanyRequest request, CancellationToken cancellationToken)
        {
            var companies = await companyService.GetAll();
            var sortedCompanies = companies.OrderBy(x => x.Status).ThenBy(x => x.CreateDate);

            return new GetAllCompanyResponse { Companies = sortedCompanies };
        }
    }
}
