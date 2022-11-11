using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UseCases.NewCompanyPhoto
{
    public class NewCompanyPhotoRequest : IRequest<NewCompanyPhotoResponse>
    {
        public int CompanyId { get; init; }
        public string FileName { get; init; } = default!;
        public string CompleteLocalStoredPath { get; init; } = default!;
        public bool IsThumb { get; init; }
    }

    public class NewCompanyPhotoResponse
    {
        public bool Sucess { get; init; }
    }

    public class NewCompanyPhotoRequestHandler : IRequestHandler<NewCompanyPhotoRequest, NewCompanyPhotoResponse>
    {
        private readonly IPhotoStorageService photoStorageService;
        private readonly ICompanyPhotoService companyPhotoService;
        private readonly ICompanyNotificationService notificationService;

        public NewCompanyPhotoRequestHandler(IPhotoStorageService photoStorageService, 
            ICompanyPhotoService companyPhotoService, ICompanyNotificationService notificationService)
        {
            this.photoStorageService = photoStorageService;
            this.companyPhotoService = companyPhotoService;
            this.notificationService = notificationService;
        }

        public async Task<NewCompanyPhotoResponse> Handle(NewCompanyPhotoRequest request, CancellationToken cancellationToken)
        {
            if (!Exists(request.CompleteLocalStoredPath, request.FileName)) 
                throw new ArgumentException("File not found");

            var mediaLink = await photoStorageService.Save(request.CompleteLocalStoredPath, request.FileName);

            var photo = new Photo
            {
                CompanyId = request.CompanyId,
                IsThumb = request.IsThumb,
                Name = request.FileName,
                Url = mediaLink,
                CreateDate = DateTime.Now
            };

            await companyPhotoService.AddPhoto(photo);
            await notificationService.NotifyCompanyChanges(CompanyNotificationStatus.Updated, request.CompanyId);

            return new NewCompanyPhotoResponse { Sucess = true };
        }

        private bool Exists(string completePath, string fileName) => 
            File.Exists(Path.Combine(completePath, fileName));
    }
}
