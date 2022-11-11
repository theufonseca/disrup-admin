using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Infra.MySql.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.MySql.Services
{
    public class CompanyPhotoService : ICompanyPhotoService
    {
        private readonly IMapper mapper;
        private readonly IServiceScopeFactory serviceScopeFactory;

        public CompanyPhotoService(IMapper mapper, IServiceScopeFactory serviceScopeFactory)
        {
            this.mapper = mapper;
            this.serviceScopeFactory = serviceScopeFactory;
        }

        public async Task AddPhoto(Photo photo)
        {
            var photoModel = mapper.Map<PhotoModel>(photo);

            using var scope = serviceScopeFactory.CreateScope();
            var dataContext = scope.ServiceProvider.GetRequiredService<DataContext>();

            dataContext.Photo.Add(photoModel);
            await dataContext.SaveChangesAsync();
        }

        public Task DeletePhoto(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Photo> GetPhotoBydId(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Photo>> GetPhotoList(int companyId)
        {
            throw new NotImplementedException();
        }

        public Task<Photo> GetThumbPhoto(int companyId)
        {
            throw new NotImplementedException();
        }

        public Task UpdatePhoto(Photo photo)
        {
            throw new NotImplementedException();
        }
    }
}
