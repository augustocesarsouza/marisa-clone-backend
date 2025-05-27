using AutoMapper;
using Marisa.Application.DTOs;
using Marisa.Application.Services.Interfaces;
using Marisa.Domain.Entities;
using Marisa.Domain.Repositories;
using Marisa.Infra.Data.CloudinaryConfigClass;
using Marisa.Infra.Data.UtilityExternal.Interface;
using SixLabors.ImageSharp;
using CloudinaryDotNet.Actions;
using Marisa.Application.DTOs.Validations.Interfaces;
using Marisa.Domain.EnumHelper;
using Marisa.Domain.Enums;

namespace Marisa.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICloudinaryUti _cloudinaryUti;
        private readonly IProductCreateDTOValidator _productCreateDTOValidator;

        public ProductService(IProductRepository productRepository, IMapper mapper, IUnitOfWork unitOfWork,
            ICloudinaryUti cloudinaryUti, IProductCreateDTOValidator productCreateDTOValidator)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _cloudinaryUti = cloudinaryUti;
            _productCreateDTOValidator = productCreateDTOValidator;
        }

        public async Task<ResultService<ProductDTO>> GetProductById(Guid? productId)
        {
            try
            {
                var product = await _productRepository.GetProductById(productId);

                if (product == null)
                    return ResultService.Fail<ProductDTO>("Error product null");

                return ResultService.Ok(_mapper.Map<ProductDTO>(product));
            }
            catch (Exception ex)
            {
                return ResultService.Fail<ProductDTO>(ex.Message);
            }
        }

        public async Task<ResultService<List<ProductDTO>>> GetAllProductType(string type)
        {
            try
            {
                var products = await _productRepository.GetAllProductType(type);

                if (products == null)
                    return ResultService.Fail<List<ProductDTO>>("Error product null");

                return ResultService.Ok(_mapper.Map<List<ProductDTO>>(products));
            }
            catch (Exception ex)
            {
                return ResultService.Fail<List<ProductDTO>>(ex.Message);
            }
        }

        public async Task<ResultService<ProductDTO>> Create(ProductDTO? productDTO)
        {
            if (productDTO == null)
                return ResultService.Fail<ProductDTO>("productDTO is null");

            var validationUser = _productCreateDTOValidator.ValidateDTO(productDTO);

            if (!validationUser.IsValid)
                return ResultService.RequestError<ProductDTO>("validation error check the information", validationUser);

            try
            {
                await _unitOfWork.BeginTransaction();

                if (productDTO.Type == null)
                    return ResultService.Fail<ProductDTO>("error Type it is null");

                var enumHere = EnumHelper.GetEnumValueFromDescription<ProductType>(productDTO.Type);

                var productId = Guid.NewGuid();
                productDTO.SetId(productId);

                Random random = new Random();

                long numero = (long)(random.NextDouble() * 9_000_000_0000L) + 1_000_000_0000L;
                productDTO.SetCode(numero);

                productDTO.SetInstallmentPrice(productDTO.Price / productDTO.InstallmentTimesMarisaCard);

                var valorDesconto = productDTO.Price * (productDTO.DiscountPercentage / 100.0);
                var precoComDesconto = productDTO.Price - valorDesconto;
                productDTO.SetPriceDiscounted(precoComDesconto);

                if (productDTO.ImageUrlBase64 != null && productDTO.ImageUrlBase64.Length > 0)
                {
                    string base64String = productDTO.ImageUrlBase64.Split(',')[1];
                    var width = 10;
                    var height = 10;

                    // Converte a string base64 em um array de bytes
                    byte[] imageBytes = Convert.FromBase64String(base64String);

                    // Usa a classe Image para carregar a imagem a partir do array de bytes
                    using (var ms = new MemoryStream(imageBytes))
                    {
                        using (var image = Image.Load(ms))
                        {
                            width = image.Width;
                            height = image.Height; // criar as imagens no cloudinary e banco de dados
                        }
                    }

                    CloudinaryCreate result = await _cloudinaryUti.CreateMedia(productDTO.ImageUrlBase64, "imgs-backend-frontend-marisa/backend/product", width, height);

                    if (result.ImgUrl == null || result.PublicId == null)
                    {
                        await _unitOfWork.Rollback();
                        return ResultService.Fail<ProductDTO>("error when create ImgPerfil");
                    }

                    productDTO.SetImageUrl(result.ImgUrl);
                }

                // fazer o "VER AS FORMAS DE PARCELAMENTO" onde tem os valores fazer isso no frontend

                var product = _mapper.Map<Product>(productDTO);

                var data = await _productRepository.CreateAsync(product);

                if (data == null)
                    return ResultService.Fail<ProductDTO>("error when create product null value");

                await _unitOfWork.Commit();

                return ResultService.Ok(_mapper.Map<ProductDTO>(data));
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return ResultService.Fail<ProductDTO>(ex.Message);
            }
        }

        public async Task<ResultService<ProductDTO>> Delete(Guid productId)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                var product = await _productRepository.GetProductByIdToDelete(productId);

                if(product == null)
                    return ResultService.Fail<ProductDTO>("error product is null");

                string url = product.ImageUrl ?? "";
                string startPattern = "/imgs-backend-frontend-marisa/backend/product/";

                int startIndex = url.IndexOf(startPattern);

                if (startIndex != -1)
                {
                    string result = url.Substring(startIndex + startPattern.Length);
                    result = "imgs-backend-frontend-marisa/backend/product/" + result; // Reanexando a parte inicial desejada

                    bool isImage = url.Contains("/image/");
                    bool isVideo = url.Contains("/video/");

                    if (isImage)
                    {
                        CloudinaryResult cloudinaryResult = _cloudinaryUti.DeleteMediaCloudinary(result, ResourceType.Image);

                        if (!cloudinaryResult.DeleteSuccessfully)
                            return ResultService.Fail<ProductDTO>("error when delete image");
                    }
                    else if (isVideo)
                    {
                        CloudinaryResult cloudinaryResult = _cloudinaryUti.DeleteMediaCloudinary(result, ResourceType.Video);

                        if (!cloudinaryResult.DeleteSuccessfully)
                            return ResultService.Fail<ProductDTO>("error when delete video");
                    }
                }

                var deleteMovie = await _productRepository.DeleteAsync(product);

                await _unitOfWork.Commit();
                return ResultService.Ok<ProductDTO>("delete successfully");
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return ResultService.Fail<ProductDTO>($"{ex.Message}");
            }
        }
    }
}
