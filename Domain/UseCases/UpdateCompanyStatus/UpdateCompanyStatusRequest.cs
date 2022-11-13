using Domain.Enums;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UseCases.UpdateCompanyStatus
{
    public class UpdateCompanyStatusRequest : IRequest<UpdateCompanyStatusResponse>
    {
        public int CompanyId { get; set; }
        public CompanyStatus NewStatus { get; set; }
    }

    public class UpdateCompanyStatusResponse
    {
        public bool Sucess { get; set; }
    }

    public class UpdateCompanyStatusRequestHandler : IRequestHandler<UpdateCompanyStatusRequest, UpdateCompanyStatusResponse>
    {
        private readonly ICompanyService companyService;
        private readonly ICompanyNotificationService companyNotificationService;

        public UpdateCompanyStatusRequestHandler(ICompanyService companyService, ICompanyNotificationService companyNotificationService)
        {
            this.companyService = companyService;
            this.companyNotificationService = companyNotificationService;
        }
        public async Task<UpdateCompanyStatusResponse> Handle(UpdateCompanyStatusRequest request, CancellationToken cancellationToken)
        {
            if (request.CompanyId == 0)
                throw new ArgumentException("Company id needs to be greater than zero(0)");

            var company = await companyService.GetById(request.CompanyId);

            if (company == null)
                throw new ArgumentException("Company not found");

            company.SetStatus(request.NewStatus);

            await companyService.Update(company);
            await companyNotificationService.NotifyCompanyChanges(CompanyNotificationStatus.Updated, request.CompanyId);

            return new UpdateCompanyStatusResponse { Sucess = true };
        }
    }
}
