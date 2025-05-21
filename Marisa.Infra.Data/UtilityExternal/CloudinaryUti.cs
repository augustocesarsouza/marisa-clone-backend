using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Marisa.Infra.Data.CloudinaryConfigClass;
using Microsoft.Extensions.Configuration;
using Marisa.Infra.Data.UtilityExternal.Interface;

namespace Marisa.Infra.Data.UtilityExternal
{
    public class CloudinaryUti : ICloudinaryUti
    {
        private readonly Account _account;
        private Cloudinary _cloudinary;

        private readonly IConfiguration _configuration;

        public CloudinaryUti(IConfiguration configuration)
        {
            _configuration = configuration;

            var apiSecret = Environment.GetEnvironmentVariable("CLOUDINARY_APISECRET") ?? _configuration["Cloudinary:ApiSecret"];
            var apiKey = Environment.GetEnvironmentVariable("CLOUDINARY_APIKEY") ?? _configuration["Cloudinary:ApiKey"];
            var accountName = Environment.GetEnvironmentVariable("CLOUDINARY_ACCOUNT_NAME") ?? _configuration["Cloudinary:AccountName"];

            _account = new Account(accountName, apiKey, apiSecret);
            var cloudinary = new Cloudinary(_account);
            _cloudinary = cloudinary;
        }

        public async Task<CloudinaryCreate> CreateMedia(string url, string folder, int width, int height)
        {

            bool isImage = url.StartsWith("data:image");
            bool isVideo = url.StartsWith("data:video");

            if (isImage)
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(url),
                    Transformation = new Transformation().Width(width).Height(height).Crop("fill").Quality(100),
                    Folder = folder
                };

                var uploadResult = _cloudinary.Upload(uploadParams);

                return new CloudinaryCreate
                {
                    PublicId = uploadResult.PublicId,
                    ImgUrl = _cloudinary.Api.UrlImgUp.BuildUrl(uploadResult.PublicId),
                };
            }
            else if (isVideo)
            {
                var uploadParams = new VideoUploadParams()
                {
                    File = new FileDescription(url),
                    Transformation = new Transformation().Width(width).Height(height).Crop("fill").Quality(100),
                    Folder = folder
                };

                var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                return new CloudinaryCreate
                {
                    PublicId = uploadResult.PublicId,
                    ImgUrl = _cloudinary.Api.UrlVideoUp.BuildUrl(uploadResult.PublicId),
                };
            }
            else
            {
                throw new ArgumentException("Invalid media type. Only images and videos are supported.");
            }
        }

        public CloudinaryResult DeleteMediaCloudinary(string url, ResourceType resourceType)
        {
            try
            {
                var destroyParams = new DeletionParams(url)
                {
                    ResourceType = resourceType
                };

                var result = _cloudinary.Destroy(destroyParams);

                if (result.Result == "ok")
                {
                    return new CloudinaryResult(true, false, "Recurso deletado com sucesso.");
                }
                else if (result.Result == "not found")
                {
                    return new CloudinaryResult(false, false, "Recurso não encontrado.");
                }
                else
                {
                    return new CloudinaryResult(false, true, $"Erro ao deletar recurso: {result.Result}");
                }
            }
            catch (Exception ex)
            {
                return new CloudinaryResult(false, true, ex.Message);
            }
        }
    }
}
