using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using RealtorFirm.DAL.Interfaces;
using RealtorFirm.DAL.EF;
using RealtorFirm.DAL.Entities;


namespace RealtorFirm.DAL.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private RealtorContext db;

        public UserRepository(RealtorContext context)
        {
            db = context;
        }

        public IEnumerable<User> GetAll()
        {
            return db.Users;
        }

        public User Get(int id)
        {
            return db.Users.Find(id);
        }

        public void Create(User user)
        {
            db.Users.Add(user);
        }

        public void Update(User user)
        {
            db.Entry(user).State = EntityState.Modified;
        }

        public IEnumerable<User> Find(Func<User, bool> predicate)
        {
            return db.Users.Where(predicate).ToList();
        }

        public User FindOne(Func<User, bool> predicate)
        {
            return db.Users.Where(predicate).FirstOrDefault();
        }

        public void Delete(int id)
        {
            User user = db.Users.Find(id);
            if (user != null)
                db.Users.Remove(user);
        }
    }
}
