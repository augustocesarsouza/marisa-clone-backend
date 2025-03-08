using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Marisa.Infra.Data.CloudinaryConfigClass;

namespace Marisa.Infra.Data.UtilityExternal.Interface
{
    public interface ICloudinaryUti
    {
        public Task<CloudinaryCreate> CreateMedia(string url, string folder, int width, int height);
        public CloudinaryResult DeleteMediaCloudinary(string url, ResourceType resourceType, Cloudinary cloudinary);
    }
}
