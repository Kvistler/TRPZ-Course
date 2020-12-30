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
    public class ApplyService : IApplyService
    {
        IUnitOfWork Database { get; set; }

        public ApplyService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void Add(int id, AppartmentDTO appartment, int user,int realtor)
        {
            if (appartment == null)
                throw new ValidationException("Client information is not entered", "");
            Apply apl = new Apply
            {
                SenderId = id,
                OwnerId = appartment.ClientId,
                AppartmentId = appartment.AppartmentId,
                City = appartment.City,
                Address = appartment.Address,
                Rooms = appartment.Rooms,
                Price = appartment.Price,
                Status = appartment.Status,
                Description = appartment.Description,
                ApplyStatus = "not allowed",
                UserId = user,
                OwnerRealtor = realtor
            };
            Database.Applies.Create(apl);
            Database.Save();
        }

        public IEnumerable<ApplyDTO> Get(int? id)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Apply, ApplyDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Apply>, List<ApplyDTO>>(Database.Applies.Find(p => p.UserId == id));
        }

        public IEnumerable<ApplyDTO> GetAppartment(int id)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Apply, ApplyDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Apply>, List<ApplyDTO>>(Database.Applies.Find(p => p.AppartmentId == id));
        }

        public IEnumerable<ApplyDTO> Search(int price, int rooms)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Apply, ApplyDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Apply>, List<ApplyDTO>>(Database.Applies.Find(p => p.Price == price && p.Rooms == rooms));
        }

        public void Delete(int id)
        {
            Database.Applies.Delete(id);
            Database.Save();
        }

        public IEnumerable<ApplyDTO> GetNotifications(int id)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Apply, ApplyDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Apply>, List<ApplyDTO>>(Database.Applies.Find(p => p.OwnerRealtor == id));
        }

        public void Update(ApplyDTO applyDTO)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ApplyDTO, Apply>()).CreateMapper();
            Database.Applies.Update(mapper.Map<ApplyDTO, Apply>(applyDTO));
            Database.Save();
        }

        public UserDTO GetUser(int? id)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDTO>()).CreateMapper();
            return mapper.Map<User, UserDTO>(Database.Users.FindOne(p => p.UserId == id));
        }

        public void UpdateOwner(ApplyDTO applyDTO, int id, int applyId)
        {
            Database.Applies.Delete(applyId);
            Database.Save();
            Apply apl = new Apply
            {
                SenderId = applyDTO.SenderId,
                OwnerId = id,
                AppartmentId = applyDTO.AppartmentId,
                ApplyStatus = applyDTO.ApplyStatus
            };
            Database.Applies.Create(apl);

            Database.Save();
        }

        public void UpdateSender(ApplyDTO applyDTO, int id, int applyId)
        {
            Database.Applies.Delete(applyId);
            Database.Save();
            Apply apl = new Apply
            {
                SenderId = id,
                OwnerId = applyDTO.OwnerId,
                AppartmentId = applyDTO.AppartmentId,
                ApplyStatus = applyDTO.ApplyStatus
            };
            Database.Applies.Create(apl);

            Database.Save();
        }

        public IEnumerable<ApplyDTO> GetOwner(int? userId)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Apply, ApplyDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Apply>, List<ApplyDTO>>(Database.Applies.Find(p => p.OwnerId == userId));
        }

        public IEnumerable<ApplyDTO> GetSender(int? userId)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Apply, ApplyDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Apply>, List<ApplyDTO>>(Database.Applies.Find(p => p.SenderId == userId));

        }

        public IEnumerable<ApplyDTO> GetClient(int? userId)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Apply, ApplyDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Apply>, List<ApplyDTO>>(Database.Applies.Find(p => p.SenderId == userId));
        }

        public IEnumerable<ApplyDTO> GetClientApply(int? userId)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Apply, ApplyDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Apply>, List<ApplyDTO>>(Database.Applies.Find(p => p.SenderId == userId && p.OwnerId == userId));
        }

        public void Dispose()
		{
            Database.Dispose();
		}
    }
}
