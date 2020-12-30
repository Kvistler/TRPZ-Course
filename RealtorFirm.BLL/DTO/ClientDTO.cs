using System;

namespace RealtorFirm.BLL.DTO
{
    public class ClientDTO
    {
        public int ClientId { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Patronimic { get; set; }

        public string Account { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Email { get; set; }

        public int UserId { get; set; }

        public string Role { get; set; }
    }
}
