using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UseCases.UpdateCompany
{
    public class UpdateCompanyRequest : IRequest<UpdateCompanyResponse>
    {
        public Company Company { get; set; }
    }

    public class UpdateCompanyResponse
    {
        public bool Sucess { get; set; }
    }

    public class UpdateCompanyRequestHandler : IRequestHandler<UpdateCompanyRequest, UpdateCompanyResponse>
    {
        private readonly ICompanyService companyService;

        public UpdateCompanyRequestHandler(ICompanyService companyService)
        {
            this.companyService = companyService;
        }

        public async Task<UpdateCompanyResponse> Handle(UpdateCompanyRequest request, CancellationToken cancellationToken)
        {
            var company = request.Company;

            if (company is null || company.Id is null || company.Id == 0)
                throw new ArgumentException("Company is not in the corret format to be updated");

            company.ValidateMandatoryData();
            company.ValidatePatternData();

            var currentCompany = await companyService.GetById(Convert.ToInt32(company.Id));

            await companyService.Update(company);

            return new UpdateCompanyResponse
            {
                Sucess = true
            };
        }
    }
}
