using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Task_Manager.Models;

namespace Task_Manager.Interface
{
    public class UserRepository : IUserRepository
    {
        private List<Users> users = new List<Users>();
        public IEnumerable<Users> GetAll()
        {
            return users;
        }

        public Users Get(int id)
        {
            return users.Find(e => e.id == id);
        }

        public Users Add(Users user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("Employee");
            }
            //users.id= _nextId++;
            users.Add(user);
         
            return user;
        }

        public Users Update(Users user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("Employee");
            }
            int index = users.FindIndex(e => e.id == user.id);
            users.RemoveAt(index);
            users.Add(user);
            return user;
        }

        public IEnumerable<Users> Delete(int id)
        {
            users.RemoveAll(e => e.id == id);
            return users;
        }
    }
}
