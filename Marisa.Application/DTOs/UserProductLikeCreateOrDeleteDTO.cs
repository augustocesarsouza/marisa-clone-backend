namespace Marisa.Application.DTOs
{
    public class UserProductLikeCreateOrDeleteDTO
    {
        public bool Deleted { get; set; }
        public bool Created { get; set; }
        
        public UserProductLikeCreateOrDeleteDTO(bool deleted, bool created)
        {
            Deleted = deleted;
            Created = created;
        }

        public UserProductLikeCreateOrDeleteDTO()
        {
        }
    }
}
