using AutoMapper;
using RealtorFirm.BLL.DTO;
using RealtorFirm.BLL.Infrastructure;
using RealtorFirm.BLL.Interfaces;
using RealtorFirm.DAL.Entities;
using RealtorFirm.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtorFirm.BLL.Services
{
    public class ClientService : IClientService
    {
        IUnitOfWork Database { get; set; }


        public ClientService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void Add(ClientDTO clientDTO)
        {
            if (clientDTO == null)
                throw new ValidationException("Client information is not entered", "");
            Client client = new Client
            {
                Name = clientDTO.Name,
                Surname = clientDTO.Surname,
                Patronimic = clientDTO.Patronimic,
                Account = clientDTO.Account,
                DateOfBirth = clientDTO.DateOfBirth,
                PhoneNumber = clientDTO.PhoneNumber,
                Email = clientDTO.Email,
                UserId = clientDTO.UserId,
                Role = clientDTO.Role
            };
            Database.Clients.Create(client);
            Database.Save();
        }

        public void Update(ClientDTO clientDTO)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ClientDTO,Client>()).CreateMapper();
            Database.Clients.Update(mapper.Map<ClientDTO,Client>(clientDTO));
            Database.Save();
        }

        public void DeleteUser(ClientDTO clientDTO)
        {
            Database.Clients.Delete(clientDTO.ClientId);
            Database.Save();
            Client client = new Client
            {
                Name = clientDTO.Name,
                Surname = clientDTO.Surname,
                Patronimic = clientDTO.Patronimic,
                Account = clientDTO.Account,
                DateOfBirth = clientDTO.DateOfBirth,
                PhoneNumber = clientDTO.PhoneNumber,
                Email = clientDTO.Email,
                UserId = 0,
                Role = clientDTO.Role
            };
            Database.Clients.Create(client);
            Database.Save();

        }

        public void Delete(int id)
        {
            Database.Clients.Delete(id);
            Database.Save();
        }

        public ClientDTO Get(int id)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Client, ClientDTO>()).CreateMapper();
            return mapper.Map<Client, ClientDTO>(Database.Clients.FindOne(p => p.ClientId == id));
        }

        public IEnumerable<ClientDTO> GetAll()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Client, ClientDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Client>, List<ClientDTO>>(Database.Clients.GetAll());
        }


        public ClientDTO Get(string name, string surname, DateTime date)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Client, ClientDTO>()).CreateMapper();
            return mapper.Map<Client, ClientDTO>(Database.Clients.FindOne(p => p.Name == name && p.Surname == surname && p.DateOfBirth == date));
        }

        public IEnumerable<ClientDTO> GetUser(int userId)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Client, ClientDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Client>, List<ClientDTO>>(Database.Clients.Find(p => p.UserId == userId));
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
