using System.Collections.Generic;
using RealtorFirm.BLL.DTO;

namespace RealtorFirm.BLL.Interfaces
{
    public interface IAppartmentService
    {
        void Add(AppartmentDTO appDto);
        void Delete(int id);
        void Update(AppartmentDTO appDto);
        AppartmentDTO Get(int id);
        IEnumerable<AppartmentDTO> GetAll();
        IEnumerable<AppartmentDTO> GetForClient(int? id);
        IEnumerable<AppartmentDTO> GetBySearch(int? p1, int? p2, string city, int? rooms);
        AppartmentDTO GetToAdd(string address, string city);
        IEnumerable<ApplyDTO> GetApply(int? id);
        void UpdateForClient(AppartmentDTO appDto, int newId);
        void Dispose();
    }
}
