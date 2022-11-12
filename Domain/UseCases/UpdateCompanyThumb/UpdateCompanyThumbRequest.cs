using Domain.Enums;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UseCases.UpdateCompanyThumb
{
    public class UpdateCompanyThumbRequest : IRequest<UpdateCompanyThumbResponse>
    {
        public int PhotoId { get; set; }
    }

    public class UpdateCompanyThumbResponse
    {
        public bool Sucess { get; set; }
    }

    public class UpdateCompanyThumbRequestHandler : IRequestHandler<UpdateCompanyThumbRequest, UpdateCompanyThumbResponse>
    {
        private readonly ICompanyPhotoService companyPhotoService;
        private readonly ICompanyNotificationService companyNotificationService;

        public UpdateCompanyThumbRequestHandler(ICompanyPhotoService companyPhotoService, 
            ICompanyNotificationService companyNotificationService)
        {
            this.companyPhotoService = companyPhotoService;
            this.companyNotificationService = companyNotificationService;
        }

        public async Task<UpdateCompanyThumbResponse> Handle(UpdateCompanyThumbRequest request, CancellationToken cancellationToken)
        {
            if (request.PhotoId == 0)
                throw new ArgumentException("PhotoId needs to be filled");

            var photo = await companyPhotoService.GetPhotoBydId(request.PhotoId);

            if (photo is null || photo.Id == 0)
                throw new ArgumentException("Photo not found");

            await companyPhotoService.ResetThumb(photo.CompanyId);

            photo.SetIsThumb(true);
            await companyPhotoService.UpdatePhoto(photo);

            await companyNotificationService.NotifyCompanyChanges(CompanyNotificationStatus.Updated, photo.CompanyId);

            return new UpdateCompanyThumbResponse
            {
                Sucess = true
            };
        }
    }
}
