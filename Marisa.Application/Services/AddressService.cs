using System.ComponentModel;
using System.Reflection;
using AutoMapper;
using Marisa.Application.CodeRandomUser.Interface;
using Marisa.Application.DTOs;
using Marisa.Application.DTOs.Validations.Interfaces;
using Marisa.Application.Services.Interfaces;
using Marisa.Domain.Entities;
using Marisa.Domain.Enum;
using Marisa.Domain.Repositories;
using Marisa.Infra.Data.Repositories;
using Marisa.Infra.Data.UtilityExternal.Interface;
using XAct.Users;

namespace Marisa.Application.Services
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAddressCreateDTOValidator _addressCreateDTOValidator;
        private readonly ICodeRandomDictionary _codeRandomDictionary;
        private readonly ISendEmailUser _sendEmailUser;

        public AddressService(IAddressRepository addressRepository, IUserRepository userRepository, IMapper mapper, IUnitOfWork unitOfWork,
            IAddressCreateDTOValidator addressCreateDTOValidator, ICodeRandomDictionary codeRandomDictionary, ISendEmailUser sendEmailUser)
        {
            _addressRepository = addressRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _addressCreateDTOValidator = addressCreateDTOValidator;
            _codeRandomDictionary = codeRandomDictionary;
            _sendEmailUser = sendEmailUser;
        }

        public async Task<ResultService<AddressDTO>> GetAddressDTOById(Guid? id)
        {
            try
            {
                var address = await _addressRepository.GetAddressByIdWithUser(id);

                if (address == null)
                    return ResultService.Fail<AddressDTO>("Error user null");

                return ResultService.Ok(_mapper.Map<AddressDTO>(address));
            }
            catch (Exception ex)
            {
                return ResultService.Fail<AddressDTO>(ex.Message);
            }
        }

        public async Task<ResultService<AddressDTO>> Create(AddressDTO? addressDTO)
        {
            if (addressDTO == null)
                return ResultService.Fail<AddressDTO>("userDTO is null");

            var validationUser = _addressCreateDTOValidator.ValidateDTO(addressDTO);

            if (!validationUser.IsValid)
                return ResultService.RequestError<AddressDTO>("validation error check the information", validationUser);

            try
            {
                await _unitOfWork.BeginTransaction();

                var userIdString = addressDTO.GetUserId() ?? "";
                var userId = Guid.Parse(userIdString);

                string tag = addressDTO.AddressType ?? "";
                bool isValidTag = IsValidTagProduct(tag);

                if (!isValidTag)
                    return ResultService.Fail<AddressDTO>("provided type is not valid");

                Guid id = Guid.NewGuid();

                AddressType type;
                type = (AddressType)Enum.Parse(typeof(AddressType), tag, true);

                string description = GetEnumDescription(type);
                addressDTO.SetAddressType(description);
                addressDTO.SetId(id);

                var addressMap = _mapper.Map<Address>(addressDTO);
                addressMap.SetUserId(userId);

                var data = await _addressRepository.CreateAsync(addressMap);

                if (data == null)
                    return ResultService.Fail<AddressDTO>("error when create address null value");

                await _unitOfWork.Commit();

                return ResultService.Ok(_mapper.Map<AddressDTO>(data));
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return ResultService.Fail<AddressDTO>(ex.Message);
            }
        }

        public static string GetEnumDescription(Enum value)
        {
            FieldInfo? field = value.GetType().GetField(value.ToString());

            if (field != null)
            {
                DescriptionAttribute? attribute = field.GetCustomAttribute<DescriptionAttribute>();
                return attribute?.Description ?? value.ToString();
            }

            return value.ToString();
        }

        public bool IsValidTagProduct(string tagProduct)
        {
            var value = Enum.TryParse(typeof(AddressType), tagProduct, true, out _);
            return value;
        }

        private static int GerarNumeroAleatorio()
        {
            Random random = new Random();
            return random.Next(100000, 1000000);
        }
    }
}
