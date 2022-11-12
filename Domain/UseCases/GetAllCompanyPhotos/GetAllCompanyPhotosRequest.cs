using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UseCases.GetAllCompanyPhotos
{
    public class GetAllCompanyPhotosRequest : IRequest<GetAllCompanyPhotosResponse>
    {
        public int CompanyId { get; set; }
    }

    public class GetAllCompanyPhotosResponse
    {
        public IEnumerable<Photo> Photos { get; set; }
    }

    public class GetAllCompanyPhotosRequestHandler : IRequestHandler<GetAllCompanyPhotosRequest, GetAllCompanyPhotosResponse>
    {
        private readonly ICompanyPhotoService companyPhotoService;

        public GetAllCompanyPhotosRequestHandler(ICompanyPhotoService companyPhotoService)
        {
            this.companyPhotoService = companyPhotoService;
        }

        public async Task<GetAllCompanyPhotosResponse> Handle(GetAllCompanyPhotosRequest request, CancellationToken cancellationToken)
        {
            if (request.CompanyId == 0)
                throw new ArgumentException("Company Id has to be filled");

            var photos = await companyPhotoService.GetPhotoList(request.CompanyId);
            var sortedPhotos = photos.OrderByDescending(x => x.IsThumb).ThenBy(x => x.CreateDate);
               
            return new GetAllCompanyPhotosResponse
            {
                Photos = sortedPhotos
            };
        }
    }
}
