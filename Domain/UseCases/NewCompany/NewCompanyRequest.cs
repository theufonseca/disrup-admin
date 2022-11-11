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

namespace Domain.UseCases.NewCompany
{
    public class NewCompanyRequest : IRequest<NewCompanyResponse>
    {
        public Company Company { get; set; }
    }

    public class NewCompanyResponse
    {
        public int CompanyId { get; set; }
    }

    public class NewCompanyRequestHandler : IRequestHandler<NewCompanyRequest, NewCompanyResponse>
    {
        private readonly ICompanyService companyService;
        private readonly ICompanyNotificationService notificationService;

        public NewCompanyRequestHandler(ICompanyService companyService,
            ICompanyNotificationService notificationService)
        {
            this.companyService = companyService;
            this.notificationService = notificationService;
        }

        public async Task<NewCompanyResponse> Handle(NewCompanyRequest request, CancellationToken cancellationToken)
        {
            var company = request.Company;
            company.ValidateMandatoryData();
            company.ValidatePatternData();
            company.SetCreateDate();

            if (await companyService.Exists(company.Document))
                throw new CompanyAlreadyExists(company.Document);

            company.SetStatus(CompanyStatus.PENDING_APPROVAL);
            
            var companyId = await companyService.Insert(company);

            await notificationService.NotifyCompanyChanges(CompanyNotificationStatus.New, companyId);

            return new NewCompanyResponse
            {
                CompanyId = companyId
            };
        }
    }
}
