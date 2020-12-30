using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using RealtorFirm.DAL.Interfaces;
using RealtorFirm.DAL.EF;
using RealtorFirm.DAL.Entities;

namespace RealtorFirm.DAL.Repositories
{
    public class AppartmentRepository : IRepository<Appartment>
    {
        private RealtorContext db;

        public AppartmentRepository(RealtorContext context)
        {
            db = context;
        }

        public IEnumerable<Appartment> GetAll()
        {
            return db.Appartments;
        }

        public Appartment Get(int id)
        {
            return db.Appartments.Find(id);
        }

        public void Create(Appartment appartment)
        {
            db.Appartments.Add(appartment);
        }

        public void Update(Appartment appartment)
        {
            db.Entry(appartment).State = EntityState.Modified;
        }

        public IEnumerable<Appartment> Find(Func<Appartment, bool> predicate)
        {
            return db.Appartments.Where(predicate).ToList();
        }

        public Appartment FindOne(Func<Appartment, bool> predicate)
        {
            return db.Appartments.Where(predicate).FirstOrDefault();
        }

        public void Delete(int id)
        {
            Appartment appartment = db.Appartments.Find(id);
            if (appartment != null)
                db.Appartments.Remove(appartment);
        }
    }
}
