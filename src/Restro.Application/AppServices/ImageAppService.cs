using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restro.DTO;
using Restro.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restro.AppServices
{
    public class ImageAppService : RestroAppServiceBase , IApplicationService
    {
        private readonly IRepository<Photos, Guid> _imageRepository;

        public ImageAppService(IRepository<Photos, Guid> imageRepository)
        {
            _imageRepository = imageRepository;
        }

        public async Task UploadImage([FromForm]byte[] fileBytes, string fileName)
        {
            var image = new Photos
            {
                ImageBytes = fileBytes,
                FileName = fileName
            };

            await _imageRepository.InsertAsync(image);

            // Return the Id of the saved image entity
            //return image.Id;
        }

        public async Task<GetImageDTO> GetImageById(Guid imageId)
        {
            var image = await _imageRepository.GetAll().AsNoTracking()
                .Where(x => x.Id == imageId)
                .Select(x => new GetImageDTO
                {
                    FileName = x.FileName,
                    ImageBytes = x.ImageBytes
                }).FirstOrDefaultAsync();
            return image;
        }
    }
}
