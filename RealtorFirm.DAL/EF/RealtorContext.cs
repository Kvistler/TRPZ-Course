using System.Data.Entity;
using RealtorFirm.DAL.Entities;

namespace RealtorFirm.DAL.EF
{
	public partial class RealtorContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Appartment> Appartments { get; set; }

        public DbSet<Apply> Applies { get; set; }

        public DbSet<Client> Clients { get; set; }

        static RealtorContext()
        {
            Database.SetInitializer(new TestDbInitializer());
        }

        public RealtorContext(string connectionString)
                : base(connectionString)
        {
        }
    }
}
