using RealtorFirm.DAL.EF;
using RealtorFirm.DAL.Entities;
using RealtorFirm.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace RealtorFirm.DAL.Repositories
{
    public class ClientRepository:IRepository<Client>
    {
        private RealtorContext db;

        public ClientRepository(RealtorContext context)
        {
            db = context;
        }

        public IEnumerable<Client> GetAll()
        {
            return db.Clients;
        }

        public Client Get(int id)
        {
            return db.Clients.Find(id);
        }

        public void Create(Client client)
        {
            db.Clients.Add(client);
        }

        public void Update(Client client)
        {
            db.Entry(client).State = EntityState.Modified;
        }

        public IEnumerable<Client> Find(Func<Client, bool> predicate)
        {
            return db.Clients.Where(predicate).ToList();
        }

        public Client FindOne(Func<Client, bool> predicate)
        {
            return db.Clients.Where(predicate).FirstOrDefault();
        }

        public void Delete(int id)
        {
            Client client = db.Clients.Find(id);
            if (client != null)
                db.Clients.Remove(client);
        }
    }
}
