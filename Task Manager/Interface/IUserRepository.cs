using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_Manager.Models;

namespace Task_Manager.Interface
{
    interface IUserRepository
    {
        IEnumerable<Users> GetAll();
        Users Get(int id);
        Users Add(Users user);
        Users Update(Users user);
        IEnumerable<Users> Delete(int id);
    }
}
