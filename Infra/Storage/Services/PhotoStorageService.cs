using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Storage.Services
{
    public class PhotoStorageService : IPhotoStorageService
    {
        private readonly GoogleCloudStorage googleCloudStorage;

        public PhotoStorageService(GoogleCloudStorage googleCloudStorage)
        {
            this.googleCloudStorage = googleCloudStorage;
        }

        public async Task<string> Save(string completeFilePath, string photoName)
            => await googleCloudStorage.UploadFileAsync(completeFilePath, photoName);        
    }
}
