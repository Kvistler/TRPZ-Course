using System.Collections.Generic;
using RealtorFirm.BLL.DTO;

namespace RealtorFirm.BLL.Interfaces
{
    public interface IUserService
    {
        void Add(UserDTO userDto);
        void Update(UserDTO userDto);
        void Delete(int id);
        UserDTO Get(int? id);
        UserDTO Get(string login, string password);
        IEnumerable<UserDTO> GetAll();
        void Dispose();
    }
}
