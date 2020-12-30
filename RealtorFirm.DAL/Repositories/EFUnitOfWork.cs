using System;
using RealtorFirm.DAL.Interfaces;
using RealtorFirm.DAL.Entities;
using RealtorFirm.DAL.EF;

namespace RealtorFirm.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private RealtorContext db;
        
        private AppartmentRepository appartmentRepository;

        private UserRepository userRepository;

        private ApplyRepository applyRepository;

        private ClientRepository clientRepository;

        public EFUnitOfWork(string connectionString)
        {
            db = new RealtorContext(connectionString);
        }

        public IRepository<Appartment> Appartments
        {
            get
            {
                if (appartmentRepository == null)
                    appartmentRepository = new AppartmentRepository(db);
                return appartmentRepository;
            }
        }

        public IRepository<User> Users
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepository(db);
                return userRepository;
            }
        }

        public IRepository<Apply> Applies
        {
            get
            {
                if (applyRepository == null)
                    applyRepository = new ApplyRepository(db);
                return applyRepository;
            }
        }

        public IRepository<Client> Clients
        {
            get
            {
                if (clientRepository == null)
                    clientRepository = new ClientRepository(db);
                return clientRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
