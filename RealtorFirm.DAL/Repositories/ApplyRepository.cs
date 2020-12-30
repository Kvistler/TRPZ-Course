using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using RealtorFirm.DAL.Interfaces;
using RealtorFirm.DAL.EF;
using RealtorFirm.DAL.Entities;

namespace RealtorFirm.DAL.Repositories
{
    public class ApplyRepository : IRepository<Apply>
    {
        private RealtorContext db;

        public ApplyRepository(RealtorContext context)
        {
            db = context;
        }

        public IEnumerable<Apply> GetAll()
        {
            return db.Applies;
        }

        public Apply Get(int id)
        {
            return db.Applies.Find(id);
        }

        public void Create(Apply apply)
        {
            db.Applies.Add(apply);
        }

        public void Update(Apply apply)
        {
            db.Entry(apply).State = EntityState.Added;
        }

        public IEnumerable<Apply> Find(Func<Apply, bool> predicate)
        {
            return db.Applies.Where(predicate).ToList();
        }

        public Apply FindOne(Func<Apply, bool> predicate)
        {
            return db.Applies.Where(predicate).FirstOrDefault();
        }

        public void Delete(int id)
        {
            Apply apply = db.Applies.Find(id);
            if (apply != null)
                db.Applies.Remove(apply);
        }
    }
}
