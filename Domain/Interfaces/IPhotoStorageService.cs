using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IPhotoStorageService
    {
        Task<string> Save(string completeFilePath, string photoName);
    }
}
