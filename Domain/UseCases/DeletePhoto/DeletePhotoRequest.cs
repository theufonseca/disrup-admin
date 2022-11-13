using Domain.Enums;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UseCases.DeletePhoto
{
    public class DeletePhotoRequest : IRequest<DeletePhotoResponse>
    {
        public int PhotoId { get; set; }
    }

    public class DeletePhotoResponse
    {
        public bool Sucess { get; set; }
    }

    public class DeletePhotoRequestHandler : IRequestHandler<DeletePhotoRequest, DeletePhotoResponse>
    {
        private readonly ICompanyPhotoService companyPhotoService;
        private readonly ICompanyNotificationService companyNotificationService;
        private readonly IPhotoStorageService photoStorageService;

        public DeletePhotoRequestHandler(ICompanyPhotoService companyPhotoService, ICompanyNotificationService companyNotificationService,
            IPhotoStorageService photoStorageService)
        {
            this.companyPhotoService = companyPhotoService;
            this.companyNotificationService = companyNotificationService;
            this.photoStorageService = photoStorageService;
        }

        public async Task<DeletePhotoResponse> Handle(DeletePhotoRequest request, CancellationToken cancellationToken)
        {
            if (request.PhotoId == 0)
                throw new ArgumentException("Photo needs to be greater than zero (0)");

            var photo = await companyPhotoService.GetPhotoBydId(request.PhotoId);

            if (photo == null)
                throw new ArgumentException("Photo not found");

            await companyPhotoService.DeletePhoto(request.PhotoId);
            await photoStorageService.Delete(photo.Name);
            await companyNotificationService.NotifyCompanyChanges(CompanyNotificationStatus.Updated, photo.CompanyId);

            return new DeletePhotoResponse { Sucess = true };
        }
    }
}
