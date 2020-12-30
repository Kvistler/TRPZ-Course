using System.Collections.Generic;

namespace RealtorFirm.PL.Models
{
    public class ClientsAndAppartmentsModel
    {
        public IEnumerable<ClientModel> ClientsModel { get; set; }
        public IEnumerable<AppartmentModel> AppartmentsModel { get; set; }
    }
}