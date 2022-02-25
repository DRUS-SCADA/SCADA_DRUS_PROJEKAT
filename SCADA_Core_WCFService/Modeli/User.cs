using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCADACore
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        [Index(IsUnique = true)]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Username mora imati minimalno 3 karaktera!")]
        public string EncryptedUsername { get; set; }
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Paswword mora sadrzati minimalno 8 Karaktera")]
        public string EncryptedPassword { get; set; }


        public User() { }

        public User(string name, string surname, string encryptedUsername, string encryptedPassword)
        {
            Name = name;
            Surname = surname;
            EncryptedUsername = encryptedUsername;
            EncryptedPassword = encryptedPassword;


        }
    }
}
