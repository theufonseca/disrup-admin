using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ICompanyPhotoService
    {
        Task AddPhoto(Photo photo);
        Task UpdatePhoto(Photo photo);
        Task DeletePhoto(int id);
        Task<Photo> GetThumbPhoto(int companyId);
        Task<Photo> GetPhotoBydId(int id);
        Task<IEnumerable<Photo>> GetPhotoList(int companyId);
    }
}
