using RealtorFirm.BLL.DTO;
using System.Collections.Generic;

namespace RealtorFirm.BLL.Interfaces
{
    public interface IApplyService
    {
        void Add(int id, AppartmentDTO app, int user, int realtor);
        IEnumerable<ApplyDTO> Get(int? id);
        void Delete(int id);
        IEnumerable<ApplyDTO> GetNotifications(int id);
        void Update(ApplyDTO applyDto);
        UserDTO GetUser(int? id);
        void UpdateOwner(ApplyDTO applyDto, int id, int applyId);
        void UpdateSender(ApplyDTO applyDto, int id, int applyId);
        IEnumerable<ApplyDTO> GetSender(int? userId);
        IEnumerable<ApplyDTO> GetOwner(int? userId);
        IEnumerable<ApplyDTO> GetClient(int? userId);
        IEnumerable<ApplyDTO> GetAppartment(int id);
        IEnumerable<ApplyDTO> Search(int price, int rooms);
        IEnumerable<ApplyDTO> GetClientApply(int? userId);
        void Dispose();
    }
}
