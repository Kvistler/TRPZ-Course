using System.Collections.Generic;
using RealtorFirm.BLL.DTO;
using RealtorFirm.BLL.Infrastructure;
using RealtorFirm.DAL.Entities;
using RealtorFirm.DAL.Interfaces;
using AutoMapper;
using RealtorFirm.BLL.Interfaces;

namespace RealtorFirm.BLL.Services
{
    public class AppartmentService : IAppartmentService
    {
        IUnitOfWork Database { get; set; }

        public AppartmentService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public void Add(AppartmentDTO appDTO)
        {
            if (appDTO == null)
                throw new ValidationException("Appartment information is not entered", "");
            Appartment apply = new Appartment
            {
                Address = appDTO.Address,
                ClientId = appDTO.ClientId,
                City = appDTO.City,
                Rooms = appDTO.Rooms,
                Price = appDTO.Price,
                Status = "not rented",
                Description = appDTO.Description
            };
            Database.Appartments.Create(apply);
            Database.Save();
        }

        public void Update(AppartmentDTO appDTO)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<AppartmentDTO, Appartment>()).CreateMapper();
            Database.Appartments.Update(mapper.Map<AppartmentDTO, Appartment>(appDTO));
            Database.Save();
        }

        public void Delete(int id)
        {
            Database.Appartments.Delete(id);
            Database.Save();
        }

        public IEnumerable<AppartmentDTO> GetBySearch(int? p1, int? p2, string city, int? rooms)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Appartment, AppartmentDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Appartment>, List<AppartmentDTO>>(Database.Appartments.Find(
                p => p.Price >= p1 && p.Price <= p2 && p.City.ToLower() == city && p.Rooms == rooms && p.Status == "not rented"));
        }

        public AppartmentDTO Get(int id)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Appartment, AppartmentDTO>()).CreateMapper();
            return mapper.Map<Appartment, AppartmentDTO>(Database.Appartments.FindOne(p => p.AppartmentId == id));
        }

        public IEnumerable<AppartmentDTO> GetForClient(int? clientId)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Appartment, AppartmentDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Appartment>, List<AppartmentDTO>>(Database.Appartments.Find(p => p.ClientId == clientId));
        }

        public IEnumerable<AppartmentDTO> GetAll()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Appartment, AppartmentDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Appartment>, List<AppartmentDTO>>(Database.Appartments.GetAll());
        }

        public AppartmentDTO GetToAdd(string address, string city)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Appartment, AppartmentDTO>()).CreateMapper();
            return mapper.Map<Appartment, AppartmentDTO>(Database.Appartments.FindOne(p => p.Address == address
            && p.City == city));
        }

        public IEnumerable<ApplyDTO> GetApply(int? id)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Apply, ApplyDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Apply>, List<ApplyDTO>>(Database.Applies.Find(p => p.AppartmentId == id));
        }

        public void UpdateForClient(AppartmentDTO appDto, int id)
        { 
            Database.Appartments.Delete(appDto.AppartmentId);
            Database.Save();
            Appartment app = new Appartment
            {
                AppartmentId = appDto.AppartmentId,
                ClientId = id,
                Address = appDto.Address,
                City = appDto.City,
                Rooms = appDto.Rooms,
                Price = appDto.Price,
                Status = appDto.Status,
                Description = appDto.Description
            };
            Database.Appartments.Create(app);

            Database.Save();

        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
