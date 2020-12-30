using System;
using System.Data.Entity;
using RealtorFirm.DAL.Entities;

namespace RealtorFirm.DAL.EF
{
	public partial class RealtorContext
	{
		public class TestDbInitializer : DropCreateDatabaseIfModelChanges<RealtorContext>
        {
            protected override void Seed(RealtorContext db)
            {
                db.Users.Add(new User { UserId = 1,
                                        Login = "Login1",
                                        Password = "12345",
                                        Name = "Name1",
                                        Surname = "Surname1",
                                        Account = "5168757329533816",
                                        PhoneNumber = "380111111111",
                                        Email = "email1@gmail.com"});
                db.Users.Add(new User { UserId = 2,
                                        Login = "Login2",
                                        Password = "11111",
                                        Name = "Name2",
                                        Surname = "Surname2",
                                        Account = "4168757329533815",
                                        PhoneNumber = "380222222222",
                                        Email = "email2@gmail.com" });
                db.Users.Add(new User { UserId = 3,
                                        Login = "Login3",
                                        Password = "22222",
                                        Name = "Name3",
                                        Surname = "Surname3",
                                        Account = "3168757329533815",
                                        PhoneNumber = "380333333333",
                                        Email = "email3@gmail.com" });
                
                db.Appartments.Add(new Appartment { AppartmentId = 1,
                                                    City = "City1",
                                                    Address = "Street 1",
                                                    Rooms = 1,
                                                    ClientId = 1,
                                                    Status = "not rented",
                                                    Price = 1000,
                                                    Description = "appartment 1" });
                db.Appartments.Add(new Appartment { AppartmentId = 2,
                                                    City = "City2",
                                                    Address = "Street 2",
                                                    Rooms = 2,
                                                    ClientId = 2,
                                                    Status = "not rented",
                                                    Price = 2000,
                                                    Description = "appartment 2"});
                
                db.Applies.Add(new Apply { ApplyId = 1,
                                           SenderId = 2,
                                           OwnerId = 2,
                                           AppartmentId = 1 ,
                                           City = "City1",
                                           Address = "Street 1",
                                           Rooms = 1,
                                           ApplyStatus = "allowed",
                                           Status = "not rented",
                                           Price = 1000,
                                           Description = "appartment 1",
                                           UserId = 3,
                                           OwnerRealtor = 1 });
                db.Applies.Add(new Apply { ApplyId = 2,
                                           SenderId = 2,
                                           OwnerId = 1,
                                           AppartmentId = 2,
                                           City = "City2",
                                           Address = "Street 2",
                                           Rooms = 2,
                                           ApplyStatus = "not allowed",
                                           Status = "not rented",
                                           Price = 7000,
                                           Description = "appartment 2",
                                           UserId = 1,
                                           OwnerRealtor = 1});
                
                db.Clients.Add(new Client { ClientId = 1,
                                            Name = "Name1",
                                            Surname = "Surname1",
                                            Patronimic = "Patronimic1",
                                            Account = "5168757329533816",
                                            PhoneNumber = "380111111111",
                                            DateOfBirth = new DateTime(2000, 1, 1),
                                            Email = "email1@gmail.com",
                                            UserId = 1,
                                            Role = "landlord" });
                db.Clients.Add(new Client { ClientId = 2,
                                            Name = "Name2",
                                            Surname = "Surname2",
                                            Patronimic = "Patronimic2",
                                            Account = "4168757329533815",
                                            PhoneNumber = "380222222222",
                                            DateOfBirth = new DateTime(2000, 2, 2),
                                            Email = "email2@gmail.com",
                                            UserId = 3,
                                            Role = "renter" });
                
                db.SaveChanges();
            }
        }
    }
}
