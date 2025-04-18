namespace Marisa.Application.DTOs
{
    public class AddressDTO
    {
        public Guid? Id { get; set; }
        public string? AddressNickname { get; set; }
        public string? AddressType { get; set; }
        public string? RecipientName { get; set; }
        public string? ZipCode { get; set; }
        public string? Street { get; set; }
        public int? Number { get; set; }
        public string? Complement { get; set; }
        public string? Neighborhood { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ReferencePoint { get; set; }
        public string? UserId { get; set; }
        public UserDTO UserDTO { get; private set; } = new UserDTO();

        public AddressDTO(Guid? id, string? addressNickname, string? addressType, string? recipientName, string? zipCode, string? street,
            int? number, string? complement, string? neighborhood, string? city, string? state, string? referencePoint)
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
        }

        public AddressDTO()
        {
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
        public string? GetUserId() => UserId;
        public void SetUserId(string? value) => UserId = value;
    }
}
