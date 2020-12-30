using System;
using RealtorFirm.DAL.Entities;

namespace RealtorFirm.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<User> Users { get; }
        IRepository<Appartment> Appartments { get; }
        IRepository<Apply> Applies { get; }
        IRepository<Client> Clients { get; }
        void Save();

    }
}
