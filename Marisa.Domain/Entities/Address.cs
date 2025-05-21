
using System.ComponentModel;
using System.Reflection;
using Marisa.Domain.Enums;

namespace Marisa.Domain.Entities
{
    public class Address
    {
        public Guid? Id { get; private set; }
        public string? AddressNickname { get; private set; } // Apelido do Endereço
        public string? AddressType { get; private set; } // Tipo de Endereço - colocar um enum
        public string? RecipientName { get; private set; } // Nome do Destinatário
        public string? ZipCode { get; private set; } // CEP
        public string? Street { get; private set; } // Endereço
        public int? Number { get; private set; } // Número
        public string? Complement { get; private set; } // Complemento
        public string? Neighborhood { get; private set; } // Bairro
        public string? City { get; private set; } // Cidade
        public string? State { get; private set; } // UF
        public string? ReferencePoint { get; private set; } // Ponto de Referência
        public bool? MainAddress { get; private set; }
        public Guid? UserId { get; private set; }
        public User? User { get; private set; }

        public Address(Guid? id, string? addressNickname, string? addressType, string? recipientName, string? zipCode, string? street, int? number, 
            string? complement, string? neighborhood, string? city, string? state, string? referencePoint, bool? mainAddress)
        {
            Id = id;
            AddressNickname = addressNickname;
            AddressType = addressType;
            RecipientName = recipientName;
            ZipCode = zipCode;
            Street = street;
            Number = number;
            Complement = complement;
            Neighborhood = neighborhood;
            City = city;
            State = state;
            ReferencePoint = referencePoint;
            MainAddress = mainAddress;
        }

        public Address(Guid? id, string? addressNickname, string? addressType, string? recipientName, string? zipCode, string? street,
            int? number, string? complement, string? neighborhood, string? city, string? state, string? referencePoint, bool? mainAddress, Guid? userId, User? user) : this(id, addressNickname, addressType, recipientName, zipCode, street, number, complement, neighborhood, city, state, referencePoint, mainAddress)
        {
            UserId = userId;
            User = user;
        }

        public Address()
        {
        }

        public static string GetEnumDescription(AddressType value)
        {
            FieldInfo? field = value.GetType().GetField(value.ToString());

            if (field != null)
            {
                DescriptionAttribute? attribute = field.GetCustomAttribute<DescriptionAttribute>();
                return attribute?.Description ?? value.ToString();
            }

            return value.ToString();
        }

        public Guid? GetId() => Id;
        public void SetId(Guid? value) => Id = value;

        public string? GetAddressNickname() => AddressNickname;
        public void SetAddressNickname(string? value) => AddressNickname = value;

        public string? GetAddressType() => AddressType;
        public void SetAddressType(string? value) => AddressType = value;

        public string? GetRecipientName() => RecipientName;
        public void SetRecipientName(string? value) => RecipientName = value;

        public string? GetZipCode() => ZipCode;
        public void SetZipCode(string? value) => ZipCode = value;

        public string? GetStreet() => Street;
        public void SetStreet(string? value) => Street = value;

        public int? GetNumber() => Number;
        public void SetNumber(int? value) => Number = value;

        public string? GetComplement() => Complement;
        public void SetComplement(string? value) => Complement = value;

        public string? GetNeighborhood() => Neighborhood;
        public void SetNeighborhood(string? value) => Neighborhood = value;

        public string? GetCity() => City;
        public void SetCity(string? value) => City = value;

        public string? GetState() => State;
        public void SetState(string? value) => State = value;

        public string? GetReferencePoint() => ReferencePoint;
        public void SetReferencePoint(string? value) => ReferencePoint = value;

        public bool? GetMainAddress() => MainAddress;
        public void SetMainAddress(bool? value) => MainAddress = value;

        public Guid? GetUserId() => UserId;
        public void SetUserId(Guid? value) => UserId = value;

        public void SetUser(User? value) => User = value;
    }
}
