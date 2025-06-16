using AutoMapper;
using CloudinaryDotNet.Actions;
using Marisa.Application.DTOs;
using Marisa.Application.Services.Interfaces;
using Marisa.Domain.Entities;
using Marisa.Domain.Repositories;
using Marisa.Infra.Data.CloudinaryConfigClass;
using Marisa.Infra.Data.UtilityExternal.Interface;
using SixLabors.ImageSharp;

namespace Marisa.Application.Services
{
    public class ProductAdditionalInfoService : IProductAdditionalInfoService
    {
        private readonly IProductAdditionalInfoRepository _productAdditionalInfoRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICloudinaryUti _cloudinaryUti;

        public ProductAdditionalInfoService(IProductAdditionalInfoRepository productAdditionalInfoRepository,
            IMapper mapper, IUnitOfWork unitOfWork, ICloudinaryUti cloudinaryUti)
        {
            _productAdditionalInfoRepository = productAdditionalInfoRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _cloudinaryUti = cloudinaryUti;
        }

        public async Task<ResultService<ProductAdditionalInfoDTO>> GetProductAdditionalInfoById(Guid? productAdditionalInfoId)
        {
            try
            {
                var productAdditionalInfo = await _productAdditionalInfoRepository.GetProductAdditionalInfoById(productAdditionalInfoId);

                if (productAdditionalInfo == null)
                    return ResultService.Fail<ProductAdditionalInfoDTO>("Error productAdditionalInfo null");

                return ResultService.Ok(_mapper.Map<ProductAdditionalInfoDTO>(productAdditionalInfo));
            }
            catch (Exception ex)
            {
                return ResultService.Fail<ProductAdditionalInfoDTO>(ex.Message);
            }
        }

        public async Task<ResultService<ProductAdditionalInfoDTO>> GetProductAdditionalInfoByProductId(Guid? productId)
        {
            try
            {
                var productAdditionalInfo = await _productAdditionalInfoRepository.GetProductAdditionalInfoByProductId(productId);

                if (productAdditionalInfo == null)
                    return ResultService.Fail<ProductAdditionalInfoDTO>("Error productAdditionalInfo null");

                return ResultService.Ok(_mapper.Map<ProductAdditionalInfoDTO>(productAdditionalInfo));
            }
            catch (Exception ex)
            {
                return ResultService.Fail<ProductAdditionalInfoDTO>(ex.Message);
            }
        }

        public async Task<ResultService<ProductAdditionalInfoDTO>> Create(ProductAdditionalInfoDTO productAdditionalInfoDTO)
        {
            try
            {
                var productAdditionalInfoMap = _mapper.Map<ProductAdditionalInfo>(productAdditionalInfoDTO);

                var imgsSecondary = productAdditionalInfoDTO.ImgsSecondary;
                var imgsSecondaryListUrl = new List<string>();

                if (imgsSecondary != null && imgsSecondary.Count > 0)
                {
                    imgsSecondary.ForEach(async (el) =>
                    {
                        string base64String = el.Split(',')[1];
                        var width = 10;
                        var height = 10;

                        byte[] imageBytes = Convert.FromBase64String(base64String);

                        using (var ms = new MemoryStream(imageBytes))
                        {
                            using (var image = Image.Load(ms))
                            {
                                width = image.Width;
                                height = image.Height; // criar as imagens no cloudinary e banco de dados
                            }
                        }

                        CloudinaryCreate result = await _cloudinaryUti.CreateMedia(el, "imgs-backend-frontend-marisa/backend/product/imgs-secondary",
                            width, height);

                        if (result.ImgUrl == null || result.PublicId == null)
                        {
                            await _unitOfWork.Rollback();
                        }

                        if (result != null && result.ImgUrl != null)
                        {
                            imgsSecondaryListUrl.Add(result.ImgUrl);
                        }
                    });
                }

                productAdditionalInfoMap.SetImgsSecondary(imgsSecondaryListUrl);
                var productAdditionalInfo = await _productAdditionalInfoRepository.CreateAsync(productAdditionalInfoMap);

                if (productAdditionalInfo == null)
                    return ResultService.Fail<ProductAdditionalInfoDTO>("Error productAdditionalInfo null");

                return ResultService.Ok(_mapper.Map<ProductAdditionalInfoDTO>(productAdditionalInfo));
            }
            catch (Exception ex)
            {
                return ResultService.Fail<ProductAdditionalInfoDTO>(ex.Message);
            }
        }

        public async Task<ResultService<ProductAdditionalInfoDTO>> DeleteProductAdditionalInfo(Guid? productId)
        {
            try
            {
                var productAdditionalInfo = await _productAdditionalInfoRepository.GetProductAdditionalInfoByProductId(productId);

                if (productAdditionalInfo == null)
                {
                    return ResultService.Fail<ProductAdditionalInfoDTO>("not found");
                }

                var arrayUrl = productAdditionalInfo.ImgsSecondary;

                if (arrayUrl != null && arrayUrl.Any())
                {
                    foreach (var url in arrayUrl)
                    {
                        string startPattern = "/imgs-backend-frontend-marisa/backend/product/imgs-secondary/";

                        int startIndex = url.IndexOf(startPattern);

                        if (startIndex != -1)
                        {
                            string result = url.Substring(startIndex + startPattern.Length);
                            result = "imgs-backend-frontend-marisa/backend/product/imgs-secondary/" + result; // Reanexando a parte inicial desejada

                            bool isImage = url.Contains("/image/");
                            bool isVideo = url.Contains("/video/");

                            if (isImage)
                            {
                                CloudinaryResult cloudinaryResult = _cloudinaryUti.DeleteMediaCloudinary(result, ResourceType.Image);

                                if (!cloudinaryResult.DeleteSuccessfully)
                                    return ResultService.Fail<ProductAdditionalInfoDTO>("error when delete image");
                            }
                            else if (isVideo)
                            {
                                CloudinaryResult cloudinaryResult = _cloudinaryUti.DeleteMediaCloudinary(result, ResourceType.Video);

                                if (!cloudinaryResult.DeleteSuccessfully)
                                    return ResultService.Fail<ProductAdditionalInfoDTO>("error when delete video");
                            }
                        }

                        //var deleteFound = _cloudinaryUti.DeleteMediaCloudinary(url, ResourceType.Image);

                        //if (!deleteFound.DeleteSuccessfully)
                        //    return ResultService.Fail(deleteFound.Message);
                    }
                }

                var deleteEntity = _productAdditionalInfoRepository.DeleteAsync(productAdditionalInfo);
                var productDTOMap = _mapper.Map<ProductAdditionalInfoDTO>(deleteEntity);
                return ResultService.Ok(productDTOMap);
                
            }
            catch (Exception ex)
            {
                return ResultService.Fail<ProductAdditionalInfoDTO>(ex.Message);
            }
        }
    }
}
