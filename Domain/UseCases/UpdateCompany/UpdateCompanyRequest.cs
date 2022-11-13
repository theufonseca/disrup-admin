using Domain.Entities;
using Domain.Enums;
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
        private readonly ICompanyNotificationService companyNotificationService;

        public UpdateCompanyRequestHandler(ICompanyService companyService, ICompanyNotificationService companyNotificationService)
        {
            this.companyService = companyService;
            this.companyNotificationService = companyNotificationService;
        }

        public async Task<UpdateCompanyResponse> Handle(UpdateCompanyRequest request, CancellationToken cancellationToken)
        {
            var company = request.Company;

            if (company is null || company.Id is null || company.Id == 0)
                throw new ArgumentException("Company is not in the corret format to be updated");

            company.ValidateMandatoryData();
            company.ValidatePatternData();
            
            var currentCompany = await companyService.GetById((int)company.Id);
            var existsDocument = await companyService.Exists(company.Document);

            if (currentCompany is null)
                throw new ArgumentException("Company not found");

            currentCompany.CopyToUpdate(company);

            if (existsDocument && currentCompany.Document != company.Document)
                throw new ArgumentException("This document already exists");

            await companyService.Update(company);

            await companyNotificationService.NotifyCompanyChanges(CompanyNotificationStatus.Updated, (int)company.Id);

            return new UpdateCompanyResponse
            {
                Sucess = true
            };
        }
    }
}
