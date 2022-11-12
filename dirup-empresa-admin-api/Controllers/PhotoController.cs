using dirup_empresa_admin_api.Models;
using Domain.UseCases.GetAllCompanyPhotos;
using Domain.UseCases.NewCompanyPhoto;
using Domain.UseCases.UpdateCompanyThumb;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace dirup_empresa_admin_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController : ControllerBase
    {
        private readonly string completeFilePath;
        private readonly IMediator mediator;

        public PhotoController(IWebHostEnvironment environment, IMediator mediator)
        {
            completeFilePath = environment.WebRootPath + @"\Upload";
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> PhotoUpload([FromForm] PhotoUploadModel photoUploaded)
        {
            if (photoUploaded.File == null || photoUploaded.File.Length == 0)
                throw new ArgumentException("File not found");

            CheckDirectory();

            var extension = Path.GetExtension(photoUploaded.File.FileName);
            var newFileName = $"{Guid.NewGuid()}{extension}";
            var pathWithFileName = @$"{completeFilePath}\{newFileName}";

            using FileStream fileStream = System.IO.File.Create(pathWithFileName);
            photoUploaded.File.CopyTo(fileStream);
            fileStream.Flush();
            fileStream.Dispose();

            var request = new NewCompanyPhotoRequest
            {
                CompanyId = photoUploaded.CompanyId,
                CompleteLocalStoredPath = completeFilePath,
                FileName = newFileName,
                IsThumb = photoUploaded.IsThumb
            };

            var result = await mediator.Send(request);

            System.IO.File.Delete(pathWithFileName);

            return Ok(result);
        }

        [HttpGet("{companyId}")]
        public async Task<IActionResult> Get(int companyId)
        {
            var request = new GetAllCompanyPhotosRequest { CompanyId = companyId };
            var result = await mediator.Send(request);
            return Ok(result);
        }

        [HttpPut("{photoId}")]
        public async Task<IActionResult> Update(int photoId)
        {
            var request = new UpdateCompanyThumbRequest { PhotoId = photoId };
            var result = await mediator.Send(request);
            return Ok(result);
        }

        private void CheckDirectory()
        {
            if(!Directory.Exists(completeFilePath))
                Directory.CreateDirectory(completeFilePath);
        }
    }
}
