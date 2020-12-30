using System.Collections.Generic;
using RealtorFirm.BLL.DTO;
using RealtorFirm.BLL.Infrastructure;
using RealtorFirm.DAL.Entities;
using RealtorFirm.DAL.Interfaces;
using AutoMapper;
using RealtorFirm.BLL.Interfaces;

namespace RealtorFirm.BLL.Services
{
    public class UserService : IUserService
    {
        IUnitOfWork Database { get; set; }
       
        public UserService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void Add(UserDTO userDTO)
        {
            if (userDTO == null)
                throw new ValidationException("User information is not entered", "");
            User user = new User
            {
                Login = userDTO.Login,
                Name = userDTO.Name,
                Surname = userDTO.Surname,
                Password = userDTO.Password,
                Account = userDTO.Account,
                PhoneNumber = userDTO.PhoneNumber,
                Email = userDTO.Email
            };
            Database.Users.Create(user);
            Database.Save();
        }

        public void Update(UserDTO userDTO)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<UserDTO, User>()).CreateMapper();
            Database.Users.Update(mapper.Map<UserDTO, User>(userDTO));
            Database.Save();

        }
        
        public void Delete(int id)
        {
            Database.Users.Delete(id);
            Database.Save();
        }

        public UserDTO Get(string login, string password)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDTO>()).CreateMapper();
            return mapper.Map<User, UserDTO>(Database.Users.FindOne(p => p.Login == login && p.Password == password));
        }

        public UserDTO Get(int? id)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDTO>()).CreateMapper();
            return mapper.Map<User, UserDTO>(Database.Users.FindOne(p => p.UserId == id));
        }

        public IEnumerable<UserDTO> GetAll()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<User>, List<UserDTO>>(Database.Users.GetAll());
        }

        public void DeleteApp(int id)
        {
            Database.Appartments.Delete(id);
            Database.Save();
        }

        public IEnumerable<AppartmentDTO> GetApp(int? id)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Appartment, AppartmentDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Appartment>, List<AppartmentDTO>>(Database.Appartments.Find(p => p.AppartmentId == id));
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
