using RealtorFirm.BLL.DTO;
using System;
using System.Collections.Generic;

namespace RealtorFirm.BLL.Interfaces
{
    public interface IClientService
    {
        void Add(ClientDTO clientDTO);
        void Update(ClientDTO clientDTO);
        void Delete(int id);
        ClientDTO Get(int id);
        IEnumerable<ClientDTO> GetUser(int userId);
        void DeleteUser(ClientDTO clientDTO);
        ClientDTO Get(string name, string surname, DateTime date);
        IEnumerable<ClientDTO> GetAll();
        void Dispose();
    }
}
