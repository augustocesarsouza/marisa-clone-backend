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
        private readonly IQuantityAttemptChangePasswordDictionary _quantityAttemptChangePasswordDictionary;

        public AddressService(IAddressRepository addressRepository, IUserRepository userRepository, IMapper mapper, IUnitOfWork unitOfWork,
            IAddressCreateDTOValidator addressCreateDTOValidator, ICodeRandomDictionary codeRandomDictionary,
            IQuantityAttemptChangePasswordDictionary quantityAttemptChangePasswordDictionary)
        {
            _addressRepository = addressRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _addressCreateDTOValidator = addressCreateDTOValidator;
            _codeRandomDictionary = codeRandomDictionary;
            _quantityAttemptChangePasswordDictionary = quantityAttemptChangePasswordDictionary;
        }

        public async Task<ResultService<AddressDTO>> GetAddressDTOById(Guid? id)
        {
            try
            {
                var address = await _addressRepository.GetAddressByIdWithUser(id);

                if (address == null)
                    return ResultService.Fail<AddressDTO>("Error address null");

                return ResultService.Ok(_mapper.Map<AddressDTO>(address));
            }
            catch (Exception ex)
            {
                return ResultService.Fail<AddressDTO>(ex.Message);
            }
        }

        public async Task<ResultService<List<AddressDTO>>> GetAllAddressByUserId(Guid? userId)
        {
            try
            {
                var addresses = await _addressRepository.GetAllAddressByUserId(userId);

                if (addresses == null)
                    return ResultService.Fail<List<AddressDTO>>("Addresses is null");

                return ResultService.Ok(_mapper.Map<List<AddressDTO>>(addresses));
            }
            catch (Exception ex)
            {
                return ResultService.Fail<List<AddressDTO>>(ex.Message);
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

                var checkIfUserAlreadyHasAddress = await _addressRepository.CheckIfUserAlreadyHasARegisteredAddress(userId);

                addressMap.SetMainAddress(false);

                if (checkIfUserAlreadyHasAddress == null)
                    addressMap.SetMainAddress(true);


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

        public async Task<ResultService<AddressConfirmCodeEmailDTO>> VerifyCodeEmailToAddNewAddress(UserConfirmCodeEmailDTO userConfirmCodeEmailDTO)
        {
            try
            {
                if (userConfirmCodeEmailDTO.Code == null || userConfirmCodeEmailDTO.Email == null)
                    return ResultService.Fail<AddressConfirmCodeEmailDTO>("DTO property Is Null");

                var email = userConfirmCodeEmailDTO.Email;

                var userId = Guid.Parse(userConfirmCodeEmailDTO.UserId ?? "");
                var userIdString = userId.ToString();

                var (value, timeLeft) = _quantityAttemptChangePasswordDictionary.GetKey(userIdString);
                var nextQuantity = value + 1;

                if (nextQuantity == 4)
                    return ResultService.Ok(new AddressConfirmCodeEmailDTO(false, nextQuantity, timeLeft));

                if (_codeRandomDictionary.Container(email, int.Parse(userConfirmCodeEmailDTO.Code)))
                {

                    var userConfirmCodeEmail = new AddressConfirmCodeEmailDTO(true, 0, timeLeft);

                    _codeRandomDictionary.Remove(email);
                    _quantityAttemptChangePasswordDictionary.Remove(userIdString);

                    return ResultService.Ok(userConfirmCodeEmail);
                }
                else
                {
                    if (nextQuantity == 3)
                    {
                        _quantityAttemptChangePasswordDictionary.AddTimer(userId.ToString(), nextQuantity);
                        return ResultService.Ok(new AddressConfirmCodeEmailDTO(false, nextQuantity, timeLeft));
                    }

                    _quantityAttemptChangePasswordDictionary.Add(userId.ToString(), nextQuantity);

                    var userConfirmCodeEmail = new AddressConfirmCodeEmailDTO(false, nextQuantity, timeLeft);

                    return ResultService.Ok(userConfirmCodeEmail);
                }
            }
            catch (Exception ex)
            {
                return ResultService.Fail<AddressConfirmCodeEmailDTO>(ex.Message);
            }
        }

        public async Task<ResultService<AddressDTO>> Update(AddressDTO? addressDTO)
        {
            if (addressDTO == null)
                return ResultService.Fail<AddressDTO>("userDTO is null");

            var validationUser = _addressCreateDTOValidator.ValidateDTO(addressDTO);

            if (!validationUser.IsValid)
                return ResultService.RequestError<AddressDTO>("validation error check the information", validationUser);

            try
            {
                await _unitOfWork.BeginTransaction();

                string tag = addressDTO.AddressType ?? "";
                bool isValidTag = IsValidTagProduct(tag);

                if (!isValidTag)
                    return ResultService.Fail<AddressDTO>("provided type is not valid");

                Guid id = Guid.NewGuid();

                AddressType type;
                type = (AddressType)Enum.Parse(typeof(AddressType), tag, true);

                string description = GetEnumDescription(type);

                var addressUpdate = await _addressRepository.GetAddressById(addressDTO.Id);

                if(addressUpdate == null)
                    return ResultService.Fail<AddressDTO>("address not found");

                addressUpdate.SetAddressNickname(addressDTO.AddressNickname);
                addressUpdate.SetAddressType(description);
                addressUpdate.SetRecipientName(addressDTO.RecipientName);
                addressUpdate.SetZipCode(addressDTO.ZipCode);
                addressUpdate.SetStreet(addressDTO.Street);
                addressUpdate.SetNumber(addressDTO.Number);
                addressUpdate.SetComplement(addressDTO.Complement);
                addressUpdate.SetNeighborhood(addressDTO.Neighborhood);
                addressUpdate.SetCity(addressDTO.City);
                addressUpdate.SetState(addressDTO.State);
                addressUpdate.SetReferencePoint(addressDTO.ReferencePoint);

                var data = await _addressRepository.UpdateAsync(addressUpdate);

                if (data == null)
                    return ResultService.Fail<AddressDTO>("error when update address null value");

                await _unitOfWork.Commit();

                return ResultService.Ok(_mapper.Map<AddressDTO>(data));
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return ResultService.Fail<AddressDTO>(ex.Message);
            }
        }

        public async Task<ResultService<AddressDTO>> UpdateAddressPrimary(AddressDTO? addressDTO)
        {
            if (addressDTO == null)
                return ResultService.Fail<AddressDTO>("userDTO is null");

            if(addressDTO.Id == null)
                return ResultService.Fail<AddressDTO>("error id address to updated value main address");

            try
            {
                await _unitOfWork.BeginTransaction();

                var addressPrimary = await _addressRepository.GetAddressSetAsPrimary();

                if(addressPrimary == null)
                    return ResultService.Fail<AddressDTO>("error primary address is null");

                addressPrimary.SetMainAddress(false);

                var data1 = await _addressRepository.UpdateAsync(addressPrimary);

                if (data1 == null)
                    return ResultService.Fail<AddressDTO>("error when update address that changed main address to false");

                var addressToChangeValueMainAddress = await _addressRepository.GetAddressById(addressDTO.Id);

                if (addressToChangeValueMainAddress == null)
                    return ResultService.Fail<AddressDTO>("error address is null");

                addressToChangeValueMainAddress.SetMainAddress(true);

                var data2 = await _addressRepository.UpdateAsync(addressToChangeValueMainAddress);

                if (data2 == null)
                    return ResultService.Fail<AddressDTO>("error when update address that changed main address to true");

                await _unitOfWork.Commit();

                return ResultService.Ok(_mapper.Map<AddressDTO>(data2));
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return ResultService.Fail<AddressDTO>(ex.Message);
            }
        }

        public async Task<ResultService<AddressDTO>> DeleteAddress(Guid? addressId)
        {
            try
            {
                var addressToDelete = await _addressRepository.GetAddressById(addressId);

                if (addressToDelete == null)
                    return ResultService.Fail<AddressDTO>("Address is null");

                var addressDelete = await _addressRepository.DeleteAsync(addressToDelete);

                var valueMain = addressDelete.MainAddress ?? false;

                if (valueMain)
                {
                    var firstElement = await _addressRepository.GetAddressFirstRegister();

                    if(firstElement != null)
                    {
                        firstElement.SetMainAddress(true);

                        var addressUpdated = await _addressRepository.UpdateAsync(firstElement);

                        if (addressUpdated == null)
                            return ResultService.Fail<AddressDTO>("error when update register");

                        return ResultService.Ok(_mapper.Map<AddressDTO>(addressUpdated));
                    }

                    //if(firstElement == null)
                    //    return ResultService.Fail<AddressDTO>("no records found");
                }

                return ResultService.Ok(_mapper.Map<AddressDTO>(addressDelete));
            }
            catch (Exception ex)
            {
                return ResultService.Fail<AddressDTO>(ex.Message);
            }
        }
    }
}
