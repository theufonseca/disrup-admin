using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UseCases.CompanyFilter
{
    public class CompanyFilterRequest : IRequest<CompanyFilterResponse>
    {
        public Filters Filters { get; set; }
    }

    public class CompanyFilterResponse
    {
        public IEnumerable<Company> Companies { get; set; }
    }

    public class CompanyFilterRequestHandler : IRequestHandler<CompanyFilterRequest, CompanyFilterResponse>
    {
        private readonly ICompanySearchService companySearchService;

        public CompanyFilterRequestHandler(ICompanySearchService companySearchService)
        {
            this.companySearchService = companySearchService;
        }

        public async Task<CompanyFilterResponse> Handle(CompanyFilterRequest request, CancellationToken cancellationToken)
        {
            var companies = await companySearchService.Filter(request.Filters);

            return new CompanyFilterResponse
            {
                Companies = companies,
            };
        }
    }
}
