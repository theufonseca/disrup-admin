using Domain.Enums;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UseCases.DeleteCompany
{
    public class DeleteCompanyRequest : IRequest<DeleteCompanyResponse>
    {
        public int CompanyId { get; set; }
    }

    public class DeleteCompanyResponse
    {
        public bool Sucess { get; set; }
    }

    public class DeleteCompanyRequestHandler : IRequestHandler<DeleteCompanyRequest, DeleteCompanyResponse>
    {
        private readonly ICompanyService companyService;
        private readonly IPhotoStorageService photoStorageService;
        private readonly ICompanyPhotoService companyPhotoService;
        private readonly ICompanyNotificationService companyNotificationService;

        public DeleteCompanyRequestHandler(ICompanyService companyService, IPhotoStorageService photoStorageService, 
            ICompanyPhotoService companyPhotoService, ICompanyNotificationService companyNotificationService)
        {
            this.companyService = companyService;
            this.photoStorageService = photoStorageService;
            this.companyPhotoService = companyPhotoService;
            this.companyNotificationService = companyNotificationService;
        }
        public async Task<DeleteCompanyResponse> Handle(DeleteCompanyRequest request, CancellationToken cancellationToken)
        {
            if (request.CompanyId == 0)
                throw new ArgumentException("Company id needs to be filled");

            var company = await companyService.GetById(request.CompanyId);

            if (company == null)
                throw new ArgumentException("company not found");

            List<Task> photosDeletes = new();
            if (company.Photos != null)
                foreach (var item in company.Photos)
                {
                    photosDeletes.Add(companyPhotoService.DeletePhoto(item.Id));
                    photosDeletes.Add(photoStorageService.Delete(item.Name));
                }

            await Task.WhenAll(photosDeletes);
            await companyService.Delete(request.CompanyId);
            await companyNotificationService.NotifyCompanyChanges(CompanyNotificationStatus.Deleted, request.CompanyId);

            return new DeleteCompanyResponse { Sucess = true };
        }
    }
}
