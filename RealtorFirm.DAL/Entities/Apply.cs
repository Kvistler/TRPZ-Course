using System.ComponentModel.DataAnnotations;

namespace RealtorFirm.DAL.Entities
{
    public class Apply
    {
        [Key]
        public int ApplyId { get; set; }

        public int SenderId { get; set; }

        public int OwnerId { get; set; }

        public int AppartmentId { get; set; }

        public string City { get; set; }

        public int Rooms { get; set; }

        public int Price { get; set; }

        public string Address { get; set; }

        public string Status { get; set; }

        public string Description { get; set; }

        public string ApplyStatus { get; set; }

        public int UserId { get; set; }

        public int OwnerRealtor { get; set; }

    }
}
