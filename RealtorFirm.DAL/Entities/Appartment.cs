using System.ComponentModel.DataAnnotations;

namespace RealtorFirm.DAL.Entities
{
    public class Appartment
    {
        [Key]
        public int AppartmentId { get; set; }

        public int ClientId { get; set; }

        public string City { get; set; }

        public int Rooms { get; set;  }

        public int Price { get; set; }

        public string Address { get; set; }

        public string Status { get; set; }

        public string Description { get; set; }
    }
}
