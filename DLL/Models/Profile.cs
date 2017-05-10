using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Models
{
    class Profile
    {
        [Key, DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string salt  { get; set; }
        public string password { get; set; }
        public 
        public string Email { get; set; }
        public string Address { get; set; }
        public String Phoneno { get; set; }
        public DateTime createdon { get; set; }
        public 


    }
}
