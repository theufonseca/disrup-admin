using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Infra.MySql.Models;
using Microsoft.EntityFrameworkCore;
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

        public async Task DeletePhoto(int id)
        {
            using var scope = serviceScopeFactory.CreateScope();
            var dataContext = scope.ServiceProvider.GetRequiredService<DataContext>();

            var photo = await dataContext.Photo.FirstOrDefaultAsync(x => x.Id == id);

            dataContext.Photo.Remove(photo);
            await dataContext.SaveChangesAsync();
        }

        public async Task<Photo> GetPhotoBydId(int id)
        {
            using var scope = serviceScopeFactory.CreateScope();
            var dataContext = scope.ServiceProvider.GetRequiredService<DataContext>();

            var photo = await dataContext.Photo.FirstOrDefaultAsync(x => x.Id == id);

            return mapper.Map<Photo>(photo);
        }

        public async Task<IEnumerable<Photo>> GetPhotoList(int companyId)
        {
            List<Photo> photos = new();

            using var scope = serviceScopeFactory.CreateScope();
            var dataContext = scope.ServiceProvider.GetRequiredService<DataContext>();
            var photosModel = dataContext.Photo.Where(x => x.CompanyId == companyId).ToList();

            photos.AddRange(photosModel.Select(x => mapper.Map<Photo>(x)));

            return await Task.FromResult(photos);
        }

        public Task<Photo> GetThumbPhoto(int companyId)
        {
            throw new NotImplementedException();
        }

        public async Task ResetThumb(int companyId)
        {
            using var scope = serviceScopeFactory.CreateScope();
            var dataContext = scope.ServiceProvider.GetRequiredService<DataContext>();

            var photos = dataContext.Photo.Where(x => x.CompanyId == companyId).ToList();
            foreach (var item in photos)
            {
                item.IsThumb = false;
                dataContext.Photo.Update(item);
            }

            await dataContext.SaveChangesAsync();
        }

        public async Task UpdatePhoto(Photo photo)
        {
            using var scope = serviceScopeFactory.CreateScope();
            var dataContext = scope.ServiceProvider.GetRequiredService<DataContext>();

            var photoModel = mapper.Map<PhotoModel>(photo);
            dataContext.Photo.Update(photoModel);

            await dataContext.SaveChangesAsync();
        }
    }
}
