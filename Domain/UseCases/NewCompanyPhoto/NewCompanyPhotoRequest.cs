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
            Task resetThumbTask;
            if (!Exists(request.CompleteLocalStoredPath, request.FileName))
                throw new ArgumentException("File not found");

            var mediaLinkTask = photoStorageService.Save(request.CompleteLocalStoredPath, request.FileName);

            if (request.IsThumb)
            {
                resetThumbTask = companyPhotoService.ResetThumb(request.CompanyId);
                await Task.WhenAll(resetThumbTask, mediaLinkTask);
            }
            else 
                await Task.WhenAll(mediaLinkTask);
            
            var photo = new Photo
            {
                CompanyId = request.CompanyId,
                Name = request.FileName,
                Url = mediaLinkTask.Result,
                CreateDate = DateTime.Now
            };

            photo.SetIsThumb(request.IsThumb);
            await companyPhotoService.AddPhoto(photo);

            await notificationService.NotifyCompanyChanges(CompanyNotificationStatus.Updated, request.CompanyId);

            return new NewCompanyPhotoResponse { Sucess = true };
        }

        private bool Exists(string completePath, string fileName) =>
            File.Exists(Path.Combine(completePath, fileName));
    }
}
