namespace RealtorFirm.PL.Models
{
    public class AppartmentModel
    {
        public int AppartmentId { get; set; }

        public int ClientId { get; set; }

        public string City { get; set; }

        public int Rooms { get; set; }

        public int Price { get; set; }

        public string Address { get; set; }

        public string Status { get; set; }

        public string Description { get; set; }
    }
}